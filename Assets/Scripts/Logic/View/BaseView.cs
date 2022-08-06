using UnityEngine;

public class BaseView : MonoBehaviour, IPoolable
{
    [SerializeField]
    protected SpriteRenderer _sprite;
    [SerializeField]
    protected Transform _body;

    protected BaseController _controller;
    protected BaseModel _model;
    protected bool _rotatable;

    public BaseModel Model => _model;

    public virtual bool Active
    {
        get => gameObject.activeSelf;
        set
        {
            gameObject.SetActive(value);
        }
    }

    public virtual void Setup(BaseModel model, BaseController controller, bool rotatable)
    {
        _model = model;
        _controller = controller;
        _rotatable = rotatable;
        _sprite.sprite = model.Data.EnemySprite;
        SetCallbacks();
        Active = true;
    }

    protected virtual void SetCallbacks()
    {
        _model.Base.OnPositionChange += MoveView;
        _model.Base.OnMovementChange += RotateView;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _controller.ProceedCollision(collision, this, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _controller.ProceedCollision(collision, this, false);
    }

    private void RotateView(Vector2 newForward)
    {
        if(!_rotatable)
        {
            return;
        }
        transform.up = newForward;
    }
    private void MoveView(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    private void CheckSize()
    {
        var asteroidSize = ((AsteroidModel)_model).Size == AsteroidSize.Small ? Utils.Constants.AsteroidSmallSize : Utils.Constants.AsteroidBigSize;
        _body.localScale = new Vector3(asteroidSize, asteroidSize, 1);
    }
}
