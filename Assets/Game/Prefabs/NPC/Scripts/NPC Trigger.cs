using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] private DialogeWriter dialoge;
    [SerializeField] private float restTime = 15f;
    private bool avaliable = true;

    private void Start()
    {
        dialoge.dialogWrited.AddListener(delegate () { Invoke("ResetAvaliable", restTime); });
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void ResetAvaliable()
    {
        avaliable = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & avaliable)
        {
            dialoge.NextDialoge();
            avaliable = false;
        }
    }
}
