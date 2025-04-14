using UnityEngine;

public class Invencibilidade : MonoBehaviour
{
    public float duracao = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimento_Player player = other.GetComponent<Movimento_Player>();
            if (player != null)
            {
                player.AtivarInvencibilidadeTemporaria(duracao);
            }

            Destroy(gameObject);
        }
    }
}
