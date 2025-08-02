using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressSave : MonoBehaviour
{
    private static ProgressSave _instance;
    public static ProgressSave Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ProgressSave>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(ProgressSave).Name);
                    _instance = singleton.AddComponent<ProgressSave>();
                }
            }
            return _instance;
        }
    }

    private const string PLAYER_POS_X_KEY = "PlayerPosX";
    private const string PLAYER_POS_Y_KEY = "PlayerPosY";
    private const string PLAYER_POS_Z_KEY = "PlayerPosZ";

    private const string CAMERA_POS_X_KEY = "CameraPosX";
    private const string CAMERA_POS_Y_KEY = "CameraPosY";
    private const string CAMERA_POS_Z_KEY = "CameraPosZ";

    private const string CAMERA_SIZE_KEY = "CameraSize";

    private const string MUSIC_VALUE_KEY = "MusicValue";
    private const string SOUND_VALUE_KEY = "SoundValue";

    private const string LOCALIZATION_KEY = "LocalizationID";



    public Vector3 playerPosition;
    public Vector3 cameraPosition;
    public float cameraSize;

    public float musicValue;
    public float soundValue;

    public int localizationID;

    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var duplicates = FindObjectsOfType<ProgressSave>();
        foreach (var duplicate in duplicates)
        {
            if (duplicate != Instance)
            {
                Destroy(duplicate.gameObject);
            }
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void Save()
    {
        PlayerPrefs.SetFloat(PLAYER_POS_X_KEY, playerPosition.x);
        PlayerPrefs.SetFloat(PLAYER_POS_Y_KEY, playerPosition.y);
        PlayerPrefs.SetFloat(PLAYER_POS_Z_KEY, playerPosition.z);

        PlayerPrefs.SetFloat(CAMERA_POS_X_KEY, cameraPosition.x);
        PlayerPrefs.SetFloat(CAMERA_POS_Y_KEY, cameraPosition.y);
        PlayerPrefs.SetFloat(CAMERA_POS_Z_KEY, cameraPosition.z);

        PlayerPrefs.SetFloat(CAMERA_SIZE_KEY, cameraSize);

        PlayerPrefs.SetFloat(MUSIC_VALUE_KEY, musicValue);
        PlayerPrefs.SetFloat(SOUND_VALUE_KEY, soundValue);

        PlayerPrefs.SetInt(LOCALIZATION_KEY, localizationID);

        PlayerPrefs.Save();
    }
    public void Load()
    {
        float playerX = PlayerPrefs.GetFloat(PLAYER_POS_X_KEY, -7.5f);
        float playerY = PlayerPrefs.GetFloat(PLAYER_POS_Y_KEY, -4.45f);
        float playerZ = PlayerPrefs.GetFloat(PLAYER_POS_Z_KEY, 0f);
        playerPosition = new Vector3(playerX, playerY, playerZ);

        float cameraX = PlayerPrefs.GetFloat(CAMERA_POS_X_KEY, 0f);
        float cameraY = PlayerPrefs.GetFloat(CAMERA_POS_Y_KEY, 0f);
        float cameraZ = PlayerPrefs.GetFloat(CAMERA_POS_Z_KEY, -10f);

        cameraPosition = new Vector3(cameraX, cameraY, cameraZ);

        cameraSize = PlayerPrefs.GetFloat(CAMERA_SIZE_KEY, 6f);

        musicValue = PlayerPrefs.GetFloat(MUSIC_VALUE_KEY, 0.4f);
        soundValue = PlayerPrefs.GetFloat(SOUND_VALUE_KEY, 0.75f);

        localizationID = PlayerPrefs.GetInt(LOCALIZATION_KEY, 0);
    }
    public void Reset()
    {
        PlayerPrefs.SetFloat(PLAYER_POS_X_KEY, -7.5f);
        PlayerPrefs.SetFloat(PLAYER_POS_Y_KEY, -4.45f);
        PlayerPrefs.SetFloat(PLAYER_POS_Z_KEY, 0f);

        PlayerPrefs.SetFloat(CAMERA_POS_X_KEY, 0f);
        PlayerPrefs.SetFloat(CAMERA_POS_Y_KEY, 0f);
        PlayerPrefs.SetFloat(CAMERA_POS_Z_KEY, -10f);

        PlayerPrefs.SetFloat(CAMERA_SIZE_KEY, 6f);

        PlayerPrefs.SetFloat(MUSIC_VALUE_KEY, musicValue);
        PlayerPrefs.SetFloat(SOUND_VALUE_KEY, soundValue);

        PlayerPrefs.SetInt(LOCALIZATION_KEY, localizationID);

        PlayerPrefs.Save();
    }
}