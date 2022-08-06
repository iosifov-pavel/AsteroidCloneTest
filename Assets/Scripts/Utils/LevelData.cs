using UnityEngine;
using System;

[System.Serializable]
public struct LevelData
{
    [SerializeField]
    public LayerMask Mask;
    [SerializeField]
    public LayerMask Laser;
    [SerializeField]
    public BoxCollider2D Bounds;
}
