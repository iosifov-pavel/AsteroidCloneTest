using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlyForward 
{
    public void FlyForward(float deltaTime, float speedScale = 1);
}
