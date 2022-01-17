using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] protected Vector3 _defaultPosition = Vector3.zero;

    protected virtual void KillBall()
    {
        Destroy(this.gameObject);
    }

    protected virtual void ResetBallPosition()
    {
        if (TryGetComponent<Rigidbody2D>(out var rigidbody2D))
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0;
        }
        transform.position = _defaultPosition;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Helper.GameСonstants.BorderTag))
        {
            ResetBallPosition();
        }

        if (collision.CompareTag(Helper.GameСonstants.PocketTag))
        {
            KillBall();
        }
    }
}

