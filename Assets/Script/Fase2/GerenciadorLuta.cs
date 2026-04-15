using UnityEngine;

public class GerenciadorLuta : MonoBehaviour
{
    public GameObject prefabInimigo;
    public Transform pontoDeSpawn;
    
    private int faseAtual = 0;
    private int[] vidasDasFases = { 3, 5, 8 };

    // Definimos os tamanhos para cada fase aqui
    // 0.5f é o seu tamanho original. Vamos aumentando.
    private float[] tamanhosDasFases = { 1f, 2f, 4f };

    void Start()
    {
        SpawnarProximoInimigo();
    }

    public void InimigoMorreu()
    {
        faseAtual++;

        if (faseAtual < vidasDasFases.Length)
        {
            SpawnarProximoInimigo();
        }
        else
        {
            FindFirstObjectByType<GerenciadorJogo>().MostrarVitoria();
        }
    }

    void SpawnarProximoInimigo()
    {
        if (prefabInimigo == null || pontoDeSpawn == null) return;

        GameObject novoInimigo = Instantiate(prefabInimigo, pontoDeSpawn.position, Quaternion.identity);
        
        // 1. AJUSTE DE TAMANHO (Escala)
        float novoTamanho = tamanhosDasFases[faseAtual];
        novoInimigo.transform.localScale = new Vector3(novoTamanho, novoTamanho, 1f);

        // 2. CONFIGURAÇÃO DE VIDA
        InimigoVida vidaScript = novoInimigo.GetComponent<InimigoVida>();
        if (vidaScript != null)
        {
            vidaScript.vidaAtual = vidasDasFases[faseAtual];
            Debug.Log("Spawnado Inimigo Fase " + (faseAtual + 1) + " | HP: " + vidasDasFases[faseAtual] + " | Escala: " + novoTamanho);
        }
    }
}