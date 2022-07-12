using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShip : Enemy
{

    public override void Initialize(Vector2 spawnPosition)
    {
        base.Initialize(spawnPosition);
        StartCoroutine(UpdatePlayerPosition());
    }

    private IEnumerator UpdatePlayerPosition()
    {
        var timer = Constants.AlienUpdatePlayerPositionTime;
        while(gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if(timer >= Constants.AlienUpdatePlayerPositionTime)
            {
                var targetDirection = GameController.Instance.PlayerPosition - (Vector2)transform.position;
                targetDirection.Normalize();
                yield return RotateToPlayer(targetDirection);
                timer = Constants.AlienRotateTime;
            }
            yield return null;
        }
    }

    private IEnumerator RotateToPlayer(Vector2 targetDirection)
    {
        var timer = 0f;
        var startDirection = _flyDirection;
        var rotateTime = Constants.AlienRotateTime + Random.Range(-Constants.AlienRotateTimeRandomModifier, Constants.AlienRotateTimeRandomModifier);
        while (true)
        {
            timer += Time.deltaTime;
            _flyDirection = Vector2.Lerp(startDirection, targetDirection, timer / rotateTime);
            if (timer >= rotateTime)
            {
                break;
            }
            yield return null;
        }
    }

    public override void Move()
    {
        transform.up = _flyDirection;
        transform.Translate(_flyDirection * Time.deltaTime * _speed, Space.World);
    }
    public override void EliminatePlayer()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null)
        {
            return;
        }
        if(IsInLayerMask(collision.gameObject,_player))
        {
            collision.gameObject.SetActive(false);
            EliminatePlayer();
        }
        if (IsInLayerMask(collision.gameObject, _bullet))
        {
            collision.gameObject.SetActive(false);
            SelfDestroy();
        }

    }

    private bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }

    public override void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
