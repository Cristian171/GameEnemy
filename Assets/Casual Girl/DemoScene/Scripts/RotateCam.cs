using UnityEngine;

public class RotateCam : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 4.0f;

    private float rotationX;
    private float rotationY;

    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private float distanceFromTarget = 3.0f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField]
    private float smoothTime = 0.2f;

    [SerializeField]
    private Vector2 rotationXMinMax = new Vector2(0, 40);

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationY += mouseX;

        // Apply Clamping for X Rotation

        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        // Apply Damping between Rotation changes

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

        // Substract forward vector of the GameObject to point its forward vector to the target

        transform.position = targetObject.position - transform.forward * distanceFromTarget;
    }
}
