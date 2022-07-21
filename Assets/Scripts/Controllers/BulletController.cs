using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : BaseController<BulletModel>
{
    public override void Update(float timeStep)
    {
        FlyForward(timeStep);
    }
}
