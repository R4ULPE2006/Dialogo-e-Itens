using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    PlayerRay PlayerRay;
    Interactable cancelInteractableMeu;

    private ObjectType currentCursorType;//= ObjectType.none; // Armazena o cursor atual
    void Start()
    {
        PlayerRay = GetComponent<PlayerRay>();
    }

    /*
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        ObjectType newCursorType = ObjectType.none; // Padrão, caso nada seja encontrado

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                newCursorType = interactable.objectType; // Define o novo tipo de cursor

                if (Input.GetButtonDown("Fire1"))
                {
                    interactable.Interact();
                    cancelInteractableMeu = interactable;
                }
            }
        }

        // Só atualiza se o cursor for diferente
        if (newCursorType != currentCursorType)
        {
            currentCursorType = newCursorType;
            UIMananger.SetCursors(currentCursorType);
        }
        /*
if (interactable != null)
{
    UIMananger.SetCursors(interactable.objectType);
    if (Input.GetButtonDown("Fire1"))
    {
        interactable.Interact();
        cancelInteractableMeu = interactable;
    }
}
//else if(PlayerRay.CursorOnGround())            {                UIMananger.SetCursors(ObjectType.ground);            }
else
{
    UIMananger.SetCursors(ObjectType.none);
    CancelInteraction();
}
}*/

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        ObjectType newCursorType = ObjectType.none; // Assume que o cursor deve ser resetado

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                newCursorType = interactable.objectType;

                if (Input.GetButtonDown("Fire1"))
                {
                    interactable.Interact();
                    cancelInteractableMeu = interactable;
                }
            }
        }

        //  **Log para checar se o Update() está rodando**
        Debug.Log($"Raycast detectou: {newCursorType}");

        // **Forçar atualização do cursor SEMPRE**
        if (newCursorType != currentCursorType)
        {
            currentCursorType = newCursorType;
            UIMananger.SetCursors(currentCursorType);
        }
    }



    public void CancelInteraction()
    {
        if (cancelInteractableMeu != null)
        {
            cancelInteractableMeu.isInteracting = false;
            cancelInteractableMeu = null;
        }
    }

}
