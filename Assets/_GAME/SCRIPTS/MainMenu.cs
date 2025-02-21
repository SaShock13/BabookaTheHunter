using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    private InputPlayer _input;


    [Inject]
    public void Construct(InputPlayer input)
    {
        _input = input;
    }


    private void Start()
    {
        _input.enabled = false;
        mainMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        _input.enabled = true;
        Cursor.visible = false;
        Time.timeScale = 1f;

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Settings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        Debug.Log($"Menu Settings {this}");
    }
    public void BackToMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

}
