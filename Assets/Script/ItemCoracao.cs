using UnityEngine;

public class ItemCoracao : MonoBehaviour
{
    public float tempoSeguranca = 0.5f; 
    private float contador;
    private bool podeColetar = false;

    void Start()
    {
        contador = tempoSeguranca;
    }

    void Update()
    {
        if (contador > 0)
        {
            contador -= Time.deltaTime;
        }
        else
        {
            podeColetar = true;
        }
    }

    // Usando OnTriggerEnter2D (Certifique-se que um dos colisores é TRIGGER)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (podeColetar && other.CompareTag("Player"))
        {
            // Busca o script de vida que você postou
            ControleVida controle = FindFirstObjectByType<ControleVida>();

            if (controle != null)
            {
                controle.GanharVida(); // Chama a sua função de ganhar vida
                Debug.Log("Vida aumentada!");
                Destroy(gameObject); // DESTROI O CORAÇÃO DA CENA
            }
        }
    }
}