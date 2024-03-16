using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class EnablePassthrough : MonoBehaviour
{
    public GameObject buttonParent; // Assign in inspector, parent object of buttons
    private List<Button> buttons = new List<Button>();
    private Button activeButton;
    public int score = 0;
    public TextMeshProUGUI textbox;
    private System.Random random = new System.Random();

    public AudioClip loseSound; // Assign in inspector
    public AudioClip winSound; // Assign in inspector
    private AudioSource audioSource; // For playing sounds

    void Start()
    {
        buttons = buttonParent.GetComponentsInChildren<Button>().ToList();
        AssignButtonColorsAndListeners();
        SetRandomButtonActive();

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null) // Ensure there's an AudioSource component
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void AssignButtonColorsAndListeners()
    {
        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(delegate { ButtonClicked(btn); });
            btn.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            btn.gameObject.SetActive(false); // Initially set all buttons as invisible
        }
    }

    void SetRandomButtonActive()
    {
        // Make all buttons invisible initially
        foreach (var btn in buttons)
        {
            btn.gameObject.SetActive(false);
        }

        if (activeButton != null)
        {
            activeButton.GetComponent<Image>().color = new Color(255, 255, 255, 0); // Reset old active button color
        }

        int index = random.Next(buttons.Count);
        activeButton = buttons[index];
        activeButton.GetComponent<Image>().color = new Color(255, 255, 255, 0); // Set new active button color to transparent
        activeButton.gameObject.SetActive(true); // Only the active button is visible
    }

    public void ButtonClicked(Button clickedButton)
    {
        if (clickedButton == activeButton)
        {
            PlayWinSound();
            score += 1;
            textbox.text = "Score: " + score;
            SetRandomButtonActive(); // Set another button active
        }
        else
        {
            PlayLoseSound();
            score = 0; // Reset score
            textbox.text = "Score: " + score;
            SetRandomButtonActive(); // Set another button active
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
        if (winSound != null)
        {
            audioSource.clip = winSound;
            audioSource.Play();
        }
    }
}
