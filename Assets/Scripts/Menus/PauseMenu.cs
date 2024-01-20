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
        //Check if pause button is assigned or not
        if (m_PauseButton != null)
        {
            m_PauseButton.onClick.AddListener(OnPauseButtonPressed);
        }
        else { Debug.Log("Pause button is not assigned in the inspector"); }
        //Check if ui resume button is assigned or not
        if (m_ResumeButton != null)
        {
            m_ResumeButton.onClick.AddListener(OnResumeButtonPressed);
        }
        else { Debug.Log("Resume button is not assigned in the inspector"); }
        //Check if ui restart button is assigned or not
        if (m_RestartButton != null)
        {
            m_RestartButton.onClick.AddListener(OnRestartButtonPressed);
        }
        else { Debug.Log("Restart button is not assigned in the inspector"); }
        //Check if ui quit button is assigned or not
        if (m_QuitButton != null)
        {
            m_QuitButton.onClick.AddListener(OnQuitButtonPressed);
        }
        else { Debug.Log("Quit button is not assigned in the inspector"); }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Pause button callback
    private void OnPauseButtonPressed()
    {
        // Pause the game
        PauseApplication();
      
    }

    //resume button callback
    private void OnResumeButtonPressed()
    {
        ResumeApplication();
    }

    private void OnRestartButtonPressed()
    {
        RestartApplication();
    }

    private void OnQuitButtonPressed()
    {
        QuitApplication();
    }
    private void QuitApplication()
    {
        // Quit the application (only works in standalone builds, not in the Unity Editor)
#if UNITY_STANDALONE
            Application.Quit();
#endif
    }
    private void ResumeApplication()
    {
        m_PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        //Game Is currently paused
        m_GamePaused = false;
    }

    private void PauseApplication()
    {
        m_PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        //Game Is currently paused
        m_GamePaused = true;
    }

    private void RestartApplication()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
    public static bool m_GamePaused = false;
    private Image m_ResumeButtonImage;
    [SerializeField] private Button m_PauseButton;
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private Button m_QuitButton;
    [SerializeField] private GameObject m_PauseMenuUI;
}
