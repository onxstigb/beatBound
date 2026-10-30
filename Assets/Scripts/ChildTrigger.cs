using Unity.VisualScripting;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<ShootingEnemy>().ChildTriggerEnter(other);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<ShootingEnemy>().ChildTriggerExit(other);
        }
        
    }
}
