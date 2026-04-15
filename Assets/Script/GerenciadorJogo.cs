using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviour
{
    public GameObject telaVitoria;
    public GameObject telaDerrota;

    public void MostrarVitoria()
    {
        if (telaVitoria != null)
        {
            telaVitoria.SetActive(true);
            Time.timeScale = 0f; 
        }
    }

    public void MostrarDerrota()
    {
        if (telaDerrota != null)
        {
            telaDerrota.SetActive(true);
            Time.timeScale = 0f; 
        }
    }

    // Função atualizada para voltar ao menu inicial
    public void ReiniciarFase()
    {
        Time.timeScale = 1f; // ADICIONE ESTA LINHA AQUI!
        // Carrega a cena do menu principal
        // O nome dentro das aspas deve ser exatamente igual ao arquivo na sua pasta Scenes
        SceneManager.LoadScene("Home"); 
    }

    // Função que será chamada pelo botão
    public void CarregarProximaFase()
    {
        // Carrega a cena pelo nome exato que você tem na pasta Scenes
        Time.timeScale = 1f; // ADICIONE ESTA LINHA AQUI!
        SceneManager.LoadScene("Fase2");
    }
}