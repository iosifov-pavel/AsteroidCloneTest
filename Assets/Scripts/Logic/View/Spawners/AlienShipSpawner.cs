public class AlienShipSpawner : Spawner
{
    public override void Spawn()
    {
        var spawnPosition = CalculateSpawnPosition(ApplicationController.Instance.LevelBounds);
        var model = new AlienShipModel(_data, spawnPosition);
        var controller = new AlienShipController();
        var view = ObjectPool.GetObject(_view, _data.Type, spawnPosition);
        controller.Setup(model);
        view.Setup(model,controller,true);
        ApplicationController.Instance.GameObjects.Add(controller);
    }
}
