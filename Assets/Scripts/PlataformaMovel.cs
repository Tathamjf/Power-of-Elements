using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    public float alturaCima = 3f;      // Quanto sobe acima do ponto original
    public float alturaBaixo = 2f;     // Quanto desce abaixo do ponto original
    public float velocidade = 2f;      // Velocidade de movimento
    public float tempoEspera = 1f;     // Tempo parado no topo/fundo

    private Vector3 pontoMeio;         // Posição original
    private Vector3 pontoCima;         // Posição final superior
    private Vector3 pontoBaixo;        // Posição final inferior
    private Vector3 destinoAtual;
    //private bool subindo = true;
    private float tempoParado = 0f;

    void Start()
    {
        pontoMeio = transform.position;
        pontoCima = pontoMeio + Vector3.up * alturaCima;
        pontoBaixo = pontoMeio - Vector3.up * alturaBaixo;
        destinoAtual = pontoCima;
    }

    void Update()
    {
        if (tempoParado > 0)
        {
            tempoParado -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, destinoAtual, velocidade * Time.deltaTime);

        if (Vector3.Distance(transform.position, destinoAtual) < 0.01f)
        {
            tempoParado = tempoEspera;
            if (destinoAtual == pontoCima)
            {
                destinoAtual = pontoBaixo;
            }
            else
            {
                destinoAtual = pontoCima;
            }
        }
    }
}
