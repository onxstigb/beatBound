using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public float rotationSpeed = 10f;

    public Transform respawnPoint;

    public TextMeshProUGUI respawnText;
    public float respawnMessageTime = 1f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                input.y += 1;

            if (Keyboard.current.sKey.isPressed)
                input.y -= 1;

            if (Keyboard.current.dKey.isPressed)
                input.x += 1;

            if (Keyboard.current.aKey.isPressed)
                input.x -= 1;
        }

        Vector3 movement = new Vector3(input.x, 0f, input.y).normalized;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = movement.x * moveSpeed;
        velocity.z = movement.z * moveSpeed;
        rb.linearVelocity = velocity;

        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }

        if (transform.position.y < -10f)
        {
            Respawn();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Respawn();
            collision.gameObject.SetActive(false);

        }
    }

void Respawn()
{
    if (respawnPoint == null)
        return;

    rb.linearVelocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    transform.position = respawnPoint.position;
        
    if (respawnText != null)
    {
        respawnText.gameObject.SetActive(true);

        CancelInvoke(nameof(HideRespawnMessage));
        Invoke(nameof(HideRespawnMessage), respawnMessageTime);
    }
}

void HideRespawnMessage()
{
    if (respawnText != null)
    {
        respawnText.gameObject.SetActive(false);
    }
}
}
