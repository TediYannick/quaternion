using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform orbitCenter; 
    public Vector3 orbitAxis = Vector3.up; 
    public float orbitSpeed = 10f;
    public float orbitRadius = 5f;

    void Start()
    {
        Vector3 offset = new Vector3(orbitRadius, 0, 0); 
        transform.position = orbitCenter.position + offset;
    }

    void Update()
    {
        if (orbitCenter == null) return;

        float angle = orbitSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.AngleAxis(angle, orbitAxis);

        Vector3 offset = transform.position - orbitCenter.position;
        offset = rotation * offset;
        transform.position = orbitCenter.position + offset;

    }
}
