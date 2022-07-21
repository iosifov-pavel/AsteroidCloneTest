using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : Spawner<AsteroidModel, AsteroidController>
{
    BaseView<AsteroidModel, AsteroidController> _view;
    public override void SetSpawnObject(BaseView<AsteroidModel, AsteroidController> view)
    {
        _view = view;
    }

    public override void Spawn()
    {
        var spawnPosition = CalculateSpawnPosition(ApplicationController.Instance.LevelBounds);
        var direction = Utils.CalculateDirectionToPlayer(spawnPosition);
        var view = ObjectPool.GetObject(_view, _data.Type, spawnPosition);
        var size = Random.Range(0f, 1f) > 0.5f ? AsteroidSize.Big : AsteroidSize.Small;
        var model = new AsteroidModel(_data, spawnPosition, direction, size);
        view.Setup(model);
        ApplicationController.Instance.GameObjects.Add(view.Controller);
    }

    public void Spawn(AsteroidSize size, Vector2 spawnPosition, Vector2 direction)
    {
        var view = ObjectPool.GetObject(_view, _data.Type, spawnPosition);
        var model = new AsteroidModel(_data, spawnPosition, direction, size);
        view.Setup(model);
        ApplicationController.Instance.GameObjects.Add(view.Controller);
    }
}
