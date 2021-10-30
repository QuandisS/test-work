using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float raycastLength;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletStartTransform;
    [SerializeField] private float bulletSpeed;
    
    private bool _isFirstClicked = false;
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
            var bullet = Instantiate(bulletPrefab, _bulletStartPos, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce((hit.point - _bulletStartPos).normalized * bulletSpeed);
        }
    }
}
