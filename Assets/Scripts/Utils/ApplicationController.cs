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
    private AlienShipView _alienShipView;
    [SerializeField]
    private List<ObjectData> _presets;
    [SerializeField]
    private BoxCollider2D _levelCollider;
    [SerializeField]
    private Transform _poolHolder;
    [SerializeField]
    private Masks _masks;
    public BoxCollider2D LevelBounds => _levelCollider;
    public Masks Masks => _masks;
    public PlayerController Player { get; set; }
    public List<IUpdateable> GameObjects;
    private List<IUpdateable> _objectsQueue;
    private void Awake()
    {
        GameObjects = new List<IUpdateable>();
        _objectsQueue = new List<IUpdateable>();
        Instance = this;
    }
    void Start()
    {
        ObjectPool.Setup(_poolHolder);
        SpawnPlayer();
        SetSpawnres();
    }

    private void SetSpawnres()
    {
        SetAsteroidSpawner();
        SetAlienShipSpawner();
    }

    private void SetAsteroidSpawner()
    {
        var asteroidData = _presets.First(p => p.Type == ObjectType.Asteroid);
        var asteroidSpawner = new AsteroidSpawner();
        asteroidSpawner.Setup(asteroidData);
        asteroidSpawner.SetSpawnObject(_asteroidView);
        StartCoroutine(CheckSpawners(asteroidSpawner));
    }

    private void SetAlienShipSpawner()
    {
        var alienShipData = _presets.First(p => p.Type == ObjectType.AlienShip);
        var alienShip = new AlienShipSpawner();
        alienShip.Setup(alienShipData);
        alienShip.SetSpawnObject(_alienShipView);
        StartCoroutine(CheckSpawners(alienShip));
    }

    private void SpawnPlayer()
    {
        var playerData = _presets.First(p => p.Type == ObjectType.Player);
        var playerView = Instantiate(_playerView);
        var playerModel = new PlayerModel(playerData, Vector2.zero);
        playerView.Setup(playerModel);
        GameObjects.Add(playerView.Controller);
    }

    public void SpawnBullet(Transform bulletOrigin)
    {
        var bulletData = _presets.First(p => p.Type == ObjectType.Bullet);
        var bulletView = ObjectPool.GetObject(_bulletView, bulletData.Type, position: bulletOrigin.position, rotation: bulletOrigin.rotation);
        var bulletModel = new BulletModel(bulletData, bulletView.transform.position, bulletView.transform.up);
        bulletView.Setup(bulletModel);
        _objectsQueue.Add(bulletView.Controller);
    }

    private IEnumerator CheckSpawners(ISpawner spawner)
    {
        var timer = 0f;
        while(true)
        {
            timer += Time.deltaTime;
            if( spawner.CanSpawn(timer))
            {
                spawner.Spawn();
                timer = 0;
            }
            yield return null;
        }
    }

    private void Update()
    {
        foreach(var upd in GameObjects)
        {
            upd.Update(Time.deltaTime);
        }
        if(_objectsQueue.Count > 0)
        {
            GameObjects.AddRange(_objectsQueue);
            _objectsQueue.Clear();
        }
    }
}


[System.Serializable]
public struct Masks
{
    [SerializeField]
    public LayerMask Player;
    [SerializeField]
    public LayerMask Bullet;
    [SerializeField]
    public LayerMask Screen;
    [SerializeField]
    public LayerMask Laser;
    [SerializeField]
    public LayerMask Enemy;
}
