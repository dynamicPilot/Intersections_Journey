using IJ.MovableUnits.MediatorAndComponents;
using System.Collections;
using UnityEngine;
using Utilites.Configs;

[RequireComponent(typeof(Rigidbody2D))]
public class VCrasher : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameConfig _gameConfig;

    private float _stayInCrashTimer = 2f;
    private float _forceMultiplier = 1f;

    private Rigidbody2D _body;
    private CrasherComponent _component;

    private bool _inCrash = false;

    public delegate void StartCollision(Vector3 contactPosition, int otherIndex);
    public event StartCollision OnStartCollision;

    public delegate void StartCollisionNotify();
    public event StartCollisionNotify OnStartCollisionNotify;

    public delegate void EndCollision();
    public event EndCollision OnEndCollision;

    private void Awake()
    {
        OnAwake();
    }

    private protected void OnAwake()
    {
        _body = GetComponent<Rigidbody2D>();

        _stayInCrashTimer = _gameConfig.StayInCrashTimer;
        _forceMultiplier = _gameConfig.CrashForce;
    }

    public void SetCrasherComponent(CrasherComponent component)
    {
        _component = component;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            CollisionWithCar(collision, collision.contacts[0].point);
        }
    }

    public virtual void CollisionWithCar(Collision2D collision, Vector3 contactPosition)
    {
        _inCrash = true;
        Logging.Log("Crash!!");
        collision.gameObject.GetComponent<VCrasher>().MovingInCollision(contactPosition - transform.position);

        if (OnStartCollision != null) OnStartCollision.Invoke(contactPosition, collision.gameObject.GetComponent<IGetUnitIndex>().GetIndex());
        InCrashTimer();
    }


    public void MovingInCollision(Vector3 pushVector)
    {
        _body.AddForce(pushVector.normalized * _forceMultiplier, ForceMode2D.Impulse);
        _inCrash = true;

        if (OnStartCollisionNotify != null) OnStartCollisionNotify.Invoke();
        InCrashTimer();
    }

    void InCrashTimer()
    {
        if (!_inCrash) return;
        _component.DoInEnterCrash();

        StartCoroutine(StayInCrash());
    }

    IEnumerator StayInCrash()
    {
        yield return new WaitForSeconds(_stayInCrashTimer);
        AfterCrash();
    }

    public virtual void AfterCrash()
    {
        _inCrash = false;
        _component.DoInExitCrash();
        if (OnEndCollision != null) OnEndCollision.Invoke();
    }
}
