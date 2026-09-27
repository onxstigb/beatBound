using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm = 120f;

    public static BeatManager Instance;

    public delegate void BeatAction();
    public event BeatAction OnBeat;

    private float timer;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float beatLength = 60f / bpm;

        if (timer >= beatLength)
        {
            timer -= beatLength;

            OnBeat?.Invoke();

            Debug.Log("BEAT!");
        }
    }
}
