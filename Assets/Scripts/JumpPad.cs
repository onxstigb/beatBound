using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounceForce = 15f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            // Reset vertical velocity for a consistent bounce height
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Apply an instant upward force relative to the pad's orientation
            rb.AddForce(transform.up * bounceForce, ForceMode.Impulse);
        }
    }
}