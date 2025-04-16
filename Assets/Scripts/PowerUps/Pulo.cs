using UnityEngine;

public class Pulo : MonoBehaviour
{
    private Movimento_Player move;
    public GameObject player;

    void Start()
    {
        Debug.Log("PowerUp carregado.");
        move = player.GetComponent<Movimento_Player>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Algo entrou no trigger: " + other.name);

            Debug.Log("Power-up coletado!");
            Movimento_Player player = other.GetComponent<Movimento_Player>();
            if (player != null)
            {
                move.forcaPuloBase = 20f;
            }

            Destroy(gameObject);
        }
    }
}
