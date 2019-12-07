using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour {
    private int radianCount;
    public int turnScoreMultiplier = 1;
    private float timer = 0f;
    [SerializeField]private bool isSpinning;

    private Rigidbody rb;
    private HingeJoint hinge;
    private JointSpring joint;
    private JointMotor hingeMotor;
    private TraumaInducer screenShaker;

    private void Start() {
        this.hinge = this.GetComponent<HingeJoint>();
        this.rb = this.GetComponent<Rigidbody>();
        this.hingeMotor = this.hinge.motor;
        this.joint = this.hinge.spring;
        this.screenShaker = this.GetComponent<TraumaInducer>();
    }

    private void Update() {
        if (this.isSpinning) {
            this.hingeMotor.targetVelocity -= 3;
            this.timer = this.timer + Time.deltaTime;

        }

        if (this.hingeMotor.targetVelocity <= 0) {
            this.hinge.useMotor = false;
            this.hinge.useSpring = true;
            this.isSpinning = false;
            Debug.Log("1");

            //hinge.useMotor = false;
            //hinge.useSpring = true;
        }

        if (!this.rb.IsSleeping()) this.radianCount += Mathf.RoundToInt(this.rb.angularVelocity.magnitude);

        if (this.radianCount > 360) {

            BallScript.score += 1 * this.turnScoreMultiplier;
            this.radianCount -= 360;
        }

        if (this.rb.IsSleeping()) {
            this.isSpinning = false;
            // Debug.Log("3");
            this.radianCount = 0;
            this.timer = 0f;
        }

        this.hinge.motor = this.hingeMotor;
        this.hinge.spring = this.joint;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            this.isSpinning = true;
            this.hingeMotor.targetVelocity = 2000;
            this.hinge.useMotor = true;
            this.timer = 0f;

            screenShaker.StartCoroutine("ScreenShake");
        }
    }
}