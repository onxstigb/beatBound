using UnityEngine;

public class RhythmPlatform : MonoBehaviour
{
    private Renderer platformRenderer;
    private Collider platformCollider;

    private Vector3 originalScale;

    public float pulseAmount = 1.15f;
    public float pulseSpeed = 8f;

    private int beatCount = 0;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();

        originalScale = transform.localScale;

        BeatManager.Instance.OnBeat += ReactToBeat;

        // Start active
        SetPlatformState(true);
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

        // Repeat every 4 beats
        int patternBeat = beatCount % 4;

        // Beats 1 and 2 = active
        // Beats 3 and 4 = inactive
        bool shouldBeActive = patternBeat == 1 || patternBeat == 2;

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