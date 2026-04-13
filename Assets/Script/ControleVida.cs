using UnityEngine;
using TMPro;

public class ControleVida : MonoBehaviour
{
    public int vidaAtual = 3;
    public TextMeshProUGUI textoVidaUI; // Arraste o objeto "Coracao" aqui no Inspector

    void Start()
    {
        AtualizarInterfaceVida();
    }

    public void TomarDano()
    {
        if (vidaAtual > 0)
        {
            vidaAtual--;
            AtualizarInterfaceVida();
        }
    }

    void AtualizarInterfaceVida()
    {
        string iconesVida = "";
        for (int i = 0; i < vidaAtual; i++)
        {
            // Isso desenha um coração para cada ponto de vida restante
            iconesVida += "<sprite name=\"Vida\"> ";
        }
        textoVidaUI.text = iconesVida;
    }
}