using System.Collections;
using UnityEngine;
using Utilites.Configs;

[RequireComponent(typeof(Rigidbody2D))]
public class VCrasher : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameConfig _gameConfig;

    private float stayInCrashTimer = 2f;
    private float forceMultiplier = 1f;
    private Rigidbody2D body;
    private VEffects effectsControl;

    //private bool isSimulating = false;
    //public bool IsSimulating { set => isSimulating = value; }
    private bool inCrash = false;

    // event
    public delegate void StartCollision(Vector3 contactPosition, int otherIndex);
    public event StartCollision OnStartCollision;

    public delegate void StartCollisionNotify();
    public event StartCollisionNotify OnStartCollisionNotify;

    public delegate void EndCollision();
    public event EndCollision OnEndCollision;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        effectsControl = GetComponent<VEffects>();

        stayInCrashTimer = _gameConfig.StayInCrashTimer;
        forceMultiplier = _gameConfig.CrashForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!isSimulating) return;
        if (collision.gameObject.CompareTag("Car"))
        {
            CollisionWithCar(collision, collision.contacts[0].point);
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (!inCrash && !isSimulating) isSimulating = true;
    //}

    public virtual void CollisionWithCar(Collision2D collision, Vector3 contactPosition)
    {
        inCrash = true;
        Logging.Log("Crash!!");
        collision.gameObject.GetComponent<VCrasher>().MovingInCollision(contactPosition - transform.position);

        if (OnStartCollision != null) OnStartCollision.Invoke(contactPosition, collision.gameObject.GetComponent<IGetUnitIndex>().GetIndex());
        InCrashTimer();
    }


    public void MovingInCollision(Vector3 pushVector)
    {
        //if (isSimulating) isSimulating = false;

        body.AddForce(pushVector.normalized * forceMultiplier, ForceMode2D.Impulse);
        inCrash = true;

        if (OnStartCollisionNotify != null) OnStartCollisionNotify.Invoke();
        InCrashTimer();
    }

    void InCrashTimer()
    {
        if (!inCrash) return;       
        effectsControl.MakeWheelSmoke();

        //if (type == TYPE.emergencyCar) effectsControl.HideMarkAndTimerForTimer();
        //needTimer = false;

        StartCoroutine(StayInCrash());
    }

    IEnumerator StayInCrash()
    {
        yield return new WaitForSeconds(stayInCrashTimer);
        AfterCrash();
    }

    public virtual void AfterCrash()
    {
        inCrash = false;
        if (OnEndCollision != null) OnEndCollision.Invoke();
    }
}
