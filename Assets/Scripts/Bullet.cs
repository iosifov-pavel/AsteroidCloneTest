using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        var bulletMove = Vector2.up * Time.deltaTime * Constants.BulletSpeed;
        transform.Translate(bulletMove);
    }
}
