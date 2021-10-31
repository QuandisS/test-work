using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float raycastLength;
    [SerializeField] private Transform bulletStartTransform;
    [SerializeField] private float bulletSpeed;
    
    private bool _isFirstClicked;
    private Camera _mainCamera;
    private Vector3 _bulletStartPos;

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!_isFirstClicked) _isFirstClicked = true;
        else
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (!Physics.Raycast(ray, out var hit, raycastLength)) return;
            
            _bulletStartPos = bulletStartTransform.position;
            var bullet = BulletPooler.Instance.SpawnFromPool(_bulletStartPos, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce((hit.point - _bulletStartPos).normalized * bulletSpeed);
        }
    }
}
