using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class DisappearPlatform : MonoBehaviour
{
    [SerializeField] float timeToDisSpawn = 5f;
    [SerializeField] float timeToSpawn = 10f;

    public UnityEvent eDisSpawn;
    public UnityEvent eSpawn;

    private SpriteRenderer sprite;
    private Collider2D collider;

    private GameObject player;

    private void Awake()
    {     
        collider = gameObject.GetComponent<Collider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        eDisSpawn.AddListener(delegate () { Invoke("DisSpawn", timeToDisSpawn); });
        eSpawn.AddListener(delegate () { Invoke("Spawn", timeToSpawn); });

    //    player = transform.parent.transform.Find("PhysObject").gameObject;
    }

    private void DisSpawn() 
    {
        collider.enabled = false;
        sprite.enabled = false;
        eSpawn.Invoke();
    }

    private void Spawn() 
    {
        collider.enabled = true;
        sprite.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 _vecContact = collision.contacts[0].normal;
        float _degContact = Vector2.Angle(Vector2.up, _vecContact);

        if (collision.gameObject.CompareTag("Player") && (_degContact == 180)) 
        {
            eDisSpawn.Invoke();
        }
    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        GameObject gm = collision.gameObject;
//
//        if (gm.CompareTag("Player")) 
//        {
//            Invoke("LetPlayerFall", 0f);
//        }
//    }

//    private void LetPlayerFall()
//    {
//        eSpawn.RemoveListener(player.GetComponent<PlayerMovement>().DisPlatformControl);
//    }

}

/// TODO:
/// Говнокодинг? Я пьяный :D
/// Нахождение игрока на сцене происходит очень спорным способом
