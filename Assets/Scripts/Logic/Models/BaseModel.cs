using UnityEngine;

public class BaseModel
{
    protected BaseObjectInfo _baseObjectInfo;
    protected ObjectData _data;
    public BaseObjectInfo Base => _baseObjectInfo;
    public ObjectData Data => _data;

    public BaseModel(ObjectData data, Vector2 position)
    {
        _data = data;
        _baseObjectInfo = new BaseObjectInfo(position, Vector2.up);
    }
}
