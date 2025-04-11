using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Image slider;
        
    public void AlterarBarraVida(int vidaAtual, int vidaMaxima)
    {
        slider.fillAmount = (float) vidaAtual / vidaMaxima;
    }
}
