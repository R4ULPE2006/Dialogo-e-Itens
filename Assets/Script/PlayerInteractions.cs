using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    PlayerRay PlayerRay;
    Interactable cancelInteractableMeu;
    void Start()
    {
        PlayerRay = GetComponent<PlayerRay>();    
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Interactable interactable;
            hit.collider.TryGetComponent<Interactable>(out interactable);
            if (interactable != null)
            {
                UIMananger.SetCursors(interactable.objectType);
                if (Input.GetButtonDown("Fire1"))
                {
                    interactable.Interact();
                    cancelInteractableMeu = interactable;
                }
            }
            else if(PlayerRay.CursorOnGround())
            {
                UIMananger.SetCursors(ObjectType.ground);

            }
            else
            {
                UIMananger.SetCursors(ObjectType.none);
                CancelInteraction();
            }
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
