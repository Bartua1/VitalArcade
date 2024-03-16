using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MoleSmasherGame : MonoBehaviour
{
    public List<GameObject> moles = new List<GameObject>(); // Your 3D mole objects
    private GameObject activeMole;
    public int score = 0;
    public TextMeshProUGUI textbox;
    private System.Random random = new System.Random();

    public AudioClip loseSound;
    public AudioClip winSound;
    private AudioSource audioSource;

    void Start()
    {
        SetupMoles();
        SetRandomMoleActive();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void SetupMoles()
    {
        foreach (var mole in moles)
        {
            // Initially make all moles invisible
            SetMoleVisibility(mole, false);
        }
    }

    void SetRandomMoleActive()
    {
        if (activeMole != null)
        {
            // Make the old active mole invisible
            SetMoleVisibility(activeMole, false);
        }

        int index = random.Next(moles.Count);
        activeMole = moles[index];
        // Make the new active mole visible
        SetMoleVisibility(activeMole, true);
    }

    void SetMoleVisibility(GameObject mole, bool isVisible)
    {
        // Enable or disable the Renderer component to show or hide the mole
        var renderer = mole.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }

        // If your mole prefab uses child objects with their own renderers,
        // you may need to loop through and set each child's visibility as well.
    }

    public void MoleHit(GameObject hitMole)
    {
        if (hitMole == activeMole)
        {
            PlayWinSound();
            score++;
            textbox.text = "Score: " + score;
            SetRandomMoleActive(); // Choose another mole
        }
        else
        {
            PlayLoseSound();
            score = 0;
            textbox.text = "Score: " + score;
            SetRandomMoleActive(); // Choose another mole
        }
    }

    void PlayLoseSound()
    {
        if (loseSound != null)
        {
            audioSource.clip = loseSound;
            audioSource.Play();
        }
    }

    void PlayWinSound()
    {
        if (loseSound != null)
        {
            audioSource.clip = winSound;
            audioSource.Play();
        }
    }
}
