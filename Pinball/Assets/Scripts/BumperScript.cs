using System.Collections;
using System.Collections.Generic;
using FlatLighting;
using UnityEngine;
using UnityEngine.UI;

public class BumperScript : MonoBehaviour
{
    
    public static int bumperActivated = 0;
    
    private bool isActivated = false;
    private bool coroutine = false;
    private float timer = 0;
    
    public Text scoreText;
    private MeshRenderer bumperRenderer;
    public ParticleSystem bumperParticles;
    private PointLight ownLight;

    private Material[] baseMaterials;
    public Material[] activatedMaterials;

    public float explosionStrength = 100f;
    private void Awake()
    {
        this.bumperRenderer = this.GetComponent<MeshRenderer>();
        this.baseMaterials = this.bumperRenderer.materials;
        this.ownLight = this.GetComponentInChildren<PointLight>();
        this.bumperParticles = this.GetComponentInChildren<ParticleSystem>();
        this.bumperParticles.Stop();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {
            this.bumperParticles.Play();
            this.timer = 0;
            
            if (!this.isActivated)
            {
                this.isActivated = true;
                bumperActivated++;
            }
            
            if (!this.coroutine) {
                Debug.Log("change materials " + this.isActivated);
                this.ChangeMaterials();
            }
            
            collision.rigidbody.AddExplosionForce(this.explosionStrength, this.transform.position, 5);
        }
    }

    private void Update()
    {
        if (this.isActivated == true) this.timer = this.timer + Time.deltaTime;
        if (this.timer >= 3 && this.isActivated && !this.coroutine && bumperActivated < 4)
        {
            bumperActivated--;
            this.isActivated = false;
            this.ChangeMaterials();
        }
        if (bumperActivated == 4 && !this.coroutine) this.StartCoroutine(this.AllTriggered());
    }
    
    private IEnumerator AllTriggered()
    {
        this.coroutine = true;
        ChangeMaterials();
        Debug.Log("change");
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
        bumperActivated = 0;
        this.isActivated = false;
        this.coroutine = false;
        this.timer = 0;
        yield return null;
    }

    public void ChangeMaterials() {
        
        if (this.isActivated == true) {
            this.bumperRenderer.materials = this.activatedMaterials;
            this.ownLight.enabled = true;
        }
        else {
            this.bumperRenderer.materials = this.baseMaterials;
            this.ownLight.enabled = false;
        }
        
        
        
    }
}
