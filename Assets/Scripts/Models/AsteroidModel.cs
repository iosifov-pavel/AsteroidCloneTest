using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidModel : BaseModel
{
    public AsteroidModel(ObjectData data, Vector2 position, Vector2 direction) : base(data, position)
    {
        Base.MovementVector = direction;
    }
}
