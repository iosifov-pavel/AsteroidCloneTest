using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ApplicationController : MonoBehaviour
{
    public static ApplicationController Instance;
    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private BulletView _bulletView;
    [SerializeField]
    private AsteroidView _asteroidView;
    [SerializeField]
    private List<ObjectData> _presets;
    [SerializeField]
    private BoxCollider2D _levelCollider;
    [SerializeField]
    private Transform _enemiesHolder;
    public BoxCollider2D LevelBounds => _levelCollider;
    public PlayerController Player { get; set; }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SpawnPlayer();
        SetSpawnres();
    }

    private void SetSpawnres()
    {
        var asteroidData = _presets.First(p => p.Type == ObjectType.Asteroid);
        var asteroidSpawner = new AsteroidSpawner();
        asteroidSpawner.Setup(asteroidData);
        asteroidSpawner.SetSpawnObject(_asteroidView);
        StartCoroutine(CheckSpawners(asteroidSpawner));
    }

    private void SpawnPlayer()
    {
        var playerData = _presets.First(p => p.Type == ObjectType.Player);
        var playerView = Instantiate(_playerView);
        var playerModel = new PlayerModel(playerData, Vector2.zero);
        playerView.Setup(playerModel);
    }

    public void SpawnBullet(Transform bulletOrigin)
    {
        var bulletData = _presets.First(p => p.Type == ObjectType.Bullet);
        var bulletView = Instantiate(_bulletView, bulletOrigin.position, bulletOrigin.rotation);
        var bulletModel = new BulletModel(bulletData, bulletView.transform.position, bulletView.transform.up);
        bulletView.Setup(bulletModel);
    }

    private IEnumerator CheckSpawners(ISpawner spawner)
    {
        var timer = 0f;
        while(true)
        {
            timer += Time.deltaTime;
            if( spawner.CanSpawn(timer))
            {
                spawner.Spawn(_enemiesHolder);
                timer = 0;
            }
            yield return null;
        }
    }
}
