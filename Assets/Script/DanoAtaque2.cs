using UnityEngine;

public class DanoAtaque2 : MonoBehaviour
{
    public int danoParaDar = 1;
    public float forcaImpacto = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto atingido tem a tag "Inimigo"
        if (other.CompareTag("Inimigo"))
        {
            Debug.Log("ACERTOU O INIMIGO: " + other.name);
            
            // Calcula a direção do empurrão (do ponto de ataque para o inimigo)
            Vector2 direcao = (other.transform.position - transform.position).normalized;
            Vector2 forcaFinal = direcao * forcaImpacto;

            // 1. PRIORIDADE: Tenta o sistema de fases (InimigoVida)
            // Use isso para a luta do Boss
            InimigoVida vidaFases = other.GetComponent<InimigoVida>();
            if (vidaFases != null)
            {
                vidaFases.TomarDano(danoParaDar, forcaFinal);
                return; 
            }

            // 2. SEGUNDA OPÇÃO: Tenta o script de movimento SlimeInimigo
            // Use isso para a Fase 1 (onde não há o script InimigoVida)
            SlimeInimigo slime = other.GetComponent<SlimeInimigo>();
            if (slime != null)
            {
                slime.ReceberDano(danoParaDar, forcaFinal);
                return;
            }

            // 3. TERCEIRA OPÇÃO: Tenta o Boss ou outros inimigos
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.ReceberDano(danoParaDar, forcaFinal);
                return;
            }
        }
    }
}