using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DChest : MonoBehaviour
{
    public DDialogue[] dialogues;
    private Queue<DSentence> sentences = new Queue<DSentence>();
    public TextMeshProUGUI characterBubble;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer chatBoxRenderer;
    public Sprite chatSprite;
    private string condition;
    private int conditionCount;
    private int dialogueIndex = 0;
    private int initialCount;
    private ShibaControl shibaScript;
    private DController dControllerScript;

    //each dialogue has own condition
    //chest: notEnoughData, enoughData

    public void setKeyCondition()
    {
        Debug.Log("hey");
        conditionCount++;
        if (conditionCount < 2)
        {
            condition = "notEnoughData";
        }
        else
        {
            condition = "enoughData";
        }
        TriggerDialogue();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (dControllerScript.isInDialogue == "none" ||
                dControllerScript.isInDialogue == "chest")
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
        condition = "notEnoughData";
        conditionCount = 0;
        sentences = new Queue<DSentence>();
        GameObject husky = GameObject.Find("husky");
        shibaScript = husky.GetComponent<ShibaControl>();
        GameObject dC = GameObject.Find("RayDetector");
        dControllerScript = dC.GetComponent<DController>();
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        StartDialogues(dialogues);
    }

    private void StartDialogues(DDialogue[] dialogues)
    {
        if (condition == "enoughData")
        {
            dialogueIndex = 1;
        }
        sentences.Clear();
        foreach (DSentence sentence in dialogues[dialogueIndex].sentences)
        {
            sentences.Enqueue(sentence);
        }
        initialCount = sentences.Count;
    }

    public void DisplayNextSentence()
    {
        dControllerScript.isInDialogue = "chest";
        DSentence dSentence;

        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;

        shibaScript.canMove = false;
        shibaScript.isMoving = false;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        shibaScript.anim.SetBool("isMove", false);

        if (shibaScript.canMove == false)
        {
            dSentence = sentences.Dequeue();
            spriteRenderer.sprite = dSentence.characterSprite;
            chatBoxRenderer.sprite = chatSprite;
            characterBubble.text = dSentence.sentence;
        }
    }

    public void EndDialogue()
    {
        dControllerScript.isInDialogue = "none";
        sentences.Clear();
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;
        spriteRenderer.sprite = null;
        shibaScript.canMove = true;
        TriggerDialogue();
    }
}
