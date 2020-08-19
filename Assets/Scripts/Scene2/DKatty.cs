using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DKatty : MonoBehaviour
{
    public DDialogue[] dialogues;
    private Queue<DSentence> sentences = new Queue<DSentence>();
    public TextMeshProUGUI characterBubble;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer chatBoxRenderer;
    public Sprite chatSprite;
    public Sprite paper;
    public PickUpHusky pickupScript;
    private string condition;
    private int dialogueIndex = 0;
    private int initialCount;
    private ShibaControl shibaScript;
    private DController dControllerScript;

    //each dialogue has own condition
    //Katty: noWool, doneWool, haveWool

    public void setKeyCondition()
    {
        Debug.Log("done wool");
        condition = "doneWool";
        TriggerDialogue();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (dControllerScript.isInDialogue == "none" ||
                dControllerScript.isInDialogue == "katty")
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
        condition = "noWool";
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
        if (condition == "doneWool")
        {
            dialogueIndex = 1;
        }
        else if (condition == "haveWool")
        {
            dialogueIndex = 2;
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
        dControllerScript.isInDialogue = "katty";
        DSentence dSentence;

        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;

        shibaScript.canMove = false;
        shibaScript.isMoving = false;

        if (sentences.Count == 0)
        {
            if (dialogueIndex == 1)
            {
                pickupScript.AddItemToInventory(paper);
                pickupScript.RemoveItemInInventory("wool");
            }
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
