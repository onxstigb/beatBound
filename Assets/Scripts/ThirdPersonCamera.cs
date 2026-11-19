using UnityEngine;
using UnityEngine.InputSystem;
public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 6f, -8f);
    public float followSpeed = 8f;

    [Header("Vertical Look")]
    public float mouseSensitivity = 50f;
    public float minPitch = -60f; // Look up limit
    public float maxPitch = 75f;  // Look down limit

    private float currentPitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Start from whatever X angle you set in the Unity Inspector
        currentPitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // Keep your existing position follow logic
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        // Read vertical mouse movement and clamp the tilt angle
        float mouseY = 0f;
        if (Mouse.current != null)
        {
            mouseY = Mouse.current.delta.ReadValue().y * mouseSensitivity/200f;
        }

        currentPitch = Mathf.Clamp(currentPitch - mouseY, minPitch, maxPitch);

        // Tilt only the X-axis while preserving Y and Z rotation
        Vector3 currentEuler = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(currentPitch, currentEuler.y, currentEuler.z);
}
}