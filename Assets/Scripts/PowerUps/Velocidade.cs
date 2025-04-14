using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float multiplicador = 2f;
    public float duracao = 5f;

    void Start()
    {
        Debug.Log("PowerUp carregado.");
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
                player.AumentarVelocidadeTemporariamente(multiplicador, duracao);
            }
                        
            Destroy(gameObject); 
        }
    }
}
