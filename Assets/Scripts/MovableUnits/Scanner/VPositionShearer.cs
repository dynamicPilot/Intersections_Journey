using UnityEngine;
public interface IPositionShearer
{
    public abstract Vector3 GetSetToOriginVelocityVector();
    public abstract Vector3 GetPosition();
    public abstract Vector2 GetSize();
    public abstract float GetGap();
    public abstract IDirectionShearer GetDirectionShearer();
    public abstract GameObject GetName();
}


public class VPositionShearer : IPositionShearer
{
    private float _gap;
    private Vector2 _size;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _collider;

    public VPositionShearer(float gap, Vector2 size, Rigidbody2D rigidbody, BoxCollider2D collider)
    {
        _gap = gap;
        _size = size;
        _rigidBody = rigidbody;
        _collider = collider;

    }

    public IDirectionShearer GetDirectionShearer()
    {
        return _rigidBody.GetComponent<VScanner>() as IDirectionShearer;
    }

    public float GetGap()
    {
        return _gap + _size.y;
    }

    public Vector3 GetPosition()
    {
        return _rigidBody.position;
    }

    public Vector3 GetSetToOriginVelocityVector()
    {
        return new Vector3(_rigidBody.position.x, _rigidBody.position.y, 0f) - _collider.bounds.center;
    }

    public Vector2 GetSize()
    {
        return _size;
    }

    public GameObject GetName()
    {
        return _rigidBody.gameObject;
    }
}
