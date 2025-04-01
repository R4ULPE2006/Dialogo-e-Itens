using UnityEngine;
using UnityEngine.UI;

public class UIMananger : MonoBehaviour
{
    /*
     * 
     * 
    //SE FOR UMA TEXTURA

    public Image[] inventoryImages;

    public static UIMananger instance;
    public Texture2D[] cursors;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void SetCursors(ObjectType objectType)
    {    
        if (instance == null)
        return;
        Cursor.SetCursor(instance.cursors[(int)objectType], Vector2.zero, CursorMode.Auto);
        
    }

        public static void SetInventoryImage(Item item)
    {
        if (instance == null) 
            return;
        for (int i = 0; i< instance.inventoryImages.Length; i++)
        {
            if (!instance.inventoryImages[i].gameObject.activeInHierarchy)
            {

            }
        }
    }
     
     
     */

    //SE FOR UM SPRITE DE UMA TEXTURA

    public Image[] inventoryImages;

    public static UIMananger instance;
    public Sprite[] cursors;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /*
    public static void SetCursors(ObjectType objectType)
    {
        if (instance == null)
            return;

        Sprite sprite = instance.cursors[(int)objectType];

        if (sprite == null)
            return;

        // Criando a textura a partir do Sprite
        Texture2D texture = SpriteToTexture(sprite);

        // Definindo o cursor
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }
    */

    public static void SetCursors(ObjectType objectType)
    {
        if (instance == null)
        {
            Debug.LogError("UIMananger instance is null!");
            return;
        }

        if (instance.cursors == null || instance.cursors.Length == 0)
        {
            Debug.LogError("Cursors array is empty!");
            return;
        }

        CreateEmptyCursor(); //  Garante que temos uma textura vazia para reset

        Texture2D texture = null;

        if ((int)objectType >= 0 && (int)objectType < instance.cursors.Length)
        {
            Sprite sprite = instance.cursors[(int)objectType];

            if (sprite != null)
            {
                texture = SpriteToTexture(sprite);
            }
        }

        if (texture == null)
        {
            Debug.LogWarning("Cursor inválido ou nulo, resetando para cursor vazio.");
            texture = emptyCursor; //  Usa a textura vazia ao invés de `null`
        }

        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        Debug.Log("Setting cursor: " + objectType);
    }




    private static Texture2D SpriteToTexture(Sprite sprite)
    {
        if (sprite == null)
            return null;

        // Criando uma nova textura baseada no sprite
        Texture2D newTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.rect.x, (int)sprite.rect.y,
            (int)sprite.rect.width, (int)sprite.rect.height
        );
        newTexture.SetPixels(pixels);
        newTexture.Apply();

        return newTexture;
    }

    public static void SetInventoryImage(Item item)
    {
        if (instance == null)
            return;
        for (int i = 0; i < instance.inventoryImages.Length; i++)
        {
            if (!instance.inventoryImages[i].gameObject.activeInHierarchy)
            {
                instance.inventoryImages[i].sprite = item.itemImage;
                instance.inventoryImages[i].gameObject.SetActive(true);
                break;
            }
        }
    }
    private static Texture2D emptyCursor; // Textura vazia para resetar o cursor

    private static void CreateEmptyCursor()
    {
        if (emptyCursor == null)
        {
            emptyCursor = new Texture2D(1, 1);
            emptyCursor.SetPixel(0, 0, Color.clear);
            emptyCursor.Apply();
        }
    }

}
