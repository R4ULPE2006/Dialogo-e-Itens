using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]

public class Dialogue : ScriptableObject
{

    public string dialogueText;
    public string playerAnswer;
    public Sprite portrait;
    public bool isEnd;
    public Item conditionItem; //Caso seja necess�rio um item
    public Dialogue[] answers;

}
