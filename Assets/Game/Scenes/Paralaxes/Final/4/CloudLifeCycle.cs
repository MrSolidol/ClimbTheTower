using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLifeCycle : MonoBehaviour
{
    private const float X_FRAMES = 14;

    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private SpriteRenderer cloudSprite;

    [SerializeField] private float movementSpeed;


    private void Awake()
    {
        cloudSprite.sprite = spriteList[Random.Range(0, spriteList.Count)];
        movementSpeed += Random.Range(0.0f, 0.2f);
    }

    private void Update()
    {
        transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        if (transform.position.x >= X_FRAMES) 
        {
            transform.position = new Vector3(-X_FRAMES, transform.position.y, transform.position.z) ;
        }
    }
}
