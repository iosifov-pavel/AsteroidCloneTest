using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected LayerMask _player;
    [SerializeField]
    protected LayerMask _bullet;
    [SerializeField]
    protected LayerMask _screen;
    [SerializeField]
    protected LayerMask _laser;
    [SerializeField]
    protected float _spawnTime;
    [SerializeField]
    protected float _speed;

    protected Vector2 _flyDirection;

    public float SpawnTime => _spawnTime;
    public virtual void Initialize(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
    }
    public abstract void Move();
    public abstract void EliminatePlayer();
    public abstract void SelfDestroy();

    private void Update()
    {
        Move();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }
        if (IsInLayerMask(collision.gameObject, _player))
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
}
