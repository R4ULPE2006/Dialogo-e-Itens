using UnityEngine;

public class CollectableItem : Interactable
{
    public Item item;

    public override void Interact()
    {
        //Debug.Log("Tentando coletar o item: " + item.name);
        Inventory.SetItem(item); 
        Destroy(gameObject);
    }
}
