using UnityEngine;

public class RotateWithQuaternion : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; 
    public float angle = 45f; 

    void Update()
    {
        float deltaAngle = angle * Time.deltaTime; 
        QuaternionCustom q = new QuaternionCustom(Mathf.Cos(deltaAngle * Mathf.Deg2Rad / 2), 
                                                   Mathf.Sin(deltaAngle * Mathf.Deg2Rad / 2) * rotationAxis.x, 
                                                   Mathf.Sin(deltaAngle * Mathf.Deg2Rad / 2) * rotationAxis.y, 
                                                   Mathf.Sin(deltaAngle * Mathf.Deg2Rad / 2) * rotationAxis.z);
        Matrix4x4 rotationMatrix = q.ToMatrix();
        transform.rotation = rotationMatrix.rotation * transform.rotation; 
    }
}
