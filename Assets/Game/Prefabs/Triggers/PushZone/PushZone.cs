using UnityEngine;

public class PushZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            var oldPos = collision.gameObject.transform.position;
            float yPos = transform.position.y + transform.localScale.y / 2 + 1f;

            collision.gameObject.transform.position = new Vector3(oldPos.x, yPos, oldPos.z);
        }
    }
}
