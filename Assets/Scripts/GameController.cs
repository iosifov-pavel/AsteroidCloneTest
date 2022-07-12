using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private ShipController _player;
    [SerializeField]
    private BoxCollider2D _levelCollider;
    [SerializeField]
    private EnemySpawner _enemySpawner;

    public Vector2 PlayerPosition => _player.transform.position;
    public BoxCollider2D LevelBounds => _levelCollider;
    public EnemySpawner Spawner => _enemySpawner;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
