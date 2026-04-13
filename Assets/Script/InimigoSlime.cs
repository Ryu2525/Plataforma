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
            // Aplica a força para cima e para frente
            rb.linearVelocity = new Vector2(forcaHorizontal, forcaPuloVertical);
            
            // Ativa o gatilho da animação de pulo
            anim.SetTrigger("pulou"); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se tocou na Layer "Chao"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            estaNoChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            estaNoChao = false;
        }
    }
}