using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СontourAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material spriteMaterial;
    private Vector3 upDir = Vector2.up;
    private Vector3 offset;

    Vector3 RotateVector2D(Vector2 vector, float angleDegrees)
    {
        float angleRad = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);

        float x = vector.x * cos - vector.y * sin;
        float y = vector.x * sin + vector.y * cos;

        return new Vector3(x, y, 1f);
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteMaterial = spriteRenderer.material;
            
        offset = transform.position - transform.parent.position;
    }


    public void SetNewContour(float _value, float _angle) 
    {
        //SetFill(1 - _value);
        spriteRenderer.color = new Color(1, 1, 1, _value * 0.8f);
        if (_value == 0) { return; }
        transform.rotation = Quaternion.Euler(0f, 0f, _angle + 90);
        transform.position = transform.parent.position + RotateVector2D(offset * Mathf.Pow(_value, 0.2f), _angle + 90);
        transform.localScale = new Vector3(Mathf.Pow(_value, 0.5f), Mathf.Pow(_value, 0.5f), Mathf.Pow(_value, 0.5f));
    }


    public void SetFill(float _value)
    {
        spriteMaterial.SetVector("_ClipRect", new Vector4(0, 0, 1, 1 - _value));
    }
}
