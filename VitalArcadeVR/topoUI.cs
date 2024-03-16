using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ToposUI : MonoBehaviour
{
    public Button playTopos;
    private Button activeButton;
    public GameObject buttonParent; // Assign in inspector, parent object of buttons
    private List<Button> topos = new List<Button>();
    public int score = 0;
    private System.Random random = new System.Random();
    public TextMeshProUGUI textbox;
    public AudioClip loseSound; // Assign in inspector
    public AudioClip winSound; // Assign in inspector
    private AudioSource audioSource; // For playing sound

    void Start()
    {
        topos = buttonParent.GetComponentsInChildren<Button>().ToList();
        SetToposInactive(); // Corrected method name to follow C# naming conventions
        playTopos.onClick.AddListener(delegate { ButtonClicked(playTopos); });
        playTopos.gameObject.SetActive(true); // Initially set the play button as visible
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null) // Ensure there's an AudioSource component
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void AssignToposColorsAndListeners()
    {
        foreach (var btn in topos)
        {
            btn.onClick.AddListener(delegate { TopoClicked(btn); });
            btn.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            btn.gameObject.SetActive(false); // Initially set all buttons as invisible
        }
    }

    void SetToposInactive()
    {
        textbox.text = "Mata al topo para jugar!";
        foreach (var topo in topos)
        {
            topo.gameObject.SetActive(false);
        }
    }

    void SetToposActive()
    {
        playTopos.gameObject.SetActive(false); // Assuming you still want to hide the play button
        textbox.text = "Score: 0";
        AssignToposColorsAndListeners();
        SetRandomTopoActive();
    }

    public void ButtonClicked(Button clickedButton)
    {
        // Depending on your logic, you can toggle the canvas visibility here
        // For example, if clicking any button should show/hide the canvas, you can:
        SetToposActive();
    }

    public void TopoClicked(Button clickedButton)
    {
        if (clickedButton == activeButton)
        {
            PlayWinSound();
            score += 1;
            textbox.text = "Score: " + score;
            SetRandomTopoActive(); // Set another button active
        }
        else
        {
            PlayLoseSound();
            score = 0; // Reset score
            textbox.text = "Score: " + score;
            SetRandomTopoActive(); // Set another button active
        }
    }

    void SetRandomTopoActive()
    {
        // Make all buttons invisible initially
        foreach (var btn in topos)
        {
            btn.gameObject.SetActive(false);
        }

        if (activeButton != null)
        {
            activeButton.GetComponent<Image>().color = new Color(255, 255, 255, 0); // Reset old active button color
        }

        int index = random.Next(topos.Count);
        activeButton = topos[index];
        activeButton.GetComponent<Image>().color = new Color(255, 255, 255, 0); // Set new active button color to transparent
        activeButton.gameObject.SetActive(true); // Only the active button is visible
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
