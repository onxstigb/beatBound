using UnityEngine;

public class RhythmPlatform : MonoBehaviour
{
    private Renderer platformRenderer;
    private Collider platformCollider;

    private Vector3 originalScale;

    public float pulseAmount = 1.15f;
    public float pulseSpeed = 8f;

    // Turn this off for platforms that should start inactive
    public bool startActive = true;

    private int beatCount = 0;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();

        originalScale = transform.localScale;

        BeatManager.Instance.OnBeat += ReactToBeat;

        // Set the starting state
        SetPlatformState(startActive);
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
        beatCount++;

        int patternBeat = beatCount % 4;

        // Normal pattern:
        // Beat 1 = active
        // Beat 2 = active
        // Beat 3 = inactive
        // Beat 4 = inactive

        bool shouldBeActive =
            patternBeat == 1 || patternBeat == 2;

        // Reverse the pattern if this platform starts inactive
        if (!startActive)
        {
            shouldBeActive = !shouldBeActive;
        }

        SetPlatformState(shouldBeActive);

        // Pulse on every beat
        transform.localScale = originalScale * pulseAmount;
    }

    void SetPlatformState(bool active)
    {
        platformCollider.enabled = active;

        if (active)
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