using UnityEngine;

[System.Serializable]
public class Vector3Surrogate
{
    public float x, y, z;
    public Vector3Surrogate(Vector3 v) { x = v.x; y = v.y; z = v.z; }
    public Vector3Surrogate(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    public Vector3 ToVector3() => new Vector3(x, y, z);
}