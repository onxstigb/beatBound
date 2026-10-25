using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    private Renderer enemyRenderer;

    private System.Collections.Generic.List<GameObject> bullets = new System.Collections.Generic.List<GameObject>();

    private Collider enemyCollider;

    private Vector3 originalScale;

    public float pulseAmount = 1.15f;

    public float pulseSpeed = 5f;

    public bool startActive = false;

    private int beatCount = 0;

    
    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyCollider = GetComponent<Collider>();

        originalScale = transform.localScale;

        BeatManager.Instance.OnBeat += ReactToBeat;
    }

    // Update is called once per frame
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

        // Pulse on every beat
        transform.localScale = originalScale * pulseAmount;
    }

    void fire()
    {
        if(bullets.Count == 5)
        {
            bullets[bullets.Count - 1].GetComponent<Bullet>().DestroyBullet();
        }
    }
}
