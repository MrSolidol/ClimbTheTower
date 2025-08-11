using UnityEngine;

[CreateAssetMenu(fileName = "DialogeInfo", menuName = "Gameplay/Dialoge Info")]
public class DialogeInfo : ScriptableObject
{
    [SerializeField] private string npcName;
    [SerializeField] private string[] dialogeKeys;


    public string NpcName { get { return npcName; } }
    public string[] DialogeKeys { get { return dialogeKeys; } }
}
