using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "AsteroidClon/Enemies", order = 1)]
public class ObjectData : ScriptableObject
{
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private ObjectType _type;
    [SerializeField]
    private float _spawnFrequency;
    [SerializeField]
    private float _points;
    [SerializeField]
    private float _objectScale;

    public ObjectType Type => _type;
    public float SpawnFrequency => _spawnFrequency;
    public float Speed => _speed;
    public Sprite Sprite => _sprite;
    public float Points => _points;
    public float Scale => _objectScale;
}

public enum ObjectType
{
    Asteroid,
    AlienShip,
    Bullet,
    Player
}
