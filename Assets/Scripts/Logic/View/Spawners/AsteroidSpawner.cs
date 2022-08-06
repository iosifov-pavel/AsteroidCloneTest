using UnityEngine;

public class AsteroidSpawner : Spawner
{
    public override void Spawn()
    {
        var spawnPosition = CalculateSpawnPosition(ApplicationController.Instance.LevelBounds);
        var direction = ApplicationController.Instance.CalculateDirectionToPlayer(spawnPosition);
        var view = ObjectPool.GetObject(_view, _data.Type, spawnPosition);
        var size = Random.Range(0f, 1f) > 0.5f ? AsteroidSize.Big : AsteroidSize.Small;
        var model = new AsteroidModel(_data, spawnPosition, direction, size);
        var controller = new AsteroidController();
        controller.Setup(model);
        view.Setup(model,controller, false);
        ApplicationController.Instance.GameObjects.Add(controller);
    }

    public void Spawn(AsteroidSize size, Vector2 spawnPosition, Vector2 direction)
    {
        var view = ObjectPool.GetObject(_view, _data.Type, spawnPosition);
        var model = new AsteroidModel(_data, spawnPosition, direction, size);
        var controller = new AsteroidController();
        controller.Setup(model);
        view.Setup(model,controller, false);
        ApplicationController.Instance.GameObjects.Add(controller);
    }
}
