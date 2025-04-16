﻿using System.Collections;
using UnityEngine;

public class Movimento_Player : MonoBehaviour
{
    private Transform myCamera;

    private CharacterController controller;
    private Animator animator;

    private bool estaNoChao;  
    [SerializeField] private Transform footPlayer;
    [SerializeField] private LayerMask colisaoPlayer;

    public float forcaY;

    //POWER UPS!!!
    // power-up velocidade
    public float velocidadeBase = 5f;
    private float velocidadeAtual;

    private Coroutine boostCoroutine;

    //power-up pulo
    public float forcaPuloBase = 10f; 
    private Coroutine puloCoroutine;
    public float duracao = 5f;

    //power-up invencibilidade
    private bool estaInvencivel = false;
    private Coroutine invencibilidadeCoroutine;


    //sistema de vida
    public int vidaMaxima = 20;
    public int vidaAtual;
    

    [SerializeField] private BarraDeVida barra;

    //sistema de ataque
    private bool podeMover = true; // Começa podendo se mover

    public int dano = 5;
    public float alcance = 2f;
    public LayerMask Enemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;

        vidaAtual = vidaMaxima;
        velocidadeAtual = velocidadeBase;

        barra.ColocarVidaMaxima(vidaMaxima);

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        StartCoroutine(EsperarCamera());
    }

    private IEnumerator EsperarCamera()
    {
        while (Camera.main == null)
        {
            yield return null;
        }

        myCamera = Camera.main.transform;
    }




    // Update is called once per frame
    void Update()
    {
        if (myCamera == null) return;


        if (!podeMover)
        {
            // Ainda aplica gravidade enquanto não pode mover
            controller.Move(new Vector3(0, forcaY, 0) * Time.deltaTime);
            return;
        }

        // movimento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var movimento = new Vector3(horizontal, 0, vertical);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0;

        //Debug.Log($"Movimento: {movimento}, forcaY: {forcaY}, estaNoChao: {estaNoChao}");


        //movimento final
        Vector3 movimentoFinal = (movimento * velocidadeAtual);
        movimentoFinal.y = forcaY;

        controller.Move(movimentoFinal * Time.deltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

        animator.SetBool("IsWalking", movimento != Vector3.zero);

        // gravidade e chão
        estaNoChao = Physics.CheckSphere(footPlayer.position, 0.3f, colisaoPlayer);
        animator.SetBool("EstaNoChao", estaNoChao);

        if (estaNoChao && forcaY < 0)
        {
            forcaY = -2f; // pequena força pra manter no chão
        }

        // pulo
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            forcaY = forcaPuloBase;
            animator.SetTrigger("Jump");
        }

        // aplica gravidade
        forcaY += -9.81f * Time.deltaTime;

        // ataque
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            podeMover = false;
            animator.SetTrigger("Attack");

            // Espera o tempo REAL da animação antes de liberar o movimento
            Invoke("FimDoAtaque", 0.8f); 
        }
    }


    private void AplicarDano()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, alcance, Enemy))
        {
            Movimento_Player golem = hit.collider.GetComponent<Movimento_Player>();
            if (golem != null)
            {
                golem.TomarDano(dano);
                Debug.Log("Dano aplicado via evento da animação!");
            }
        }

        GolemEnemy inimigo = hit.collider.GetComponent<GolemEnemy>();
        if (inimigo != null)
        {
            inimigo.TomarDano(dano);
        }

    }

    void FimDoAtaque()
    {        
        podeMover = true;
    }


    public void TomarDano(int danoRecebido)
    {
        if (estaInvencivel) return; // Ignora dano se invencível

        vidaAtual -= danoRecebido;
        barra.AlterarBarraVida(vidaAtual, danoRecebido);
        
        animator.SetTrigger("TomarDano");
        podeMover = false;

        Invoke("FimDano", 0.5f);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void FimDano()
    {
        podeMover = true;
    }


    void Morrer()
    {
        animator.SetTrigger("Die");
        Debug.Log(gameObject.name + " morreu!");
        Destroy(gameObject); 
    }

    public void TomarImpulso(Vector3 direcao, float forca, float impulso)
    {
        impulso = direcao.y * forca;
        
    }


    //POWER UPs


    //power-up velocidade
    public void AumentarVelocidadeTemporariamente(float multiplicador, float duracao)
    {
        if (boostCoroutine != null)
        {
            StopCoroutine(boostCoroutine);
        }
        boostCoroutine = StartCoroutine(SpeedBoostCoroutine(multiplicador, duracao));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplicador, float duracao)
    {
        velocidadeAtual = velocidadeBase * multiplicador;
        yield return new WaitForSeconds(duracao);
        velocidadeAtual = velocidadeBase;
    }


    //power-up invencibilidade
    public void AtivarInvencibilidadeTemporaria(float duracao)
    {
        if (invencibilidadeCoroutine != null)
        {
            StopCoroutine(invencibilidadeCoroutine);
        }
        invencibilidadeCoroutine = StartCoroutine(InvencibilidadeCoroutine(duracao));
    }

    private IEnumerator InvencibilidadeCoroutine(float duracao)
    {
        estaInvencivel = true;
        Debug.Log("Invencível!");

        // Efeito visual (piscar)
        Renderer rend = GetComponentInChildren<Renderer>();
        float tempo = 0f;
        while (tempo < duracao)
        {
            if (rend != null) rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(0.2f);
            tempo += 0.2f;
        }

        if (rend != null) rend.enabled = true;
        estaInvencivel = false;
        Debug.Log("Invencibilidade acabou.");
    }

}
