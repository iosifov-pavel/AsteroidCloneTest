using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidModel : BaseModel
{
    private AsteroidSize _size;
    public AsteroidSize Size => _size;
    public float SpeedScale
    {
        get
        {
            if(_size == AsteroidSize.Small)
            {
                return Utils.Constants.AsteroidSmallSpeedScale;
            }
            return 1;
        }
    }

    public AsteroidModel(ObjectData data, Vector2 position, Vector2 direction, AsteroidSize size) : base(data, position)
    {
        _size = size;
        Base.MovementVector = direction;
    }
}
public enum AsteroidSize
{
    Small,
    Big
}
