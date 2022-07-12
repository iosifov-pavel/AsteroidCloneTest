using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData
{
    private int _health;
    private int _bulletsCount;
    private int _laserCount;

    public ShipData()
    {
        _health = Constants.PlayerHealth;
        _bulletsCount = Constants.PlayerBullets;
        _laserCount = Constants.PlayerLazers;
    }
}
