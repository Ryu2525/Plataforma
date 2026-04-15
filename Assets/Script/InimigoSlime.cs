using UnityEngine;

public class InimigoSlime : MonoBehaviour
{
    public float forcaPuloVertical = 5f;
    public float forcaHorizontal = 2f;
    public float intervaloPulo = 2f;
    public int vida = 3;
    
    private Rigidbody2D rb;
    private Animator anim;
    private bool estaNoChao;
    private int direcao = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InvokeRepeating("Pular", intervaloPulo, intervaloPulo);
    }

    void Pular()
    {
        if (estaNoChao)
        {
            if (anim != null) anim.SetTrigger("pulou"); 
            rb.linearVelocity = new Vector2(forcaHorizontal * direcao, forcaPuloVertical);
            estaNoChao = false;
        }
    }

    public void ReceberDano(int dano, Vector2 forcaImpacto)
    {
        vida -= dano; //test 
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(forcaImpacto, ForceMode2D.Impulse);
        
        if (vida <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Dano no Player
        if (collision.gameObject.CompareTag("Player"))
        {
            ControleVida cv = FindFirstObjectByType<ControleVida>();
            if (cv != null) cv.TomarDano();

            float lado = (collision.transform.position.x - transform.position.x) > 0 ? 1 : -1;
            
            // Knockback no Player
            MovimentoPersonagem mov = collision.gameObject.GetComponent<MovimentoPersonagem>();
            if (mov != null) mov.AplicarKnockback(new Vector2(lado * 7f, 3f));

            // Repulsão no Slime
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(-lado * 4f, 2f), ForceMode2D.Impulse);

            InverterDirecao();
        }
        // Chão (Tag Chao)
        else if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
            rb.linearVelocity = Vector2.zero;
            if (anim != null) anim.SetTrigger("caiu");
        }
        // Paredes
        else 
        {
            InverterDirecao();
        }
    }

    void InverterDirecao()
    {
        direcao *= -1;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direcao, transform.localScale.y, 1);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao")) estaNoChao = false;
    }
}