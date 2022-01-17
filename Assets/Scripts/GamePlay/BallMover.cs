using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 0;
    [SerializeField] private int _timeToPush = 0;
    private Transform _transform;
    private Rigidbody2D _rb;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        if (TryGetComponent<Rigidbody2D>(out var rigidbody2D))
        {
            _rb = rigidbody2D;
        }

        StartCoroutine(WhaitToPushBall(_timeToPush));
    }

    private void PushBall()
    {
        _rb.AddForce(Vector2.up * _moveSpeed, ForceMode2D.Impulse);
    }

    private IEnumerator WhaitToPushBall(int time)
    {
        yield return new WaitForSeconds(time);
        PushBall();
    }


}
