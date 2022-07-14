using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView<M, C> : MonoBehaviour where M : BaseModel where C : BaseController<M>, new()
{
	[SerializeField]
	protected SpriteRenderer _sprite;
	[SerializeField]
	protected Transform _body;

	protected M _model;
	protected C _controller;
	protected bool ReadyToUse => _controller != null;

	public virtual void Setup(M model)
	{
		_model = model;
		_controller = new C();
		_controller.Setup(_model);
		SetCallbacks();
	}

	protected virtual void SetCallbacks()
    {

    }
}
