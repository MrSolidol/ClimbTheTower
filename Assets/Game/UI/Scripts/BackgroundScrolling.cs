using UnityEngine;

[System.Serializable]
public class BackgroundElement
{
    public MeshRenderer background;
    [Range(-5, 5)] public float XscrollSpeed;
    [Range(-5, 5)] public float YscrollSpeed;
    [HideInInspector] public Material backgroundMaterial;
}

public class BackgroundScrolling : MonoBehaviour
{
    [SerializeField] private BackgroundElement[] backgroundElements;

    private void Start()
    {
        foreach (BackgroundElement element in backgroundElements)
        {
            element.backgroundMaterial = element.background.material;
        }
        Invoke("ChangeXVec", Random.Range(2.5f, 10f));
    }

    private void Update()
    {
        foreach (BackgroundElement element in backgroundElements)
        {
            float _vecX = element.backgroundMaterial.mainTextureOffset.x + Time.deltaTime * element.XscrollSpeed;
            float _vecY = element.backgroundMaterial.mainTextureOffset.y + Time.deltaTime * element.YscrollSpeed;
            element.backgroundMaterial.mainTextureOffset = new Vector2(_vecX, _vecY);
        }
    }

    private void ChangeXVec() 
    {
        foreach (BackgroundElement element in backgroundElements)
        {
            element.XscrollSpeed *= -1;
        }

        Invoke("ChangeXVec", Random.Range(1f, 10f));
    }
}
