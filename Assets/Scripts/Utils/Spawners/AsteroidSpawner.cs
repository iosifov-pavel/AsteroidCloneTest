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

    public override void Spawn(Transform parent)
    {
        var view = ObjectPool.GetObject(_view, _data.Type, parent);
        var spawnPosition = CalculateSpawnPosition(ApplicationController.Instance.LevelBounds);
        var direction = Utils.CalculateDirectionToPlayer(spawnPosition);
        var model = new AsteroidModel(_data, spawnPosition, direction);
        view.Setup(model);
        ApplicationController.Instance.GameObjects.Add(view.Controller);
    }
}
