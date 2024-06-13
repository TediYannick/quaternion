using UnityEngine;

public class FreeCameraWithQuaternion : MonoBehaviour
{
    public float movementSpeed = 10f; 
    public float lookSpeed = 2f;
    public float sprintMultiplier = 2f; 
    public float maxLookAngle = 90f; 

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationY += mouseX;

     
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle);

        Quaternion rotationXQuat = Quaternion.AngleAxis(rotationX, Vector3.right);
        Quaternion rotationYQuat = Quaternion.AngleAxis(rotationY, Vector3.up);

        transform.localRotation = rotationYQuat * rotationXQuat;

        
        float moveSpeed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed *= sprintMultiplier; 
        }

        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveRight = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveUp = 0f;

        if (Input.GetKey(KeyCode.E))
        {
            moveUp += moveSpeed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.Q))
        {
            moveUp -= moveSpeed * Time.deltaTime; 
        }

        
        transform.Translate(moveRight, moveUp, moveForward);
    }
}
