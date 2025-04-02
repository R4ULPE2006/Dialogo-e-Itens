using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectType
{
    ground, door, text, dialogue, collectable, none
}

public class UiManager : MonoBehaviour
{

    [Tooltip("The order of the cursors must be the same as the ObjectType enum\n" +
        "0. ground\n" +
        "1. door\n" +
        "2. text\n" +
        "3. dialogue\n" +
        "4. collectable\n" +
        "5. none\n")]
    public Button sairButton; // Arraste o botão no Inspector

    public Sprite[] cursors;

    public Sprite playerPortrait;
    public Image crosshairImage; // Referência à imagem do crosshair

    [Tooltip("Interactions")]
    public GameObject interactionPanel;
    public TMP_Text interactionText;
    public Image portrait;
    public TMP_Text[] answersTexts;

    [Tooltip("Inventory")]
    public Image[] inventoryImages;

    public static UiManager instance;

    public bool inDialogue = false;

    public TMP_Text infoText;

    TextInteractable textInteractable;


    void Awake()
    {
        // Garantir que só exista uma instância do UiManager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Para não destruir entre cenas, se necessário
        sairButton.onClick.AddListener(FinishDialogue);
    }


    public static void SetCursor(ObjectType objectType)
    {
        if (instance == null)
        {
            Debug.LogError("UiManager instance is null!");
            return;
        }

        if (instance.crosshairImage == null)
        {
            Debug.LogError("crosshairImage is not set in the inspector!");
            return;
        }

       // Debug.Log("Mudando crosshair para " + objectType);  // Log para ver se o código entra aqui

        switch (objectType)
        {
            case ObjectType.ground:
                //Debug.Log("Configuring crosshair for ground.");
                instance.crosshairImage.sprite = instance.cursors[0];
                break;

            case ObjectType.door:
                //Debug.Log("Configuring crosshair for door.");
                instance.crosshairImage.sprite = instance.cursors[1];
                break;

            case ObjectType.text:
                //Debug.Log("Configuring crosshair for text.");
                instance.crosshairImage.sprite = instance.cursors[2];
                break;

            case ObjectType.dialogue:
                //Debug.Log("Configuring crosshair for dialogue.");
                instance.crosshairImage.sprite = instance.cursors[3];
                break;

            case ObjectType.collectable:
                //Debug.Log("Configuring crosshair for collectable.");
                instance.crosshairImage.sprite = instance.cursors[4];
                break;

            default:
                //Debug.Log("Configuring default crosshair.");
                instance.crosshairImage.sprite = instance.cursors[5];
                break;
        }

        instance.crosshairImage.gameObject.SetActive(true);
    }



    public static void SetText(TextInteractable interactable)
    {
        if (instance == null)
            return;

        EnableMouse();
        FindFirstObjectByType<RotacaoCamera>().podeRotacionar = false; 

        instance.portrait.sprite = instance.playerPortrait;
        instance.sairButton.gameObject.SetActive(true);
        if (interactable.conditionalItem != null)
        {
            // Debug.Log("Tem conditional item");
            if (Inventory.HasItem(interactable.conditionalItem))
            {
               // Debug.Log("Tem item");
                instance.interactionText.text = interactable.conditionalText;
                if (interactable.useItem)
                {
                    Inventory.UseItem(interactable.conditionalItem);
                    interactable.onUseItem.Invoke();
                    if (interactable.newItem != null)
                        Inventory.SetItem(interactable.newItem);
                }

            }
            else
            {
               // Debug.Log("Não tem item");
                instance.interactionText.text = interactable.text;
            }
        }
        else
        {
           // Debug.Log("Não tem conditional item");
            instance.interactionText.text = interactable.text;
        }
        if (interactable.audioClip != null)
            SoundManager.PlaySound(interactable.audioClip);

        instance.interactionPanel.SetActive(true);
        instance.textInteractable = interactable;
    }
    public static void FinishText()
    {
        if (instance == null)
            return;

        instance.inDialogue = false;

        DisableMouse();
        FindFirstObjectByType<RotacaoCamera>().podeRotacionar = true;

        // Desativa o painel de interação e o botão "Sair"
        instance.interactionPanel.SetActive(false);
        instance.sairButton.gameObject.SetActive(false);
    }


    public static void DisableInteraction()
    {
        if (instance == null)
            return;

        instance.interactionPanel.SetActive(false);
        if (instance.textInteractable != null)
            instance.textInteractable.isInteracting = false;
    }

    public static void SetInventoryImage(Item item)
    {
        if (instance == null)
            return;

        DisableInteraction();
        SetInfoText(item.description);
        if (SoundManager.instance != null)
            SoundManager.PlaySound(SoundManager.instance.itemCollect);

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


    public static void SetDialogue(Dialogue dialogue)
    {
        if (instance == null)
            return;

        instance.sairButton.gameObject.SetActive(true);

        if (dialogue.isEnd)
        {
            FinishDialogue();
            return;
        }

        if (SoundManager.instance != null)
            SoundManager.PlaySound(SoundManager.instance.introDialogue);

        instance.inDialogue = true;
        SetCursor(ObjectType.none);
        DisableInteraction();
        instance.portrait.sprite = dialogue.portrait;
        instance.interactionText.text = dialogue.dialogueText;

        EnableMouse(); // Habilita o mouse
        FindFirstObjectByType<RotacaoCamera>().podeRotacionar = false; // Bloqueia a rotação da câmera


        for (int i = 0; i < instance.answersTexts.Length; i++)
        {
            instance.answersTexts[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < instance.answersTexts.Length; i++)
        {
            if (i < dialogue.answers.Length)
            {
                if (dialogue.answers[i].conditionalItem)
                {
                    if (!Inventory.HasItem(dialogue.answers[i].conditionalItem))
                    {
                        continue;
                    }
                }
                instance.answersTexts[i].text = dialogue.answers[i].playerAnswer;
                instance.answersTexts[i].GetComponent<AnswerTextButton>().SetButton(dialogue.answers[i]);
                instance.answersTexts[i].gameObject.SetActive(true);
            }
            else
            {
                instance.answersTexts[i].gameObject.SetActive(false);
            }
        }
        instance.interactionPanel.SetActive(true);
    }

    public static void FinishDialogue()
    {
        if (instance == null)
            return;

        if (SoundManager.instance != null)
            SoundManager.PlaySound(SoundManager.instance.finishDialogue);

        instance.inDialogue = false;

        // Fecha o painel de interação
        instance.interactionPanel.SetActive(false);

        // Bloqueia o mouse e libera a rotação da câmera
        DisableMouse();
        FindFirstObjectByType<RotacaoCamera>().podeRotacionar = true;
        instance.sairButton.gameObject.SetActive(false); // Esconde o botão

        for (int i = 0; i < instance.answersTexts.Length; i++)
        {
            instance.answersTexts[i].gameObject.SetActive(false);
        }

        DisableInteraction();
    }


    public static void SetInfoText(string text)
    {
        if (instance == null)
            return;


        instance.infoText.text = text;
        instance.infoText.gameObject.SetActive(true);

        instance.Invoke("DisableInfoText", 4f);
    }

    void DisableInfoText()
    {

        instance.infoText.gameObject.SetActive(false);
    }

    public static void RemoveInventoryImage(Item item)
    {
        if (instance == null)
            return;

        for (int i = 0; i < instance.inventoryImages.Length; i++)
        {
            if (instance.inventoryImages[i].sprite == item.itemImage)
            {
                instance.inventoryImages[i].gameObject.SetActive(false);
                break;
            }
        }
    }

    public static void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
