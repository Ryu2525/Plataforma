using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para reiniciar a fase

public class GerenciadorJogo : MonoBehaviour
{
    public GameObject telaVitoria;
    public GameObject telaDerrota; // Arraste a nova tela aqui no Inspector

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
            Time.timeScale = 0f; // Pausa o jogo na derrota
        }
    }

    public void ReiniciarFase()
    {
        Time.timeScale = 1f; // Despausa o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}