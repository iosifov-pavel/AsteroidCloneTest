using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : Spawner
{
    public override void Setup(ObjectData data, BaseView view, EventManager eventManager, LevelData levelData)
    {
        base.Setup(data, view, eventManager, levelData);
        _eventManager.OnAsteroidDisassemble += SpawnAsteroidPart;
    }
    protected override void PlayerPositionCallback(Vector2 playerPosition)
    {
        var spawnPosition = CalculateSpawnPosition(_levelData.Bounds);
        var direction = (playerPosition - spawnPosition).normalized;
        var view = ObjectPool.GetObject(_view, _data.Type, spawnPosition);
        var size = Random.Range(0f, 1f) > 0.5f ? AsteroidSize.Big : AsteroidSize.Small;
        var model = new AsteroidModel(_data, spawnPosition, direction, size);
        var controller = new AsteroidController();
        controller.SetUtils(_eventManager, _levelData);
        controller.Setup(model);
        view.Setup(model, controller, false);
        _eventManager.OnSpawnNewController?.Invoke(this, controller);
    }

    public void SpawnAsteroidPart(object sender, KeyValuePair<Vector2, Vector2> spawnData)
    {
        var view = ObjectPool.GetObject(_view, _data.Type, spawnData.Key);
        var model = new AsteroidModel(_data, spawnData.Key, spawnData.Value, AsteroidSize.Small);
        var controller = new AsteroidController();
        controller.SetUtils(_eventManager, _levelData);
        controller.Setup(model);
        view.Setup(model, controller, false);
        _eventManager.OnSpawnNewController?.Invoke(this, controller);
    }
}
