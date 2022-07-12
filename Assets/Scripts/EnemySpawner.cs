using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Collider2D _levelCollider;
    [SerializeField]
    private List<Enemy> _enemyTypes;
    [SerializeField]
    private Transform _enemiesHolder;
    private void Start()
    {
        foreach (var enemy in _enemyTypes)
        {
            StartCoroutine(SpawnEnemy(enemy));
        }
    }

    private IEnumerator SpawnEnemy(Enemy enemy)
    {
        var timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= enemy.SpawnTime)
            {
                var newEnemy = Instantiate<Enemy>(enemy, _enemiesHolder);
                newEnemy.Initialize(CalculateSpawnPosition());
                timer = 0;
            }
            yield return null;
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        var horizontalSide = Random.Range(0f, 1f) > 0.5;
        var positiveSide = Random.Range(0f, 1f) > 0.5;
        var posX = Random.Range(-_levelCollider.bounds.extents.x, _levelCollider.bounds.extents.x);
        var posY = Random.Range(-_levelCollider.bounds.extents.y, _levelCollider.bounds.extents.y);
        if (horizontalSide)
        {
            posY = positiveSide ? _levelCollider.bounds.extents.y : -_levelCollider.bounds.extents.y;
        }
        if (!horizontalSide)
        {
            posX = positiveSide ? _levelCollider.bounds.extents.x : -_levelCollider.bounds.extents.x;
        }
        return new Vector2(posX, posY);
    }
}
