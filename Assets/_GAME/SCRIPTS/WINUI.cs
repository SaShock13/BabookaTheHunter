using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WINUI : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    private void OnEnable()
    {
        BearHelth.OnBearDeath += WIN;
    }


    private void WIN()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
