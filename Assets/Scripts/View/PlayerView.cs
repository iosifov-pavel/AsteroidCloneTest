using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : BaseView<PlayerModel,PlayerController>
{
    [SerializeField]
    private Transform _bulletInitialPosition;

    public override void Setup(PlayerModel model)
    {
        base.Setup(model);
        ApplicationController.Instance.Player = _controller;
        _controller.SetInput(_bulletInitialPosition);
    }
    protected override void SetCallbacks()
    {
        EventManager.OnRotationChanged += RotatePlayer;
        _model.Base.OnPositionChange.AddListener(MovePlayer);
    }

    private void RotatePlayer(object sender, Vector2 newForward)
    {
        transform.up = newForward;
    }
    private void MovePlayer(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

}
