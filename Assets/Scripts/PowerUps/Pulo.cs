using UnityEngine;

public class Pulo : MonoBehaviour
{
    public float multiplicador = 1.5f;
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
                player.AumentarPuloTemporariamente(multiplicador, duracao);
            }

            Destroy(gameObject);
        }
    }
}
