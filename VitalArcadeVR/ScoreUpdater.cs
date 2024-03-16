using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   // Its a string that contains the number of the score

    public AudioClip loseSound; // Assign in inspector
    public AudioClip winSound; // Assign in inspector
    private AudioSource audioSource; // For playing sounds

    public void UpdateScoreText()
    {
        int score = int.Parse(scoreText.text);   // It converts the score to an integer
        score++;    // It adds 1 to the score
        scoreText.text = score.ToString();   // It converts the score to a string and updates the score
    } 

    public void ResetScore()
    {
        scoreText.text = "0";   // It resets the score to 0
    }

    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound); // Play the lose sound
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound); // Play the win sound
    }
}