using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject levelCompleteText;
    public AudioSource musicSource;

    private bool levelComplete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelComplete)
        {
            levelComplete = true;

            if (levelCompleteText != null)
            {
                levelCompleteText.SetActive(true);
            }

            if (musicSource != null)
            {
                musicSource.Stop();
            }

            Debug.Log("LEVEL COMPLETE!");
        }
    }
}