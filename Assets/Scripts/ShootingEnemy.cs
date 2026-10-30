using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    private Renderer enemyRenderer;

    private System.Collections.Generic.List<GameObject> bullets = new System.Collections.Generic.List<GameObject>();

    private Collider enemyCollider;

    private Vector3 originalScale;

    private bool PlayerInRange;

    public float pulseAmount = 1.15f;

    public float pulseSpeed = 5f;

    public bool startActive = false;

    public float bulletSpeed = 8f;

    public Transform player;

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

    public void ChildTriggerEnter(Collider other)
    {
        PlayerInRange = true;
    }

    public void ChildTriggerExit(Collider other)
    {
        PlayerInRange = false;
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
            patternBeat == 1 || patternBeat == 3;

        // Reverse the pattern if this platform starts inactive
        if (!startActive)
        {
            shouldBeActive = !shouldBeActive;
        }

        SetEnemyState(shouldBeActive);

        // Pulse on every beat
        transform.localScale = originalScale * pulseAmount;
    }

    void SetEnemyState(bool active)
    {
        enemyCollider.enabled = active;

        if (active && PlayerInRange)
        {
            
            enemyRenderer.material.color = Color.red;
            fire();
        }
        else
        {
            enemyRenderer.material.color = Color.gray;
        }
    }



    void fire()
    {
        if(bullets.Count == 10)
        {
            GameObject target = bullets[0];
            bullets.RemoveAt(0);
            Destroy(target);
        }
        
        
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bullet.layer = LayerMask.NameToLayer("Bullets");
        bullet.transform.position = transform.position;
        bullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        bullet.tag = "EnemyBullet";
        bullet.AddComponent<Rigidbody>();
        bullet.GetComponent<Rigidbody>().useGravity = false;
        bullet.GetComponent<Rigidbody>().linearVelocity = (player.position - transform.position).normalized * bulletSpeed;
        bullets.Add(bullet);

    }
}
