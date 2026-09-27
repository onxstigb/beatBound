using UnityEngine;

public class RhythmPlatform : MonoBehaviour
{
    private Renderer platformRenderer;
    private Collider platformCollider;

    private bool isActive = true;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();

        BeatManager.Instance.OnBeat += ReactToBeat;

        platformRenderer.material.color = Color.green;
    }

    void ReactToBeat()
    {
        isActive = !isActive;

        platformCollider.enabled = isActive;

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