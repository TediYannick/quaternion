using UnityEngine;

public class RotateWithMatrix : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; 
    public float angle = 45f; 
    void Update()
    {
        float deltaAngle = angle * Time.deltaTime; 
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(deltaAngle, rotationAxis));
        transform.rotation = rotationMatrix.rotation * transform.rotation;
    }
}
 