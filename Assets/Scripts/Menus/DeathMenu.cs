using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    private void Start()
    {
        // Check if the restart button is assigned in the inspector
        if (m_RestartButton != null)
        {
            // Add a listener to the restart button to handle clicks
            m_RestartButton.onClick.AddListener(RestartApplication);
        }
        else
        {
            // Log an error if the restart button is not assigned
            Debug.LogError("m_RestartButton is not assigned in the inspector");
        }
    }

    // Method to enable the death screen GUI
    public void EnableGUI()
    {
        m_DeathScreenGui.SetActive(true);  // Set the death screen GUI to active
        Time.timeScale = 0.0f;              // Pause the game by setting time scale to 0
    }

    // Method to restart the application
    private void RestartApplication()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Set the time scale to normal (unpause)
        Time.timeScale = 1.0f;
    }

    [Header("UI Elements")]
    [SerializeField] private GameObject m_DeathScreenGui;  // Reference to the death screen GUI GameObject
    [SerializeField] private Button m_RestartButton;       // Reference to the restart button

}
