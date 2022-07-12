using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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

    public List<Enemy> SpawnEnemy(Enemy enemy, int count = 1)
    {
        var spawnedEnemies = new List<Enemy>();
        for(var i = 0; i < count;i++)
        {
            var newEnemy = Instantiate<Enemy>(enemy, _enemiesHolder);
            newEnemy.Initialize(CalculateSpawnPosition(GameController.Instance.LevelBounds));
            spawnedEnemies.Add(newEnemy);
        }
        return spawnedEnemies;
    }

    private IEnumerator SpawnEnemy(Enemy enemy)
    {
        var timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= enemy.SpawnTime)
            {
                SpawnEnemy(enemy);
                timer = 0;
            }
            yield return null;
        }
    }

    private Vector2 CalculateSpawnPosition(BoxCollider2D collider)
    {
        var horizontalSide = Random.Range(0f, 1f) > 0.5;
        var positiveSide = Random.Range(0f, 1f) > 0.5;
        var posX = Random.Range(-collider.bounds.extents.x, collider.bounds.extents.x);
        var posY = Random.Range(-collider.bounds.extents.y, collider.bounds.extents.y);
        if (horizontalSide)
        {
            posY = positiveSide ? collider.bounds.extents.y : -collider.bounds.extents.y;
        }
        if (!horizontalSide)
        {
            posX = positiveSide ? collider.bounds.extents.x : -collider.bounds.extents.x;
        }
        return new Vector2(posX, posY);
    }
}
