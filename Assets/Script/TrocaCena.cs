using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Importante para o novo sistema

public class TrocaCena : MonoBehaviour
{
    [Header("Configurações de UI")]
    public GameObject painelTutorial; // Arraste o seu painel de tutorial aqui no Inspector

    void Update()
    {
        // 1. Verifica se a tecla Enter foi pressionada para iniciar o jogo
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            CarregarFase1();
        }

        // 2. Verifica se a tecla Esc foi pressionada para abrir/fechar o tutorial
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            AlternarTutorial();
        }
    }

    public void CarregarFase1()
    {
        // Certifique-se de que a cena Fase1 está no Build Settings
        SceneManager.LoadScene("Fase1");
    }

    public void AlternarTutorial()
    {
        if (painelTutorial != null)
        {
            // Pega o estado atual (ativo ou inativo) e inverte
            bool estaAtivo = painelTutorial.activeSelf;
            painelTutorial.SetActive(!estaAtivo);
        }
        else
        {
            Debug.LogWarning("Aviso: O objeto do Painel Tutorial não foi arrastado para o script!");
        }
    }
}