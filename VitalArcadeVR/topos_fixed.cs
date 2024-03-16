using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ToposFixed : MonoBehaviour
{
    public GameObject buttonParent; // Assign in inspector, parent object of buttons
    private List<Button> buttons = new List<Button>();
    private Button activeButton;
    public int score = 0;
    public TextMeshProUGUI textbox;
    private System.Random random = new System.Random();
    public List<SkinnedMeshRenderer> meshRenderers; // Assign in inspector, one for each button

    private Dictionary<Button, SkinnedMeshRenderer> buttonToRendererMap = new Dictionary<Button, SkinnedMeshRenderer>();

    public AudioClip loseSound; // Assign in inspector
    public AudioClip winSound; // Assign in inspector
    private AudioSource audioSource; // For playing sounds

    void Start()
    {
        buttons = buttonParent.GetComponentsInChildren<Button>().ToList();
        if (buttons.Count != meshRenderers.Count)
        {
            Debug.LogError("The number of buttons and mesh renderers must match.");
            return;
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            buttonToRendererMap[buttons[i]] = meshRenderers[i];
        }

        AssignButtonColorsAndListeners();
        SetRandomButtonActive();

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null) // Ensure there's an AudioSource component
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // void AssignButtonColorsAndListeners()
    // {
    //     foreach (var btn in buttons)
    //     {
    //         btn.onClick.AddListener(delegate { ButtonClicked(btn); });
    //         // Set button Image alpha to 0 (fully transparent) to "hide" it visually but keep it interactive
    //         Color btnColor = btn.GetComponent<Image>().color;
    //         btnColor.a = 0f; // Make transparent
    //         btn.GetComponent<Image>().color = btnColor;

    //         buttonToRendererMap[btn].enabled = false; // Initially hide all skinned mesh renderers
    //     }
    // }

    // void SetRandomButtonActive()
    // {
    //     if (activeButton != null)
    //     {
    //         // Make previous active button transparent
    //         Color inactiveBtnColor = activeButton.GetComponent<Image>().color;
    //         inactiveBtnColor.a = 0f; // Make transparent
    //         activeButton.GetComponent<Image>().color = inactiveBtnColor;

    //         buttonToRendererMap[activeButton].enabled = false; // Hide old active renderer
    //     }

    //     int index = random.Next(buttons.Count);
    //     activeButton = buttons[index];
    //     buttonToRendererMap[activeButton].enabled = true; // Show new active renderer

    //     // Make active button visible
    //     Color activeBtnColor = activeButton.GetComponent<Image>().color;
    //     activeBtnColor.a = 1f; // Make fully visible
    //     activeButton.GetComponent<Image>().color = activeBtnColor;
    // }

    void AssignButtonColorsAndListeners()
    {
        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(delegate { ButtonClicked(btn); });
            btn.GetComponent<Image>().color = Color.clear; // Make button transparent
            buttonToRendererMap[btn].enabled = false; // Initially hide all skinned mesh renderers
        }
    }

    void SetRandomButtonActive()
    {
        if (activeButton != null)
        {
            buttonToRendererMap[activeButton].enabled = false; // Hide old active renderer
        }

        int index = random.Next(buttons.Count);
        activeButton = buttons[index];
        buttonToRendererMap[activeButton].enabled = true; // Show new active renderer
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
        if (loseSound != null)
        {
            audioSource.clip = winSound;
            audioSource.Play();
        }
    }
}
