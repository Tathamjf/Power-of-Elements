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

    //sistema de vida
    public int vidaMaxima = 100;
    public int vidaAtual;

    [SerializeField] private BarraDeVida barraDeVida;

    //sistema de ataque
    private bool podeMover = true; // Começa podendo se mover

    public int dano = 25;
    public float alcance = 2f;
    public LayerMask Enemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        vidaAtual = vidaMaxima;
                
        myCamera = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!podeMover)
        {
            // Ainda aplica gravidade enquanto não pode mover
            controller.Move(new Vector3(0, forcaY, 0) * Time.deltaTime);
            return;
        }

        // movimento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0, vertical);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0;

        Vector3 movimentoFinal = (movimento * 5f) + new Vector3(0, forcaY, 0);
        controller.Move(movimentoFinal * Time.deltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

        animator.SetBool("IsWalking", movimento != Vector3.zero);

        estaNoChao = Physics.CheckSphere(footPlayer.position, 0.3f, colisaoPlayer);
        animator.SetBool("EstaNoChao", estaNoChao);

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            forcaY = 10f;
            animator.SetTrigger("Jump");
        }

        // gravidade
        if (forcaY > -9.81f)
        {
            forcaY += -9.81f * Time.deltaTime;
        }

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
        vidaAtual -= danoRecebido;
        animator.SetTrigger("TomarDano");
        podeMover = false;

        // Duração da animação de dano, ex: 0.5 segundos
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
        // Se quiser empurrar também nos eixos X ou Z, você pode ajustar `velocidadeAtual`
    }

}
