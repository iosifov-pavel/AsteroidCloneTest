using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipFiringSystem : MonoBehaviour
{
    public void ShootBullet(InputAction.CallbackContext context)
    {
        Debug.Log("shoot:"+context.ReadValue<float>());
    }

    public void ShootLaser()
    {
        Debug.Log("piu");
    }
}
