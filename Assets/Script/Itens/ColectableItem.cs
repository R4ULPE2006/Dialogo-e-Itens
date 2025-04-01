using UnityEngine;

public class ColectableItem : Interactable
{

    public Item item;

    public override void Interact()
    {
        Inventory.SetItem(item);
        Debug.Log("coletou"+ item.name);
        Destroy(gameObject);
    }
}
