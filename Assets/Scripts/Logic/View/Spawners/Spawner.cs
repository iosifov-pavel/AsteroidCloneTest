using UnityEngine;

public abstract class Spawner<M, C> : ISpawner where M : BaseModel where C : BaseController<M>, new()
{
    protected ObjectData _data;

    public bool CanSpawn(float time)
    {
        return time >= _data.SpawnFrequency;
    }
    public void Setup(ObjectData data)
    {
        _data = data;
    }

    public abstract void SetSpawnObject(BaseView<M, C> view);

    public abstract void Spawn();

    protected Vector2 CalculateSpawnPosition(BoxCollider2D collider)
    {
        var horizontalSide = Random.Range(0f, 1f) > 0.5;
        var positiveSide = Random.Range(0f, 1f) > 0.5;
        var posX = Random.Range(-collider.bounds.extents.x, collider.bounds.extents.x);
        var posY = Random.Range(-collider.bounds.extents.y, collider.bounds.extents.y);
        if (horizontalSide)
        {
            posY = positiveSide ? collider.bounds.extents.y : -collider.bounds.extents.y;
        }
        if (!horizontalSide)
        {
            posX = positiveSide ? collider.bounds.extents.x : -collider.bounds.extents.x;
        }
        return new Vector2(posX, posY);
    }
}
