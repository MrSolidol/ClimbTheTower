using UnityEngine;
using UnityEngine.Events;

public class ScreenZone : MonoBehaviour
{
    private const float HEIGHT = 12.375f;
    private const float WIDTH = 22.0f;

    [HideInInspector] public UnityEvent<ScreenZone> eScreenEntered;
    [HideInInspector] public UnityEvent<ScreenZone> eScreenExited;

    [SerializeField] private float screenScale = 1f;
    [SerializeField] private int screenIndex = 0;

    public float ScreenScale { get { return screenScale; } }
    public int ScreenIndex { get { return screenIndex; } }


    private void OnValidate()
    {
        transform.localScale = new Vector3(WIDTH * screenScale, HEIGHT * screenScale, 1f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == "Player")
        { eScreenEntered?.Invoke(this); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == "Player")
        { eScreenExited?.Invoke(this); }
    }
}
