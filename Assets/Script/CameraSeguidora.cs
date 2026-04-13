using UnityEngine;

public class CameraSeguidora : MonoBehaviour
{
    public Transform alvo; // Arraste o Personagem_0 para cá
    public float suavidade = 0.125f;
    
    // Limites da câmera
    public float limiteMinY = 0f; // Ajuste isso no Inspector
    public float limiteMaxY = 100f; 

    private float posicaoXFixa;

    void Start()
    {
        // Trava o X para a câmera não andar para os lados
        posicaoXFixa = transform.position.x;
    }

    void LateUpdate()
    {
        if (alvo != null)
        {
            // Pega a posição do player, mas "trava" o Y entre o mínimo e o máximo
            float yLimitado = Mathf.Clamp(alvo.position.y, limiteMinY, limiteMaxY);
            
            Vector3 posicaoDesejada = new Vector3(posicaoXFixa, yLimitado, transform.position.z);
            
            // Suaviza o movimento
            transform.position = Vector3.Lerp(transform.position, posicaoDesejada, suavidade);
        }
    }
}