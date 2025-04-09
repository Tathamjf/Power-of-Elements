using UnityEngine;


public class NavMesh : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;      // Velocidade do inimigo
    public float stopDistance = 10f; // Distância mínima para parar

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stopDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                // Opcional: rotacionar para olhar para o jogador
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            }
        }
    }
}
