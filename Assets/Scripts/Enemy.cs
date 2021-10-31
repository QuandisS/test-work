using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _hp = 100;
    private Collider _mainCollider;
    private Collider[] _allColliders;
    public bool isDead;

    private void Awake()
    {
        _mainCollider = GetComponent<Collider>();
        _allColliders = GetComponentsInChildren<Collider>(true);
        Ragdoll(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Bullet")) return;
        
        _hp -= 50;
        
        if (_hp > 0) return;
        
        Ragdoll(true);
        isDead = true;
    }

    private void Ragdoll(bool isRagdoll)
    {
        foreach (var col in _allColliders)
        {
            col.enabled = isRagdoll;
        }

        _mainCollider.enabled = !isRagdoll;
        GetComponent<Rigidbody>().useGravity = isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
    }
}
