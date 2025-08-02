using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCounter : MonoBehaviour
{
    public static FallCounter Instance;

    [SerializeField] private GameObject Player;
    
    [SerializeField] private float addTimer = 30f;

    private bool addReady = false;

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
        //Player.GetComponent<PlayerMovement>().eFalled.AddListener(TryToShow);
        //Player.GetComponentInChildren<PlayerAnimations>().eShowAdd.AddListener(ShowAdd);

        Invoke("UpdateAdd", addTimer);
    }

    private void TryToShow(int fallCount)
    {
        var PA = Player.GetComponentInChildren<PlayerAnimations>();

        if (addReady && fallCount > 0) 
        {
            PauseManager.Instance.blockPause = true;
            PauseManager.Instance.SetPlayerMove(false);
            //PA.Shoked = true;
        }
    }

    private void UpdateAdd()
    {
        addReady = true;
    }

    private void ShowAdd() 
    {
        var PA = Player.GetComponentInChildren<PlayerAnimations>();
        Debug.Log("ADD SHOWED!");
        addReady = false;
        Invoke("UpdateAdd", addTimer);
        //PA.ChangeAnim(AnimStates.IDLE);

        PauseManager.Instance.blockPause = false;
        PauseManager.Instance.SetPlayerMove(true);
        //PA.Shoked = false;
    }

}
