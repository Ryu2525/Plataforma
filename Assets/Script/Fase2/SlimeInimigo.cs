using UnityEngine;

public class SlimeInimigo : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float forcaPuloVertical = 5f;
    public float forcaHorizontal = 2f;
    public float intervaloPulo = 2f;
    
    [Header("Configurações de Vida (Fase Comum)")]
    public int vida = 3;
    
    private Rigidbody2D rb;
    private Animator anim;
    private bool estaNoChao;
    private int direcao = 1;

    void Awake()
    {
        // Inicializa os componentes físicos e de animação
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // Inicia a rotina de pulos automáticos
        InvokeRepeating("Pular", intervaloPulo, intervaloPulo);
    }

    void Pular()
    {
        if (estaNoChao && rb != null)
        {
            if (anim != null) anim.SetTrigger("pulou"); 
            
            // Aplica a força do pulo
            rb.linearVelocity = new Vector2(forcaHorizontal * direcao, forcaPuloVertical);
            estaNoChao = false;
        }
    }

    // Função chamada pelo Player na Fase 1 ou inimigos comuns
    public void ReceberDano(int dano, Vector2 forcaImpacto)
    {
        vida -= dano;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(forcaImpacto, ForceMode2D.Impulse);
        }
        
        // Se NÃO houver um script InimigoVida controlando (Fase 1), o Slime se destrói
        if (vida <= 0 && GetComponent<InimigoVida>() == null) 
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Colisão com o Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Tira vida do Player
            var cv = Object.FindFirstObjectByType<ControleVida>();
            if (cv != null) cv.TomarDano();

            // Aplica Knockback no Player via SendMessage (mais seguro)
            float lado = (collision.transform.position.x - transform.position.x) > 0 ? 1 : -1;
            collision.gameObject.SendMessage("AplicarKnockback", new Vector2(lado * 7f, 3f), SendMessageOptions.DontRequireReceiver);

            // Repulsão no próprio Slime ao bater no player
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(new Vector2(-lado * 4f, 2f), ForceMode2D.Impulse);
            }

            InverterDirecao();
        }
        // 2. Colisão com o Chão (CORRIGE O DESLIZE)
        else if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
            
            // TRAVA DE MOVIMENTO: Zera a velocidade ao cair para não deslizar
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; 
            }

            if (anim != null) anim.SetTrigger("caiu");
        }
        // 3. Colisão com Paredes
        else 
        {
            // Só inverte se não for um trigger (como o colisor de ataque do player)
            if (!collision.collider.isTrigger) 
            {
                InverterDirecao();
            }
        }
    }

    void InverterDirecao()
    {
        direcao *= -1;
        // Inverte o sprite visualmente
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direcao, transform.localScale.y, 1);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao")) 
        {
            estaNoChao = false;
        }
    }
}