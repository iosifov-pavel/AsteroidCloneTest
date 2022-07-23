using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    public static ApplicationController Instance;
    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private UIManager _uiManager;
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

    private PlayerController _player;
    private List<IUpdateable> _objectsQueue;
    private AsteroidSpawner _asteroidSpawner;
    private bool _gameOver;

    public BoxCollider2D LevelBounds => _levelCollider;
    public Masks Masks => _masks;
    public AsteroidSpawner AsteroidSpawner => _asteroidSpawner;
    public List<IUpdateable> GameObjects;
    private void Awake()
    {
        EventManager.OnPlayerDeath += GameOver;
        GameObjects = new List<IUpdateable>();
        _objectsQueue = new List<IUpdateable>();
        Instance = this;
    }

    private void GameOver(object sender, EventArgs e)
    {
        _gameOver = true;
        EventManager.OnPlayerDeath -= GameOver;
    }

    void Start()
    {
        ObjectPool.Setup(_poolHolder);
        _uiManager.Setup();
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
        _asteroidSpawner = new AsteroidSpawner();
        _asteroidSpawner.Setup(asteroidData);
        _asteroidSpawner.SetSpawnObject(_asteroidView);
        StartCoroutine(CheckSpawners(_asteroidSpawner));
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
        _player = playerView.Controller;
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
        while (true)
        {
            timer += Time.deltaTime;
            if (spawner.CanSpawn(timer))
            {
                spawner.Spawn();
                timer = 0;
            }
            yield return null;
        }
    }

    public Vector2 CalculateDirectionToPlayer(Vector2 position)
    {
        var playerPosition = _player.Model.Base.Position;
        return (playerPosition - position).normalized;
    }

    private void Update()
    {
        if (_gameOver)
        {
            return;
        }
        foreach (var upd in GameObjects)
        {
            upd.Update(Time.deltaTime);
        }
        if (_objectsQueue.Count > 0)
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
