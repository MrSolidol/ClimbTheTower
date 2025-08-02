using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraPanel : MonoBehaviour
{
    public float pSize = 5f;
    private float pScale = 5f/17.77777f;

    private void Start()
    {
        pSize = transform.localScale.x * pScale;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            CameraTransformation.EnterPanel(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraTransformation.ExitPanel(this);
        }
    }
}
