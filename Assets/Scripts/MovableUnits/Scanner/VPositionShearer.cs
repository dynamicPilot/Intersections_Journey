using UnityEngine;
public interface IPositionShearer
{
    public abstract Vector3 GetSetToOriginVelocityVector();
    public abstract Vector3 GetPosition();
    public abstract Vector2 GetSize();
    public abstract float GetGap();
    public abstract IDirectionShearer GetDirectionShearer();

}


public class VPositionShearer : IPositionShearer
{
    private float _gap;
    private Vector2 _size;
    private Transform _transform;
    private BoxCollider2D _collider;

    public VPositionShearer(float gap, Vector2 size, Transform transform, BoxCollider2D collider)
    {
        _gap = gap;
        _size = size;
        _transform = transform;
        _collider = collider;

    }

    public IDirectionShearer GetDirectionShearer()
    {
        return _transform.GetComponent<VScanner>() as IDirectionShearer;
    }

    public float GetGap()
    {
        return _gap + _size.y;
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }

    public Vector3 GetSetToOriginVelocityVector()
    {
        return _transform.position - _collider.bounds.center;
    }

    public Vector2 GetSize()
    {
        return _size;
    }
}
