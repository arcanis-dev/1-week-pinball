using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour
{
    int radiantCount;
    public int turnScoreMultiplier = 1;
    float timer = 0f;
    Rigidbody rb;
    bool isSpinning;
    HingeJoint hinge;
    JointSpring joint;
    JointMotor hingeMotor;
   
    private void Start()
    {
        hinge = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        hingeMotor = hinge.motor;
        joint = hinge.spring;
    }
    private void Update()
    {
        if (isSpinning)
        {
            hingeMotor.targetVelocity -= 3;
            timer = timer + Time.deltaTime;
            
        }

        if (hingeMotor.targetVelocity <= 0)
        {
            hinge.useMotor = false;
            hinge.useSpring = true;
            isSpinning = false;
            Debug.Log("1");

            //hinge.useMotor = false;
            //hinge.useSpring = true;
        }

        if (!rb.IsSleeping())
        {
            
            radiantCount += Mathf.RoundToInt(rb.angularVelocity.magnitude);
        }

        if (radiantCount > 360)
        {

            BallScript.score += 1 * turnScoreMultiplier;
            radiantCount -= 360;
        }

        if (rb.IsSleeping())
        {
            isSpinning = false;
            // Debug.Log("3");
            radiantCount = 0;
            timer = 0f;
        }
        hinge.motor = hingeMotor;
        hinge.spring = joint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            isSpinning = true;
            hingeMotor.targetVelocity = 2000;
            hinge.useMotor = true;
            timer = 0f;
        }
    }
}

