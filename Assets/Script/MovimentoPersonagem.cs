using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentoPersonagem : MonoBehaviour
{
    public float velocidade = 5f;
    public float velocidadeEscada = 3f;
    public float forcaPulo = 5f; 
    
    private Rigidbody2D rb;
    private Animator anim; // Referência para o Animator
    private bool estaNoChao;
    private bool estaNaEscada;
    private float gravidadeOriginal;

    public Transform verificadorChao; 
    public float raioVerificacao = 0.2f;
    public LayerMask layerChao; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Inicializa o Animator
        gravidadeOriginal = rb.gravityScale;
    }

    void Update()
    {
        var teclado = Keyboard.current;
        float movimentoHorizontal = 0f;

        // Movimento Esquerda/Direita
        if (teclado.dKey.isPressed) 
        {
            movimentoHorizontal = 1f;
            transform.localScale = new Vector3(0.5f, 0.5f, 1); // Vira para direita
        }
        else if (teclado.aKey.isPressed) 
        {
            movimentoHorizontal = -1f;
            transform.localScale = new Vector3(-0.5f, 0.5f, 1); // Vira para esquerda
        }

        // 1. Lógica de Movimentação (Chão vs Escada)
        if (estaNaEscada)
        {
            float movimentoVertical = 0f;
            if (teclado.wKey.isPressed) movimentoVertical = 1f;
            else if (teclado.sKey.isPressed) movimentoVertical = -1f;

            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(movimentoHorizontal * velocidade, movimentoVertical * velocidadeEscada);
            
        }
        else
        {
            rb.gravityScale = gravidadeOriginal;
            rb.linearVelocity = new Vector2(movimentoHorizontal * velocidade, rb.linearVelocity.y);
        }

        // 2. Lógica de Pulo
        if (teclado.spaceKey.wasPressedThisFrame)
        {
            if (estaNoChao || estaNaEscada)
            {
                estaNaEscada = false; 
                rb.gravityScale = gravidadeOriginal;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            }
        }

        // 3. Verificação constante do chão
        estaNoChao = Physics2D.OverlapCircle(verificadorChao.position, raioVerificacao, layerChao);

        // --- ENVIAR DADOS PARA O ANIMATOR ---
        
        // Passa a velocidade horizontal (sempre positiva) para o parâmetro "velocidade"
        anim.SetFloat("velocidade", Mathf.Abs(movimentoHorizontal));

        // Define se está pulando para o parâmetro "estaPulando"
        // Se NÃO está no chão e NÃO está na escada, ele está no ar
        anim.SetBool("estaPulando", !estaNoChao && !estaNaEscada);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada") || collision.gameObject.layer == LayerMask.NameToLayer("Escada"))
        {
            estaNaEscada = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada") || collision.gameObject.layer == LayerMask.NameToLayer("Escada"))
        {
            estaNaEscada = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada") || collision.gameObject.layer == LayerMask.NameToLayer("Escada"))
        {
            estaNaEscada = false;
        }
    }
}