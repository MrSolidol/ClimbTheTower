using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject Button;

    private void Start()
    {
        PauseManager.Instance.ePaused.AddListener(ChangePause);
        ChangePause(false);
    }

    private void ChangePause(bool show) 
    {
        PausePanel.SetActive(show);
        Button.SetActive(!show);
    }
}
