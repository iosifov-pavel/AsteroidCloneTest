public interface ISpawner
{
    public void Setup(ObjectData data, BaseView view, EventManager eventManager, LevelData levelData);
    public bool CanSpawn(float time);
    public void Spawn();
}
