using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(0f, 6f, -8f);

    public float followSpeed = 8f;

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        //transform.LookAt(player);
    }
}
