using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    private AsteroidSize _asteroidSize;
    public override void Initialize(Vector2 spawnPosition)
    {
        base.Initialize(spawnPosition);
        _flyDirection = CalculateFlyDirection(spawnPosition);
        SetSizeParametrs(DefineSize());
    }
    private AsteroidSize DefineSize()
    {
        var isBig = Random.Range(0f, 1f) > 0.5f;
        return isBig ? AsteroidSize.Big : AsteroidSize.Small;
    }
    private void SetSizeParametrs(AsteroidSize size)
    {
        _asteroidSize = size;
        switch(size)
        {
            case AsteroidSize.Small:
                transform.localScale = new Vector3(0.5f, 0.5f, 1);
                break;
            case AsteroidSize.Big:
                transform.localScale = new Vector3(1, 1, 1);
                break;
        }
    }
    private Vector2 CalculateFlyDirection(Vector2 spawnPosition)
    {
        var playerPosition = GameController.Instance.PlayerPosition;
        return (playerPosition - spawnPosition).normalized;
    }
    public void OverrideFlyDirection(Vector2 newDirection)
    {
        _flyDirection = newDirection;
    }
    public override void Move()
    {
        var resultSpeed = _speed * ( _asteroidSize == AsteroidSize.Big ? 1 : 1.5f );
        transform.Translate(_flyDirection * Time.deltaTime * resultSpeed, Space.World);
    }
    public override void EliminatePlayer()
    {

    }

    public override void SelfDestroy()
    {
        if(_asteroidSize == AsteroidSize.Big)
        {
            Disassemble();
        }
        gameObject.SetActive(false);
    }

    private void Disassemble()
    {
        var parts = GameController.Instance.Spawner.SpawnEnemy(this, transform.position, 2);
        foreach(var part in parts)
        {
            var asteroidPart = part as Asteroid;
            asteroidPart.SetSizeParametrs(AsteroidSize.Small);
        }
    }
}

public enum AsteroidSize
{
    Small,
    Big
}
