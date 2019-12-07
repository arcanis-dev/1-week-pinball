using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public int bumperScore = 100;
    public static int score = 0;
    public Text scoreText;
    void Start()
    {
        
    }

    private void Update()
    {
        scoreText.text = "Score : " + BumperScript.bumperActivated;
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bumper"))
        {
            score += bumperScore;
        }
    }
}
