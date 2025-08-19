using UnityEngine;

public class DropdownSorting : MonoBehaviour
{
    [SerializeField] private string _SortingLayerName = "UI";

    void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
            canvas.sortingLayerName = _SortingLayerName;
    }
}
