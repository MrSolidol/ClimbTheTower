using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformation : MonoBehaviour
{
    [SerializeField] AnimationCurve aPosChange;
    [SerializeField] AnimationCurve aSizChange;

    static float posTime = .25f;
    static float sizTime = .5f;
    static CameraTransformation camTransform;

    static private float frames = .01f;
    static private List<CameraPanel> panList = new List<CameraPanel>();

    private void Awake()
    {
        CameraTransformation.camTransform = Camera.main.transform.GetComponent<CameraTransformation>();
    }

    private void Start()
    {
        //ProgressSave.Instance.Load();
        //Camera.main.orthographicSize = ProgressSave.Instance.cameraSize;
        //camTransform.transform.position = ProgressSave.Instance.cameraPosition;
    }

    public static void EnterPanel(CameraPanel pan) 
    {
        panList.Add(pan);
    }

    public static void ExitPanel(CameraPanel pan) 
    {
        panList.Remove(pan);
        if (panList.Count > 0) 
        {
            ChangePos();
            ChangeSize();
        }
    }

    public static void SaveParameters() 
    {
        //ProgressSave.Instance.cameraPosition = camTransform.transform.position;
        //ProgressSave.Instance.cameraSize = Camera.main.orthographicSize;
        //ProgressSave.Instance.Save();
    }

    private static void ChangePos() 
    {
        camTransform.StartCoroutine(CPose(camTransform.transform.position, panList[0].transform.position));
    }

    private static void ChangeSize()
    {
        camTransform.StartCoroutine(CSize(Camera.main.orthographicSize, panList[0].pSize));
    }

    private static IEnumerator CPose(Vector2 point1, Vector2 point2) 
    {
        for (float i = 0; i < posTime; i += frames) 
        {
            
            Vector2 res = Vector2.Lerp(point1, point2, camTransform.aPosChange.Evaluate(i/posTime));
            camTransform.transform.position = new Vector3(res.x, res.y, camTransform.transform.position.z);
            yield return new WaitForSeconds(frames);
        }
        SaveParameters();
    }

    private static IEnumerator CSize(float start, float end)
    {
        for (float i = 0; i < sizTime; i += frames)
        {
            Camera.main.orthographicSize = Mathf.Lerp(start, end, camTransform.aSizChange.Evaluate(i / sizTime));
            yield return new WaitForSeconds(frames);
        }
        SaveParameters();
    }
}
