using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipFiringSystem : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab;
    [SerializeField]
    private Transform _bulletInitialPosition;

    private bool _isFiring;
    private float _rechargeTimer;
    public void ShootBullet(InputAction.CallbackContext context)
    {
        _isFiring = context.action.IsPressed();
        if (!_isFiring)
        {
            _rechargeTimer = 0;
        }
    }

    public void ShootLaser()
    {
        Debug.Log("piu");
    }

    private void Update()
    {
        if (_isFiring)
        {
            _rechargeTimer -= Time.deltaTime;
            if (_rechargeTimer<=0)
            {
                var bullet = Instantiate<Bullet>(_bulletPrefab);
                bullet.transform.position = _bulletInitialPosition.position;
                bullet.transform.up = _bulletInitialPosition.up;
                _rechargeTimer = Constants.BulletDelay;
            }
        }
    }
}
