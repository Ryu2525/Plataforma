using UnityEngine;

public class InimigoSlime : MonoBehaviour
{
    public float forcaPuloVertical = 5f;
    public float forcaHorizontal = 2f;
    public float intervaloPulo = 2f;
    
    private Rigidbody2D rb;
    private Animator anim;
    private bool estaNoChao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Inicia o ciclo de pulos
        InvokeRepeating("Pular", intervaloPulo, intervaloPulo);
    }

    void Pular()
    {
        if (estaNoChao)
        {
            // Ativa o gatilho da animação de pulo
            if (anim != null) anim.SetTrigger("pulou"); 

            // Aplica a força para cima e para frente
            rb.linearVelocity = new Vector2(forcaHorizontal, forcaPuloVertical);
            
            // Forçamos o estaNoChao a false para evitar pulos duplos no mesmo frame
            estaNoChao = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se tocou na Layer "Chao" ou se tem a Tag "Ground"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Chao") || collision.gameObject.CompareTag("Ground"))
        {
            estaNoChao = true;

            // SOLUÇÃO PARA O DESLIZAMENTO: Zerar a velocidade ao cair
            rb.linearVelocity = Vector2.zero;

            // Ativa o gatilho para voltar à animação parado
            if (anim != null) anim.SetTrigger("caiu");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Chao") || collision.gameObject.CompareTag("Ground"))
        {
            estaNoChao = false;
        }
    }
}