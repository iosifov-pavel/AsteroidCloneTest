using UnityEngine;

public class AsteroidModel : BaseModel
{
    public const float AsteroidSmallSpeedScale = 1.25f;
    private AsteroidSize _size;
    public AsteroidSize Size => _size;
    public float SpeedScale
    {
        get
        {
            if (_size == AsteroidSize.Small)
            {
                return AsteroidSmallSpeedScale;
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
