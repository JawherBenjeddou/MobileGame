using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            m_ResumeButton.onClick.AddListener(onResumeButtonPressed);
        }
        else { Debug.Log("Pause button is not assigned in the inspector"); }
    
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
    private void onResumeButtonPressed()
    {
        ResumeApplication();
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
    public static bool m_GamePaused = false;
    private Image m_ResumeButtonImage;
    [SerializeField] private Button m_PauseButton;
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private GameObject m_PauseMenuUI;
}
