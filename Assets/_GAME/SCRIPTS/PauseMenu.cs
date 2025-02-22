using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    private InputPlayer _input;
    private bool isPaused = false;

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject mainMenuPanel;

    [Inject]
    public void Construct(InputPlayer input)
    {
        _input = input;
    }

    public void Pause()
    {
        if (!isPaused)
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            Cursor.visible = true;
            _input.enabled = false;
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        _input.enabled = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    //todo чуть дорабоотать
    public void TOMenu()
    {
        Time.timeScale = 1f;
        isPaused= false;
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }
}
