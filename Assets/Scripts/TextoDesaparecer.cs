using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextoDesaparecer : MonoBehaviour
{
    public TextMeshPro texto; // Referência ao componente de texto
    public float tempoVisivel = 2f; // Tempo que o texto ficará visível

    void Start()
    {
        // Começa a cor do texto como transparente
        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);
    }

    public void MostrarTexto(string mensagem)
    {
        // Exibe a mensagem no texto
        texto.text = mensagem;

        // Torna o texto visível
        StartCoroutine(FadeTexto());
    }

    IEnumerator FadeTexto()
    {
        // Torna o texto visível (fade in)
        for (float t = 0; t < 1; t += Time.deltaTime / 1f) // Durando 1 segundo
        {
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        // Espera o tempo definido
        yield return new WaitForSeconds(tempoVisivel);

        // Torna o texto invisível (fade out)
        for (float t = 0; t < 1; t += Time.deltaTime / 1f)
        {
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        // Limpa o texto após desaparecer
        texto.text = "";
    }
}
