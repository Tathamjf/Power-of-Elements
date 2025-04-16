using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider slider;
        
    public void ColocarVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima;
        slider.value = vidaMaxima;
    }

    public void AlterarBarraVida(int vidaAtual, int vidaMaxima)
    {
        slider.value = (float) vidaAtual / vidaMaxima;
    }
}
