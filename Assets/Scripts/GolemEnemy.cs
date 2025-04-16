using UnityEngine;

public class GolemEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float stopDistance = 2f;
    public float alcanceDeAtaque = 3f;

    private Animator animator;

    // Vida
    public int vidaMaxima = 30;
    private int vidaAtual;
    private bool estaMorto = false;

    // Ataque
    private bool podeAtacar = true;
    public float tempoEntreAtaques = 2f;
    public int danoAtaque = 3;

    // Área de detecção
    private bool jogadorDetectado = false;

    // Dano
    private bool tomandoDano = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        if (player == null || estaMorto || tomandoDano || !jogadorDetectado) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia > stopDistance)
        {
            // Persegue o player
            Vector3 direcao = (player.position - transform.position).normalized;
            transform.position += direcao * speed * Time.deltaTime;

            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            animator.SetBool("IsRuning", true);
        }
        else
        {
            animator.SetBool("IsRuning", false);

            if (distancia <= alcanceDeAtaque && podeAtacar)
            {
                animator.SetTrigger("Attack");
                podeAtacar = false;
                Invoke("AtacarPlayer", 0.5f);
                Invoke("ResetarAtaque", tempoEntreAtaques);
            }
        }
    }

    void AtacarPlayer()
    {
        if (player == null) return;

        Movimento_Player mp = player.GetComponent<Movimento_Player>();
        if (mp != null)
        {
            mp.TomarDano(danoAtaque);
        }
    }

    void ResetarAtaque()
    {
        podeAtacar = true;
    }

    public void TomarDano(int dano)
    {
        if (estaMorto || tomandoDano) return;

        vidaAtual -= dano;
        animator.SetTrigger("TomarDano");
        tomandoDano = true;

        Invoke("LiberarDano", 0.5f);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void LiberarDano()
    {
        tomandoDano = false;
    }

    void Morrer()
    {
        estaMorto = true;
        animator.SetTrigger("Morrer");
        Destroy(gameObject, 2f);
    }

    // ⬇️ TRIGGERS DA ÁREA DE DETECÇÃO

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDetectado = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDetectado = false;
            animator.SetBool("IsRuning", false); // para a animação quando sai da área
        }
    }
}
