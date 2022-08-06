using UnityEngine;

public abstract class Spawner: ISpawner
{
    protected ObjectData _data;
    protected BaseView _view;
    protected EventManager _eventManager;
    protected LevelData _levelData;
    public bool CanSpawn(float time)
    {
        return time >= _data.SpawnFrequency;
    }
    public virtual void Setup(ObjectData data, BaseView view, EventManager eventManager, LevelData levelData)
    {
        _data = data;
        _view = view;
        _eventManager = eventManager;
        _levelData = levelData;
    }

    public void Spawn()
    {
        PlayerPositionRequest();
    }
    private void PlayerPositionRequest()
    {
        _eventManager.OnPlayerPositionRequest?.Invoke(this, PlayerPositionCallback);
    }

    protected abstract void PlayerPositionCallback(Vector2 playerPosition);

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
