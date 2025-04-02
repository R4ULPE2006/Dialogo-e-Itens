using UnityEngine;
using UnityEngine.UI;

public class DialogueInteractable : Interactable
{
    public Dialogue dialogue;

    public override void Interact()
    {
        // Substituir ShowDialogue por SetDialogue
        UiManager.SetDialogue(dialogue);
    }
}
