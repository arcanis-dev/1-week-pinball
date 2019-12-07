using System.Collections;
using System.Collections.Generic;
using FlatLighting;
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
    
    private MeshRenderer bumperRenderer;
    public ParticleSystem bumperParticles;
    private PointLight ownLight;

    private Material baseMaterial;
    public Material activatedMaterial;

    public float explosionStrength = 100f;
    private void Start()
    {
        this.bumperRenderer = this.GetComponent<MeshRenderer>();
        this.baseMaterial = this.bumperRenderer.materials[0];
        this.ownLight = gameObject.GetComponentInChildren<PointLight>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {
            ParticleSystem particles = Instantiate(this.bumperParticles, this.transform.position, Quaternion.identity);
            Destroy(particles, 3f);
            this.disabled = false;
            this.timer = 0;
            this.triggered = true;
            
            if (!this.isActivated)
            {
                this.isActivated = true;
                bumperActivated++;
            }
            
            if (!this.coroutine) {
                this.ChangeMaterials();
            }
            
            collision.rigidbody.AddExplosionForce(this.explosionStrength, this.transform.position, 5);
        }
    }

    private void Update()
    {
        if (this.triggered == true) this.timer = this.timer + Time.deltaTime;
        if (this.timer >= 3 && !this.disabled && !this.coroutine && bumperActivated < 4)
        {
            bumperActivated--;
            this.isActivated = false;
            this.disabled = true;
            this.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        if (bumperActivated == 4 && !this.coroutine) this.StartCoroutine(this.AllTriggered());
    }
    
    private IEnumerator AllTriggered()
    {
        this.coroutine = true;
        ChangeMaterials();
        yield return new WaitForSeconds(1);
        ChangeMaterials();
        yield return new WaitForSeconds(1);
        ChangeMaterials();
        yield return new WaitForSeconds(1);
        ChangeMaterials();
        yield return new WaitForSeconds(1);
        ChangeMaterials();
        yield return new WaitForSeconds(1);
        ChangeMaterials();
        this.disabled = true;
        bumperActivated = 0;
        this.isActivated = false;
        this.coroutine = false;
        this.timer = 0;
        yield return new WaitForSeconds(0);
    }

    public void ChangeMaterials() {
        if (this.isActivated == true) {
            this.bumperRenderer.materials[0] = this.activatedMaterial;
            this.bumperRenderer.materials[2] = this.activatedMaterial;
            this.ownLight.enabled = true;
        }
        else {
            this.bumperRenderer.materials[0] = this.baseMaterial;
            this.bumperRenderer.materials[2] = this.baseMaterial;
            this.ownLight.enabled = false;
        }
        
    }
}
