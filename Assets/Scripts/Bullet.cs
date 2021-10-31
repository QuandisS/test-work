using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter()
    {
        _rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
