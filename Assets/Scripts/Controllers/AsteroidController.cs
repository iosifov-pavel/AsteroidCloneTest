using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : BaseController<AsteroidModel>
{
    public override void Update(float timeStep)
    {
        FlyForward(timeStep);
    }
}
