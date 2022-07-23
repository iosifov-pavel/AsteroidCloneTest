public interface ISpawner
{
    public void Setup(ObjectData data);
    public bool CanSpawn(float time);
    public void Spawn();
}
