using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    [SerializeField]
    private BaseView _baseView;
    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private List<ObjectData> _presets;
    [SerializeField]
    private Transform _poolHolder;
    [SerializeField]
    private LevelData _levelData;

    private List<IUpdateable> _objectsQueue;
    private List<IUpdateable> _activeObjects;
    private PlayerController _player;
    private bool _gameOver;
    private EventManager _eventManager;
    private void Awake()
    {
        _activeObjects = new List<IUpdateable>();
        _objectsQueue = new List<IUpdateable>();
        ObjectPool.Setup(_poolHolder);
        _eventManager = new EventManager();
        _uiManager.Setup(_eventManager);
        _eventManager.OnPlayerDeath += GameOver;
        _eventManager.OnSpawnNewController += OnSpawnNewController;
        _eventManager.OnBulletSpawn += SpawnBullet;
        _eventManager.OnGameStart += StartGame;
    }

    private void OnSpawnNewController(object sender, IUpdateable controller)
    {
        _objectsQueue.Add(controller);
    }

    private void GameOver(object sender, EventArgs e)
    {
        _gameOver = true;
        _eventManager = null;
    }

    void StartGame(object sender, EventArgs e)
    {
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
        asteroidSpawner.Setup(asteroidData, _baseView, _eventManager, _levelData);
        StartCoroutine(CheckSpawners(asteroidSpawner));
    }

    private void SetAlienShipSpawner()
    {
        var alienShipData = _presets.First(p => p.Type == ObjectType.AlienShip);
        var alienShip = new AlienShipSpawner();
        alienShip.Setup(alienShipData, _baseView, _eventManager, _levelData);
        StartCoroutine(CheckSpawners(alienShip));
    }

    private void SpawnPlayer()
    {
        var playerData = _presets.First(p => p.Type == ObjectType.Player);
        var playerView = Instantiate(_playerView);
        var playerModel = new PlayerModel(playerData, Vector2.zero);
        _player = new PlayerController();
        _player.SetUtils(_eventManager, _levelData);
        _player.Setup(playerModel);
        _player.SetInput(playerView.BulletPosition);
        playerView.Setup(playerModel, _player, false);
        _activeObjects.Add(_player);
    }

    public void SpawnBullet(object sender, Transform bulletOrigin)
    {
        var bulletData = _presets.First(p => p.Type == ObjectType.Bullet);
        var bulletView = ObjectPool.GetObject(_baseView, bulletData.Type, position: bulletOrigin.position, rotation: bulletOrigin.rotation);
        var bulletModel = new BulletModel(bulletData, bulletView.transform.position, bulletView.transform.up);
        var bulletController = new BulletController();
        bulletController.SetUtils(_eventManager, _levelData);
        bulletController.Setup(bulletModel);
        bulletView.Setup(bulletModel, bulletController, false);
        _objectsQueue.Add(bulletController);
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
        foreach (var upd in _activeObjects)
        {
            upd.Update(Time.deltaTime);
        }
        if (_objectsQueue.Count > 0)
        {
            _activeObjects.AddRange(_objectsQueue);
            _objectsQueue.Clear();
        }
    }
}