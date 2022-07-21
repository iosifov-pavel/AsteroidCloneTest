using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipSpawner : Spawner<AlienShipModel, AlienShipController>
{
    BaseView<AlienShipModel, AlienShipController> _view;
    public override void SetSpawnObject(BaseView<AlienShipModel, AlienShipController> view)
    {
        _view = view;
    }

    public override void Spawn(Transform parent)
    {
        var view = ObjectPool.GetObject(_view, _data.Type, parent);
        var spawnPosition = CalculateSpawnPosition(ApplicationController.Instance.LevelBounds);
        var direction = Utils.CalculateDirectionToPlayer(spawnPosition);
        var model = new AlienShipModel(_data, spawnPosition);
        view.Setup(model);
        ApplicationController.Instance.GameObjects.Add(view.Controller);
    }
}
