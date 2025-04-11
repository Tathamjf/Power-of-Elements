using UnityEngine;
using TMPro;

public class SistemaDeColeta : MonoBehaviour
{

    private int Cristal = 0;

    public TextMeshProUGUI cristaisText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Cristal")
        {
            Cristal++;
            cristaisText.text = "Cristais: " + Cristal.ToString();  
            Debug.Log(Cristal);
            Destroy(other.gameObject);
        }
    }


}
