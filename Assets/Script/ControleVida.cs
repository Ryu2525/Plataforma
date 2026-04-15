using UnityEngine;
using TMPro;

public class ControleVida : MonoBehaviour
{
    public int vidaAtual = 3;
    public TextMeshProUGUI textoVidaUI;

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

        // VERIFICAÇÃO DE DERROTA
        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        // 1. Primeiro o aviso no console (se isso não aparecer, o erro é antes)
        Debug.Log("O Player morreu! Iniciando processo de derrota...");
        
        // 2. Tenta achar o Gerenciador de forma mais segura
        GerenciadorJogo gameManager = Object.FindFirstObjectByType<GerenciadorJogo>();
        
        if (gameManager != null)
        {
            Debug.Log("Gerenciador encontrado. Ativando tela...");
            gameManager.MostrarDerrota();
        }
        else
        {
            // Se o console mostrar isso, seu objeto GameManager não tem o script GerenciadorJogo
            Debug.LogError("ERRO: Objeto com o script GerenciadorJogo não encontrado na cena!");
        }
    }

    void AtualizarInterfaceVida()
    {
        string iconesVida = "";
        for (int i = 0; i < vidaAtual; i++)
        {
            iconesVida += "<sprite name=\"Vida\"> ";
        }
        textoVidaUI.text = iconesVida;
    }

    public void GanharVida()
    {
        if (vidaAtual < 3) 
        {
            vidaAtual++;
            AtualizarInterfaceVida();
        }
    }
}