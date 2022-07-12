using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    public override void Initialize(Vector2 spawnPosition)
    {
        base.Initialize(spawnPosition);
        _flyDirection = CalculateFlyDirection(spawnPosition);
    }
    private Vector2 CalculateFlyDirection(Vector2 spawnPosition)
    {
        var playerPosition = GameController.Instance.PlayerPosition;
        return (playerPosition - spawnPosition).normalized;
    }
    public override void Move()
    {
        transform.Translate(_flyDirection * Time.deltaTime * _speed, Space.World);
    }
    public override void EliminatePlayer()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public override void SelfDestroy()
    {
        throw new System.NotImplementedException();
    }
}
