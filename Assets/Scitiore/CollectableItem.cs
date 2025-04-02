using UnityEngine;

public class CollectableItem : Interactable
{
    public Item item;
    public override void Interact()
    {
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}