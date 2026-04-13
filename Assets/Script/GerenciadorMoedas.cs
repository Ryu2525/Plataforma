using UnityEngine;
using TMPro;

public class GerenciadorMoedas : MonoBehaviour
{
    public int totalMoedas = 0;
    public TextMeshProUGUI textoMoedaUI;

    void Start()
    {
        // Começa a interface limpa ou com 0 moedas
        AtualizarInterfaceMoedas();
    }

    public void AdicionarMoeda()
    {
        totalMoedas++;
        AtualizarInterfaceMoedas();
    }

    void AtualizarInterfaceMoedas()
    {
        string iconesMoedas = "";

        // O 'for' vai repetir a imagem a quantidade de vezes que você coletou
        for (int i = 0; i < totalMoedas; i++)
        {
            // Adiciona a tag da moeda para cada moeda coletada
            iconesMoedas += "<sprite name=\"Moeda\"> ";
        }

        // Se não tiver moedas, o texto fica vazio. Se tiver 3, aparecem 3 ícones.
        textoMoedaUI.text = iconesMoedas;
    }
}