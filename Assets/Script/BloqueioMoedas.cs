using UnityEngine;

public class BloqueioMoedas : MonoBehaviour
{
    public int moedasNecessarias = 4;
    private GerenciadorMoedas gerenciador;

    void Start()
    {
        // Procura o gerenciador na cena
        gerenciador = FindFirstObjectByType<GerenciadorMoedas>();
    }

    void Update()
    {
        // Se o gerenciador existir e o total de moedas for maior ou igual a 4
        if (gerenciador != null && gerenciador.totalMoedas >= moedasNecessarias)
        {
            // Desaparece com a pedra
            gameObject.SetActive(false);
            
            // Opcional: Você pode adicionar um som ou efeito de poeira aqui
            Debug.Log("Caminho liberado!");
        }
    }
}