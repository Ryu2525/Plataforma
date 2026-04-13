using UnityEngine;
using UnityEngine.Tilemaps; // Necessário para acessar componentes de Tilemap
using System.Collections;

public class ChaoFalsoTilemap : MonoBehaviour
{
    [Header("Configurações de Tempo")]
    public float tempoParaSumir = 0.2f; 
    public float tempoParaReaparecer = 2f;

    private TilemapRenderer tilemapRenderer;
    private TilemapCollider2D tilemapCollider;

    void Start()
    {
        // Pega os componentes específicos do Tilemap
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a tag do objeto que colidiu é "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SequenciaSumir());
        }
    }

    IEnumerator SequenciaSumir()
    {
        // Espera o tempo de reação (pisada)
        yield return new WaitForSeconds(tempoParaSumir);

        // Desativa o visual e a colisão
        if (tilemapRenderer != null) tilemapRenderer.enabled = false;
        if (tilemapCollider != null) tilemapCollider.enabled = false;

        // Espera o tempo para voltar
        yield return new WaitForSeconds(tempoParaReaparecer);

        // Reativa tudo
        if (tilemapRenderer != null) tilemapRenderer.enabled = true;
        if (tilemapCollider != null) tilemapCollider.enabled = true;
    }
}