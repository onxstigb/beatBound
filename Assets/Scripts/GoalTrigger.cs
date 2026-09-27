using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject levelCompleteText;

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

            Debug.Log("LEVEL COMPLETE!");
        }
    }
}