using UnityEngine;

public class UIMananger : MonoBehaviour
{

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

}
