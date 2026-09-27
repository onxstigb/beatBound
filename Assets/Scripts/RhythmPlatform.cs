using UnityEngine;

public class RhythmPlatform : MonoBehaviour
{
    private Renderer platformRenderer;
    private Collider platformCollider;

    private Vector3 originalScale;

    private bool isActive = true;

    public float pulseAmount = 1.15f;
    public float pulseSpeed = 8f;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();

        originalScale = transform.localScale;

        BeatManager.Instance.OnBeat += ReactToBeat;

        platformRenderer.material.color = Color.green;
    }

    void Update()
    {
        // Smoothly return to normal size
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            originalScale,
            pulseSpeed * Time.deltaTime
        );
    }

    void ReactToBeat()
    {
        isActive = !isActive;

        platformCollider.enabled = isActive;

        // Pulse on every beat
        transform.localScale = originalScale * pulseAmount;

        if (isActive)
        {
            platformRenderer.material.color = Color.green;
        }
        else
        {
            platformRenderer.material.color = Color.red;
        }
    }

    void OnDestroy()
    {
        if (BeatManager.Instance != null)
        {
            BeatManager.Instance.OnBeat -= ReactToBeat;
        }
    }
}