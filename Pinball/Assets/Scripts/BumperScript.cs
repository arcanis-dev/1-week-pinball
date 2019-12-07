using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BumperScript : MonoBehaviour
{
    
    public static int bumperActivated = 0;
    
    private bool triggered;
    private bool isActivated = false;
    private bool disabled = true;
    private bool coroutine = false;
    private float timer = 0;
    
    public Text scoreText;
    public Material newMaterial;
    private MeshRenderer bumperRenderer;
    private AudioSource source;
    private ParticleSystem bumperParticles;

    public float explosionStrength = 100f;
    private void Start()
    {
        this.bumperRenderer = this.GetComponent<MeshRenderer>();
        this.source = this.GetComponent<AudioSource>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {
            
        }
        if (!this.isActivated)
        {
            this.isActivated = true;
            bumperActivated++;
        }

        this.disabled = false;
        this.timer = 0;
        this.triggered = true;
        Debug.Log("yolo");
        if (!this.coroutine) this.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        collision.rigidbody.AddExplosionForce(this.explosionStrength, this.transform.position, 5);
        this.source.Play();
    }

    private IEnumerator AllTriggered()
    {
        this.coroutine = true;
        this.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        this.disabled = true;
        bumperActivated = 0;
        this.isActivated = false;
        this.coroutine = false;
        this.timer = 0;
        yield return new WaitForSeconds(0);
    }

    private void Update()
    {
        if (this.triggered == true) this.timer = this.timer + Time.deltaTime;
        if (this.timer >= 3 && !this.disabled && !this.coroutine && bumperActivated < 4)
        {
            Debug.Log("caca");
            bumperActivated--;
            this.isActivated = false;
            this.disabled = true;
            this.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        if (bumperActivated == 4 && !this.coroutine) this.StartCoroutine(this.AllTriggered());
    }
}
