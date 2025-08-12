using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

public class PlayerSave : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private bool UseSave = true;

    [SerializeField] private bool ResetSave = false;

    [Inject] private DataService dataService;

    private PlayerData playerData;


    private void Awake()
    {
        if (ResetSave)
            dataService.Save(new PlayerData());
        if (UseSave)
            Initialization();
    }

    private void OnEnable()
    {
        if (UseSave)
            playerMovement.eFloorContact.AddListener(Save);
    }

    private void OnDisable()
    {
        if (UseSave)
            playerMovement.eFloorContact.RemoveListener(Save);
    }


    private void Initialization() 
    {
        playerData = new PlayerData();
        dataService.Load(out playerData);
        transform.position = playerData.PlayerPosition.ToVector3();
    }

    private void Save() 
    {
        playerData.PlayerPosition = new Vector3Surrogate(transform.position);
        dataService.Save(playerData);
    }
}
