using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    [SerializeField] private int valorMoeda = 1;

    // Esta função roda automaticamente quando algo entra no campo da moeda
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se quem encostou tem a Tag "Player"
        if (collision.CompareTag("Player"))
        {
            // PROCURA o gerenciador na cena e avisa que coletou
            GerenciadorMoedas gerenciador = FindFirstObjectByType<GerenciadorMoedas>();
            
            if (gerenciador != null)
            {
                gerenciador.AdicionarMoeda();
            }

            // Destrói a moeda para ela sumir da tela
            Destroy(gameObject);
        }
    }
}