using UnityEngine;

public class PlayerRay : MonoBehaviour
{

    public LayerMask roundLayer;
    bool cursorOnGround;
    PlayerInteractions playerInteractions;

    void Start()
    {
        playerInteractions = GetComponent<PlayerInteractions>();
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & roundLayer) != 0)
            {
                cursorOnGround = true;
                playerInteractions.CancelInteraction();
            }
            //else         {                cursorOnGround = false;            }
        }
        else
        {
            cursorOnGround = false;
        }
    }
    public bool CursorOnGround()
    {
        return cursorOnGround;
    }
}
