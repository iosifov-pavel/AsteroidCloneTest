using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent<Vector2> OnPositionChange = new UnityEvent<Vector2>();
}
