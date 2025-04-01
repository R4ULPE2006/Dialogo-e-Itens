using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class Inventory : MonoBehaviour
{

    public List<Item> itens;

    static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public static void SetItem(Item item)
    {
        if (instance == null)
            return;

        instance.itens.Add(item);
        UIMananger.SetInventoryImage(item);
    }

}
