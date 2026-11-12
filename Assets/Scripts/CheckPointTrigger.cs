using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform respawnPoint;
    public Transform platform;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            respawnPoint.position = platform.position;
        }
    }
}
