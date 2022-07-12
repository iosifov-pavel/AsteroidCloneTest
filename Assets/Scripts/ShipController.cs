using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ShipMovement))]
public class ShipController : MonoBehaviour
{
    [SerializeField]
    private ShipMovement _shipMovement;
    [SerializeField]
    private ShipFiringSystem _shipFiringSystem;

    private ShipData _shipData;
    private ShipControl _input;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        _input = new ShipControl();
        _input.Enable();
        SetControls();
    }

    private void SetControls()
    {
        _input.Ship.Rotate.performed += (c) => _shipMovement.RotateShip(c);
        _input.Ship.MoveForward.performed += (c) => _shipMovement.Move(c);
        _input.Ship.Fire.performed += (c) => _shipFiringSystem.ShootBullet(c);
        _input.Ship.Laser.performed += (_) => _shipFiringSystem.ShootLaser();
    }

    public void SetData(ShipData shipData)
    {
        _shipData = shipData;
    }
}
