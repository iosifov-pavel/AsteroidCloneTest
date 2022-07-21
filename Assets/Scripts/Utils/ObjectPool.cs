using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ObjectPool
{
    private static Dictionary<ObjectType, List<IPoolable>> _poolObjects;
    private static Transform _poolHolder;

    public static void Setup(Transform poolHolder)
    {
        _poolObjects = new Dictionary<ObjectType, List<IPoolable>>();
        _poolHolder = poolHolder;
    }

    public static T GetObject<T>(T poolable, ObjectType type, Vector3 position = default, Quaternion rotation = default ) where T : MonoBehaviour
    {
        T result = default;
        if (!_poolObjects.ContainsKey(type))
        {
            _poolObjects.Add(type, new List<IPoolable>());
            result = CreateNew(poolable, type, position, rotation);
        }
        else if (_poolObjects.ContainsKey(type))
        {
            var firstInActive = _poolObjects[type].FirstOrDefault(p => !p.Active);
            if(firstInActive != null)
            {
                result = (T)firstInActive;
                result.transform.position = position;
                result.transform.rotation = rotation;
            }
            else
            {
                result = CreateNew(poolable, type, position, rotation);
            }
        }
        return result;
    }

    private static T CreateNew<T>(T poolable, ObjectType type, Vector3 position = default, Quaternion rotation = default) where T : MonoBehaviour
    {
        T result;
        result = GameObject.Instantiate<T>(poolable, position, rotation, _poolHolder);
        var pool = result as IPoolable;
        _poolObjects[type].Add(pool);
        pool.Active = false;
        return result;
    }

    public static void ReturnToPool(IPoolable poolable)
    {
        poolable.Active = false;
    }
}
