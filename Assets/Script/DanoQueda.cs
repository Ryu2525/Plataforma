using UnityEngine;

public class DanoQueda : MonoBehaviour
{
    public float alturaMinimaDano = 5f; // Altura a partir da qual ele perde vida
    private float pontoMaisAlto;
    private bool estavaNoAr = false;
    private Rigidbody2D rb;
    private ControleVida controleVida;

void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Como o script ControleVida está no GameManager, usamos FindFirstObjectByType
        controleVida = FindFirstObjectByType<ControleVida>(); 
        
        if (controleVida == null) {
            Debug.LogError("O script ControleVida não foi encontrado no GameManager!");
        }
    }

    void Update()
    {
        // Se a velocidade vertical for negativa, ele está descendo
        if (rb.linearVelocity.y < -0.1f)
        {
            if (!estavaNoAr)
            {
                // Registra de onde ele começou a cair
                pontoMaisAlto = transform.position.y;
                estavaNoAr = true;
            }
            
            // Se ele continuar caindo e passar do ponto registrado (ex: pulou e caiu), atualiza
            if (transform.position.y > pontoMaisAlto)
            {
                pontoMaisAlto = transform.position.y;
            }
        }
        else if (Mathf.Abs(rb.linearVelocity.y) < 0.1f && estavaNoAr)
        {
            // Acabou de pousar
            VerificarDano();
            estavaNoAr = false;
        }
    }

    void VerificarDano()
    {
        float distanciaCaindo = pontoMaisAlto - transform.position.y;

        if (distanciaCaindo >= alturaMinimaDano)
        {
            Debug.Log("Caiu de muito alto! Distância: " + distanciaCaindo);
            if (controleVida != null)
            {
                controleVida.TomarDano(); // Chama a função que tira o coração da UI
            }
        }
    }
}