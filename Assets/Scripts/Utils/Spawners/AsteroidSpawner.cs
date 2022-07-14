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
        var view = GameObject.Instantiate(_view, parent);
        var spawnPosition = CalculateSpawnPosition(ApplicationController.Instance.LevelBounds);
        var direction = CalculateDirectionToPlayer(spawnPosition);
        var model = new AsteroidModel(_data, spawnPosition, direction);
        view.Setup(model);
    }
}
