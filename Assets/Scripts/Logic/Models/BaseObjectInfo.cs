using System;
using UnityEngine;

public class BaseObjectInfo
{
    private Vector2 _position;
    private Vector2 _movementVector;

    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            OnPositionChange?.Invoke(_position);
        }
    }
    public Vector2 MovementVector
    {
        get => _movementVector;
        set
        {
            _movementVector = value;
            OnMovementChange?.Invoke(_movementVector);
        }
    }

    public BaseObjectInfo()
    {
        _position = Vector2.zero;
        _movementVector = Vector2.zero;
    }
    public BaseObjectInfo(Vector2 position, Vector2 movementVector)
    {
        _position = position;
        _movementVector = movementVector;
    }

    public Action<Vector2> OnPositionChange;
    public Action<Vector2> OnMovementChange;
}
