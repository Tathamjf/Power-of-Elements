using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private int PowerUPVelocidade = 0;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PowerUp")
        {
            PowerUPVelocidade++;
            
            Debug.Log("Power Up coletado!");
            Destroy(other.gameObject);
        }
    }
}
