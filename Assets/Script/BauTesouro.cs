using UnityEngine;

public class BauTesouro : MonoBehaviour
{
    public GameObject prefabCoracao; // Arraste o Prefab do coração aqui
    public float forcaDrop = 5f;     // Força do pulo do coração
    private bool jaAbriu = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se foi o player que encostou e se o baú ainda está fechado
        if (other.CompareTag("Player") && !jaAbriu)
        {
            AbrirBau();
        }
    }

    void AbrirBau()
    {
        jaAbriu = true;

        // Cria o coração na posição do baú
        GameObject coracao = Instantiate(prefabCoracao, transform.position, Quaternion.identity);
        
        // Faz o coração "pular" para longe
        Rigidbody2D rbCoracao = coracao.GetComponent<Rigidbody2D>();
        if (rbCoracao != null)
        {
            // Gera uma direção aleatória para cima (esquerda ou direita)
            Vector2 direcaoPulo = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized;
            rbCoracao.AddForce(direcaoPulo * forcaDrop, ForceMode2D.Impulse);
        }

        // Faz o baú desaparecer
        Destroy(gameObject);
    }
}