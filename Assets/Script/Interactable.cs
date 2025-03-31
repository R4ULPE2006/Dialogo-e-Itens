using UnityEngine;



public enum ObjectType
{
    ground, door, text, dialogue, collectable, none
}

public abstract class Interactable : MonoBehaviour
{
    public bool isInteracting;
    public ObjectType objectType;
    public abstract void Interact();
    public void CancelInteraction()
    {
        isInteracting = false;
    }
}
