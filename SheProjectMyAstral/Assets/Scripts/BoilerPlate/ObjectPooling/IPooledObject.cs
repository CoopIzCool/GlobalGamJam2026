using UnityEngine;

/// <summary>
/// All objects that intend to be pooled must inherit from this interface.
/// </summary>
public interface IPooledObject
{
    void OnObjectSpawn();
}
