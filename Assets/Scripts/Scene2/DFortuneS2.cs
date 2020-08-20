using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DFortuneS2 : MonoBehaviour
{
    public DDialogue[] dialogues;
    public GameObject carpetUI;
    public CanvasGroup carpetCanvas;
    private Queue<DSentence> sentences = new Queue<DSentence>();
    public TextMeshProUGUI characterBubble;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer chatBoxRenderer;
    public Sprite chatSprite;
    public Sprite carpetSprite;
    private string condition;
    private int dialogueIndex = 0;
    private int initialCount;
    private ShibaControl shibaScript;
    private DController dControllerScript;
    private DChest chestScript;

    //each dialogue has own condition
    //FortuneTeller: noPlay, playDone, havePlay, notPlay, notWin

    public void setCondition(string cond)
    {
        condition = cond;
        TriggerDialogue();
    }

    public string getCondition()
    {
        return condition;
    }

    public void playDoneCondition()
    {
        condition = "playDone";
        TriggerDialogue();
        DisplayNextSentence();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (dControllerScript.isInDialogue == "none" ||
                dControllerScript.isInDialogue == "fortuneteller")
            {
                if (sentences.Count == initialCount)
                {
                    shibaScript.SetTargetPosition();
                    shibaScript.Move();
                    StartCoroutine("WaitForDoneMoving");
                }
                else
                {
                    DisplayNextSentence();
                }
            }
        }
    }

    IEnumerator WaitForDoneMoving()
    {
        yield return new WaitUntil(() => shibaScript.isMoving == false);
        DisplayNextSentence();
    }

    void Start()
    {
        condition = "noPlay";
        sentences = new Queue<DSentence>();
        GameObject husky = GameObject.Find("husky");
        shibaScript = husky.GetComponent<ShibaControl>();
        GameObject dC = GameObject.Find("RayDetector");
        dControllerScript = dC.GetComponent<DController>();
        GameObject chest = GameObject.Find("Chest");
        chestScript = chest.GetComponent<DChest>();
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        StartDialogues(dialogues);
    }

    private int SetDialogueIndex()
    {
        switch (condition)
        {
            case "playDone": return 1;
            //noPlay
            default: return 0;
        }
    }

    private void StartDialogues(DDialogue[] dialogues)
    {
        dialogueIndex = SetDialogueIndex();
        sentences.Clear();
        foreach (DSentence sentence in dialogues[dialogueIndex].sentences)
        {
            sentences.Enqueue(sentence);
        }
        initialCount = sentences.Count;
    }

    public void StartCarpet()
    {
        //carpetCanvas
        shibaScript.canMove = false;
        shibaScript.isMoving = false;
        Image carpetImage = carpetUI.GetComponentInChildren<Image>();
        carpetImage.sprite = carpetSprite;
        carpetCanvas.interactable = true;
        carpetCanvas.alpha = 1;
        carpetCanvas.blocksRaycasts = true;

        TriggerDialogue();
        carpetUI.SetActive(true);
        return;
    }

    public void DisplayNextSentence()
    {
        dControllerScript.isInDialogue = "fortuneteller";
        DSentence dSentence;

        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;

        shibaScript.canMove = false;
        shibaScript.isMoving = false;
        if (sentences.Count == 0)
        {
            if (dialogueIndex == 0)
            {
                chestScript.setKeyCondition();
            }
            EndDialogue();
            StartCarpet();
            return;
        }
        shibaScript.anim.SetBool("isMove", false);

        if (shibaScript.canMove == false)
        {
            dSentence = sentences.Dequeue();
            chatBoxRenderer.sprite = chatSprite;
            spriteRenderer.sprite = dSentence.characterSprite;
            characterBubble.text = dSentence.sentence;
        }
    }

    public void EndDialogue()
    {
        dControllerScript.isInDialogue = "none";
        sentences.Clear();
        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;
        shibaScript.canMove = true;
        TriggerDialogue();
    }

}
