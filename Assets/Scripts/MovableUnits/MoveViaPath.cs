using UnityEngine;

public interface IMovable
{
    public abstract void SetNewPosition(Vector3 _position);
    public abstract float GetDistanceToMove();
    public abstract Vector3 GetPosition();
}

public interface ISetDistanceToMove
{
    public abstract void SetMovingParams(float velocity, float acceleration, float deltaT);
}

public class MoveViaPath: IMovable, ISetDistanceToMove
{
    readonly Transform transform;
    float distance = 0f;
    Vector3 position;
    public MoveViaPath(Transform _transform)
    {
        transform = _transform;
    }

    public void SetMovingParams(float velocity, float acceleration, float deltaT)
    {
        distance = (velocity * deltaT + acceleration * deltaT * deltaT / 2);
    }

    public float GetDistanceToMove()
    {
        return distance;
    }

    public void SetNewPosition(Vector3 _position)
    {
        position = _position;
    }

    public void MoveAndRotate()
    {
        Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 360f - Mathf.Atan2(position.x - transform.position.x, position.y - transform.position.y) * Mathf.Rad2Deg);
        transform.Translate(new Vector3(position.x - transform.position.x, position.y - transform.position.y, 0f), Space.World);
        transform.Rotate(rotation.eulerAngles - transform.rotation.eulerAngles, Space.World);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
