using UnityEngine;

public class GolemEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float stopDistance = 2f; // Distância mínima pra atacar
    public float alcanceDeAtaque = 3f;


    private Animator animator;

    // Vida
    public int vidaMaxima = 100;
    private int vidaAtual;

    private bool podeAtacar = true;
    public float tempoEntreAtaques = 2f;
    public int danoAtaque = 20;

    private bool estaMorto = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        if (player == null || estaMorto || tomandoDano) return;


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
            if (distancia <= stopDistance)
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
    }

    void AtacarPlayer()
    {
        // Verifica se o player tem o script Movimento_Player
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

    private bool tomandoDano = false;

    public void TomarDano(int dano)
    {
        if (estaMorto || tomandoDano) return;

        vidaAtual -= dano;
        animator.SetTrigger("TomarDano");
        tomandoDano = true;

        Invoke("LiberarDano", 0.5f); // tempo que dura a animação de dano

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
        animator.SetTrigger("Morrer");
        Destroy(gameObject, 2f); // dá tempo da animação acontecer
    }
}
