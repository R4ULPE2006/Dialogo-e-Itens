using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item")]

public class Item : ScriptableObject
{

    public string itemName;
    public Sprite itemImage;
    public string description;

}
