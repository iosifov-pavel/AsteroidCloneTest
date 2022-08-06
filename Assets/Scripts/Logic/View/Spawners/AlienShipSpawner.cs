using UnityEngine;

public class AlienShipSpawner : Spawner
{
    protected override void PlayerPositionCallback(Vector2 playerPosition)
    {
        var spawnPosition = CalculateSpawnPosition(_levelData.Bounds);
        var model = new AlienShipModel(_data, spawnPosition);
        var controller = new AlienShipController();
        var view = ObjectPool.GetObject(_view, _data.Type, spawnPosition);
        controller.SetUtils(_eventManager, _levelData);
        controller.Setup(model);
        view.Setup(model, controller, true);
        _eventManager.OnSpawnNewController?.Invoke(this, controller);
    }
}
