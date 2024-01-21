using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // Assign click listeners to UI buttons
        AssignButtonClickListeners();
    }

    // Update is called once per frame
    void Update()
    {
        // Empty update for now
    }

    // Assign click listeners to UI buttons
    private void AssignButtonClickListeners()
    {
        AssignButtonListener(m_PauseButton, OnPauseButtonPressed, "Pause");
        AssignButtonListener(m_ResumeButton, OnResumeButtonPressed, "Resume");
        AssignButtonListener(m_RestartButton, OnRestartButtonPressed, "Restart");
        AssignButtonListener(m_EnableFog, OnFogButtonPressed, "Toggle Fog");
    }

    // Helper method to assign button click listeners
    private void AssignButtonListener(Button button, UnityEngine.Events.UnityAction action, string buttonName)
    {
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
        else
        {
            Debug.LogError(buttonName + " button is not assigned in the inspector");
        }
    }

    // Callback for the fog button
    private void OnFogButtonPressed()
    {
        // Toggle the state of fog
        m_IsEnabled = !m_IsEnabled;

        // Update the visibility of the fog container
        if (m_FogContainer != null)
        {
            m_FogContainer.SetActive(m_IsEnabled);
        }
        else
        {
            Debug.LogError("Fog container is not assigned in the inspector");
        }
    }

    // Callback for the pause button
    private void OnPauseButtonPressed()
    {
        // Pause the game
        PauseApplication();
    }

    // Callback for the resume button
    private void OnResumeButtonPressed()
    {
        // Resume the game
        ResumeApplication();
    }

    // Callback for the restart button
    private void OnRestartButtonPressed()
    {
        // Restart the game
        RestartApplication();
    }

    // Method to resume the application
    private void ResumeApplication()
    {
        // Deactivate the pause menu UI
        m_PauseMenuUI.SetActive(false);

        // Set the time scale to normal (unpause)
        Time.timeScale = 1.0f;

        // Game is currently not paused
        m_GamePaused = false;
    }

    // Method to pause the application
    private void PauseApplication()
    {
        // Activate the pause menu UI
        m_PauseMenuUI.SetActive(true);

        // Set the time scale to 0 (pause)
        Time.timeScale = 0.0f;

        // Game is currently paused
        m_GamePaused = true;
    }

    // Method to restart the application
    private void RestartApplication()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Set the time scale to normal (unpause)
        Time.timeScale = 1.0f;
    }
    // Variables
    public static bool m_GamePaused = false;
    public static bool m_IsEnabled = false;
    private Image m_ResumeButtonImage;

    // UI Elements
    [Header("UI Elements")]
    [SerializeField] private Button m_PauseButton;
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private GameObject m_PauseMenuUI;
    [SerializeField] private GameObject m_FogContainer;
    [SerializeField] private Button m_EnableFog;

}
