using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData
{
    private int _playerScore;
    private int _bulletsCount;
    private int _laserCount;

    public ShipData()
    {
        _playerScore = 0;
        _bulletsCount = Constants.PlayerBullets;
        _laserCount = Constants.PlayerLazers;
    }
}
