using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 5f;  // Distância para detectar objetos
    [SerializeField] private LayerMask interactableLayer;  // Camada dos objetos interativos
    [SerializeField] private Camera playerCamera;  // Agora a câmera pode ser atribuída diretamente no Inspector

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();  // Tenta encontrar a câmera no filho, se não atribuída
        }

        if (playerCamera == null)
        {
            Debug.LogError("Câmera não encontrada. Atribua uma câmera ao PlayerInteraction.");
        }
    }

    void Update()
    {
        // Detecta objetos interativos à frente do jogador com um Raycast
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactableLayer))
        {
            // Se o objeto é interativo, altera o crosshair
            var interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                //Debug.Log("Objeto interativo detectado: " + interactable.objectType);
                UiManager.SetCursor(interactable.objectType);  // Altera o cursor (crosshair) para o tipo do objeto
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
            else if (hit.collider.CompareTag("Ground"))  // Verifica se atingiu o chão (use a tag ou camada do chão)
            {
                // Quando o Raycast atinge o chão
                UiManager.SetCursor(ObjectType.ground);  // Muda o cursor para o tipo de chão
            }
        }
        else
        {
            // Se não detectar um objeto interativo, volta ao crosshair padrão
            UiManager.SetCursor(ObjectType.none);
            //Debug.Log("Nenhum objeto interativo detectado.");
        }


    }


    // Método de interação quando o jogador clica para interagir
    public void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactableLayer))
        {
            var interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();  // Chama o método de interação do objeto
            }
        }
    }

    // Método para desenhar a linha do Raycast no Editor (para depuração)
    void OnDrawGizmos()
    {
        if (playerCamera != null)  // Garante que a câmera foi inicializada
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interactionDistance);
        }
        else
        {
             Debug.LogWarning("playerCamera não foi inicializada. Gizmos não desenhados.");
        }
    }
}
