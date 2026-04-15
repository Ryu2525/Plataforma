using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MovimentoPersonagem : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 5f;
    public float velocidadeEscada = 3f;
    public float forcaPulo = 5f; 
    
    [Header("Configurações de Ataque")]
    public GameObject objetoAtaque; // Arraste o PontoDeAtaque aqui
    public float tempoAtaqueAtivo = 0.2f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool estaNoChao;
    private bool estaNaEscada;
    private float gravidadeOriginal;
    private bool sendoEmpurrado = false;

    public Transform verificadorChao; 
    public float raioVerificacao = 0.2f;
    public LayerMask layerChao; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravidadeOriginal = rb.gravityScale;
        
        if(objetoAtaque != null) objetoAtaque.SetActive(false);
    }

    void Update()
    {
        if (sendoEmpurrado) return;

        var teclado = Keyboard.current;
        float movimentoHorizontal = 0f;

        // Lógica de Ataque
        if (teclado.enterKey.wasPressedThisFrame)
        {
            Atacar();
        }

        // Movimento Esquerda/Direita
        if (teclado.dKey.isPressed) 
        {
            movimentoHorizontal = 1f;
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else if (teclado.aKey.isPressed) 
        {
            movimentoHorizontal = -1f;
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }

        // Movimentação (Escada vs Chão)
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

        // Pulo
        if (teclado.spaceKey.wasPressedThisFrame && (estaNoChao || estaNaEscada))
        {
            estaNaEscada = false; 
            rb.gravityScale = gravidadeOriginal;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }

        estaNoChao = Physics2D.OverlapCircle(verificadorChao.position, raioVerificacao, layerChao);

        // Animator
        anim.SetFloat("velocidade", Mathf.Abs(movimentoHorizontal));
        anim.SetBool("estaPulando", !estaNoChao && !estaNaEscada);
    }

    void Atacar()
    {
        anim.SetTrigger("atacar");
        if (objetoAtaque != null) StartCoroutine(AtivarColisorAtaque());
    }

    IEnumerator AtivarColisorAtaque()
    {
        objetoAtaque.SetActive(true);
        yield return new WaitForSeconds(tempoAtaqueAtivo);
        objetoAtaque.SetActive(false);
    }

    public void AplicarKnockback(Vector2 forca)
    {
        sendoEmpurrado = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(forca, ForceMode2D.Impulse);
        Invoke(nameof(ResetarKnockback), 0.2f);
    }

    void ResetarKnockback() { sendoEmpurrado = false; }

    // Detecção de Escada
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada")) estaNaEscada = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada")) estaNaEscada = false;
    }
}