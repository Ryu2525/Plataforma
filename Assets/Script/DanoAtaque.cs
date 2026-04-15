using UnityEngine;

public class DanoAtaque : MonoBehaviour
{
    public int danoParaDar = 1;
    public float forcaImpacto = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto atingido tem a tag "Inimigo"
        if (other.CompareTag("Inimigo"))
        {
            Debug.Log("ACERTOU O INIMIGO: " + other.name);
            
            // Calcula a direção do empurrão (knockback)
            Vector2 direcao = (other.transform.position - transform.position).normalized;
            Vector2 forcaFinal = direcao * forcaImpacto;

            // 1. Tenta causar dano se for um Slime comum
            InimigoSlime slime = other.GetComponent<InimigoSlime>();
            if (slime != null)
            {
                slime.ReceberDano(danoParaDar, forcaFinal);
                return; // Encontrou o slime, não precisa procurar o boss
            }

            // 2. Tenta causar dano se for o Boss
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.ReceberDano(danoParaDar, forcaFinal);
                return; // Encontrou o boss
            }
        }
    }
}