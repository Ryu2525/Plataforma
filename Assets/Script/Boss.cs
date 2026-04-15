using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float forcaPuloVertical = 5f;
    public float forcaHorizontal = 2f;
    public float intervaloPulo = 2f;
    
    [Header("Configurações de Combate")]
    public int vida = 10; // Aumentei a vida por ser um Boss
    public float forcaEmpurraoPlayer = 7f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool estaNoChao;
    private int direcao = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        // Inicia a rotina de pulos automáticos
        InvokeRepeating("Pular", intervaloPulo, intervaloPulo);
    }

    void Pular()
    {
        if (estaNoChao)
        {
            // Dispara animação de pulo (Garante que os Triggers 'pulou' e 'caiu' existam no Animator)
            if (anim != null) anim.SetTrigger("pulou"); 
            
            rb.linearVelocity = new Vector2(forcaHorizontal * direcao, forcaPuloVertical);
            estaNoChao = false;
        }
    }

    // Função chamada pelo script DanoAtaque do Player
    public void ReceberDano(int dano, Vector2 forcaImpacto)
    {
        vida -= dano;
        
        // Aplica o coice (knockback) no Boss
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(forcaImpacto, ForceMode2D.Impulse);
        
        Debug.Log("Boss recebeu dano! Vida restante: " + vida);

        if (vida <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Boss Derrotado!");

        // Busca o Gerenciador de Jogo para mostrar a tela de vitória
        GerenciadorJogo gameManager = Object.FindFirstObjectByType<GerenciadorJogo>();
        
        if (gameManager != null)
        {
            gameManager.MostrarVitoria();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Se encostar no Player, causa impacto
        if (collision.gameObject.CompareTag("Player"))
        {
            float lado = (collision.transform.position.x - transform.position.x) > 0 ? 1 : -1;
            
            // Tenta aplicar o knockback no Player se o script dele tiver essa função
            var mov = collision.gameObject.GetComponent<MonoBehaviour>(); 
            // Nota: Se seu script de movimento tiver 'AplicarKnockback', você pode chamar aqui.

            // Boss recua um pouco ao bater
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(-lado * 3f, 2f), ForceMode2D.Impulse);

            InverterDirecao();
        }
        // 2. Se bater no Chão (Tag Chao)
        else if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
            if (anim != null) anim.SetTrigger("caiu");
        }
        // 3. Se bater em paredes/obstáculos
        else 
        {
            InverterDirecao();
        }
    }

    void InverterDirecao()
    {
        direcao *= -1;
        // Gira o sprite horizontalmente
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direcao, transform.localScale.y, 1);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = false;
        }
    }
}