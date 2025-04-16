using UnityEngine;

public class DanoPorFogo : MonoBehaviour
{
    public int danoPorSegundo = 2;
    private float tempoDano = 0f;
    public float intervaloDano = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tempoDano += Time.deltaTime;

            if (tempoDano >= intervaloDano)
            {
                Movimento_Player player = other.GetComponent<Movimento_Player>();
                if (player != null)
                {
                    player.TomarDano(danoPorSegundo);
                    Debug.Log("Player esta tomando dano");
                }
                tempoDano = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tempoDano = 0f;
        }
    }
}
