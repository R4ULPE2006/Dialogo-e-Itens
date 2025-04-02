using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 5f;  // Dist�ncia para detectar objetos
    [SerializeField] private LayerMask interactableLayer;  // Camada dos objetos interativos
    [SerializeField] private Camera playerCamera;  // Agora a c�mera pode ser atribu�da diretamente no Inspector

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();  // Tenta encontrar a c�mera no filho, se n�o atribu�da
        }

        if (playerCamera == null)
        {
            Debug.LogError("C�mera n�o encontrada. Atribua uma c�mera ao PlayerInteraction.");
        }
    }

    void Update()
    {
        // Detecta objetos interativos � frente do jogador com um Raycast
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactableLayer))
        {
            // Se o objeto � interativo, altera o crosshair
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
            else if (hit.collider.CompareTag("Ground"))  // Verifica se atingiu o ch�o (use a tag ou camada do ch�o)
            {
                // Quando o Raycast atinge o ch�o
                UiManager.SetCursor(ObjectType.ground);  // Muda o cursor para o tipo de ch�o
            }
        }
        else
        {
            // Se n�o detectar um objeto interativo, volta ao crosshair padr�o
            UiManager.SetCursor(ObjectType.none);
            //Debug.Log("Nenhum objeto interativo detectado.");
        }


    }


    // M�todo de intera��o quando o jogador clica para interagir
    public void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactableLayer))
        {
            var interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();  // Chama o m�todo de intera��o do objeto
            }
        }
    }

    // M�todo para desenhar a linha do Raycast no Editor (para depura��o)
    void OnDrawGizmos()
    {
        if (playerCamera != null)  // Garante que a c�mera foi inicializada
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interactionDistance);
        }
        else
        {
             Debug.LogWarning("playerCamera n�o foi inicializada. Gizmos n�o desenhados.");
        }
    }
}
