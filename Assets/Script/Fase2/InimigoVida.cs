using UnityEngine;

public class InimigoVida : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public int vidaAtual;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // O Player deve chamar esta função
    public void TomarDano(int dano, Vector2 forcaImpacto)
    {
        vidaAtual -= dano;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(forcaImpacto, ForceMode2D.Impulse);
        }

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        // IMPORTANTE: Busca o gerenciador e avisa que o próximo deve nascer
        GerenciadorLuta luta = Object.FindFirstObjectByType<GerenciadorLuta>();
        if (luta != null)
        {
            luta.InimigoMorreu();
        }

        Destroy(gameObject);
    }
}