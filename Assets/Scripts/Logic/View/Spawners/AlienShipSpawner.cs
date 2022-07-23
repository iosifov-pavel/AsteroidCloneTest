public class AlienShipSpawner : Spawner<AlienShipModel, AlienShipController>
{
    BaseView<AlienShipModel, AlienShipController> _view;
    public override void SetSpawnObject(BaseView<AlienShipModel, AlienShipController> view)
    {
        _view = view;
    }

    public override void Spawn()
    {
        var spawnPosition = CalculateSpawnPosition(ApplicationController.Instance.LevelBounds);
        var model = new AlienShipModel(_data, spawnPosition);
        var view = ObjectPool.GetObject(_view, _data.Type, spawnPosition);
        view.Setup(model);
        ApplicationController.Instance.GameObjects.Add(view.Controller);
    }
}
