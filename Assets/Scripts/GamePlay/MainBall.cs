using UnityEngine;

public interface IMainBall
{
    void HitInTheDirection(Vector3 speed);
}

public class MainBall : Ball, IMainBall
{
    private Rigidbody2D _rb;
    private void Awake()
    {
        Helper.TransformStorage.RegisterTransform(Helper.GameСonstants.Ball, transform);
        Helper.ServiceLocator.SharedInstanse.Register<IMainBall>(this);
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckMinVelocity();
    }

    private void OnDestroy()
    {
        Helper.TransformStorage.UnregisterTransform(Helper.GameСonstants.Ball);
        Helper.ServiceLocator.SharedInstanse.Unregister<IMainBall>();
    }

    public void HitInTheDirection(Vector3 speed)
    {    
        _rb.AddForce(-speed, ForceMode2D.Impulse);
    }

    protected override void KillBall()
    {
        ResetBallPosition();
    }

    private void CheckMinVelocity()
    {
        if (_rb.angularVelocity <= Helper.GameСonstants.MinBallVelocity)
        {
            _rb.angularVelocity = 0;
        }
    }
}

