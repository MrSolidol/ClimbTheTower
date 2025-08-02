using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [SerializeField] private PlayerMovement PM;

    public bool isPaused = false;

    public bool blockPause = false;

    public UnityEvent<bool> ePaused;

    private float timeSummator = 0f;

    public int secondsSummator = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Load();
    }

    public void StopGame()
    {
        if (blockPause) { return; }
        Time.timeScale = 0f;
        isPaused = true;
        ePaused.Invoke(isPaused);
    }

    public void ResumeGame()
    {
        if (blockPause) { return; }
        Time.timeScale = 1f;
        isPaused = false;
        ePaused.Invoke(isPaused);
    }

    public void SetPlayerMove(bool value) 
    {
        PM.enabled = value;
    }

    public void Update()
    {
        if (!isPaused) 
        {
            TimeCount();
        }
    }

    private void TimeCount() 
    {
        timeSummator += Time.deltaTime;
        if (timeSummator > 3.4f * 1000000000000000f) 
        {
            SecondsCount();
        }
    }

    private void SecondsCount() 
    {
        secondsSummator += (int)timeSummator;
        timeSummator = 0;
    }

    public void Load() 
    {
        //ProgressSave.Instance.Load();
        //secondsSummator = ProgressSave.Instance.playTime;
    }

    public void Save() 
    {
        //SecondsCount();
        //ProgressSave.Instance.playTime = secondsSummator;
        //ProgressSave.Instance.Save();
    }
}
