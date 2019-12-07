using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsScript : MonoBehaviour
{
    public Transform ball;
    Vector3 startingPosition;
    void Start()
    {
        startingPosition = ball.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.rigidbody.velocity = Vector3.zero;
            collision.transform.position = startingPosition;
        }
    }
}
