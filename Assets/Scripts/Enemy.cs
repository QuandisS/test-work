using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    
    private int _hp = 100;
    private Collider _mainCollider;
    private Collider[] _allColliders;
    public bool isDead;

    private void Awake()
    {
        _mainCollider = GetComponent<Collider>();
        _allColliders = GetComponentsInChildren<Collider>(true);
        Ragdoll(false);
        healthBar.SetHealth(_hp);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Bullet")) return;
        
        _hp -= 50;
        healthBar.SetHealth(_hp);
        
        if (_hp > 0) return;
        
        Ragdoll(true);
        isDead = true;
        Destroy(healthBar.gameObject);
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
