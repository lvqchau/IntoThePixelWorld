using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DBird : MonoBehaviour
{
    public DDialogue[] dialogues;
    private Queue<DSentence> sentences = new Queue<DSentence>();
    public TextMeshProUGUI characterBubble;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer chatBoxRenderer;
    public Sprite chatSprite;

    // public Pickup pickupScript;
    private string condition;
    private int itemCount = 0;
    private int dialogueIndex = 0;
    private int initialCount;
    private ShibaControl shibaScript;
    private ShibaControl huskyScript;
    private DController dControllerScript;
    
    //each dialogue has own condition
    //Bird: noSoup, doneSoup
    public void increaseItemCount()
    {
        itemCount++;
        if (itemCount == 2)
        {
            setKeyCondition("startSoup");
        }
    }

    public void setKeyCondition(string cond) {
        condition = cond;
        TriggerDialogue();
    }


    void OnMouseDown() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (dControllerScript.isInDialogue == "none" ||
                dControllerScript.isInDialogue == "bird") {
                if (sentences.Count == initialCount) {
                    shibaScript.SetTargetPosition();
                    shibaScript.Move();
                    huskyScript.SetTargetPosition();
                    huskyScript.Move();
                    StartCoroutine("WaitForDoneMoving");
                } else {
                    DisplayNextSentence();
                }
            }
        }
    }

    IEnumerator WaitForDoneMoving() {
        yield return new WaitUntil(() => shibaScript.isMoving == false);
        yield return new WaitUntil(() => huskyScript.isMoving == false);
        DisplayNextSentence();
    }

    IEnumerator WaitForMakingSoup()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Soup done");
        setKeyCondition("doneSoup");
        DisplayNextSentence();
    }

    void Start() {
        condition = "noSoup";
        sentences = new Queue<DSentence>();
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
        GameObject husky = GameObject.Find("husky");
        huskyScript = husky.GetComponent<ShibaControl>();
        GameObject dC = GameObject.Find("RayDetector");
        dControllerScript = dC.GetComponent<DController>();
        TriggerDialogue();
    }

    public void TriggerDialogue () {
        StartDialogues(dialogues);
    }

    private int SetDialogueIndex() {
        switch (condition) {
            case "notSoup": return 1;
            case "startSoup": return 2; //must yield Wait 5second to trigger dialogue doneSoup immediately
            case "doneSoup": return 3;
            case "haveSoup": return 4; //Add to inventory at start haveSoup
            case "makingSoup": return 5;
            //noSoup
            default: return 0;
        }
    }

    private void StartDialogues(DDialogue[] dialogues) {
        dialogueIndex = SetDialogueIndex();
        sentences.Clear();
        foreach (DSentence sentence in dialogues[dialogueIndex].sentences) {
            sentences.Enqueue(sentence);
        }    
        initialCount = sentences.Count;
    }

    private void StopMotion(ShibaControl script, bool state) {
        script.canMove = state;
        script.isMoving = state;
        script.anim.SetBool("isMove", state);
    } 

    public void DisplayNextSentence() {
        dControllerScript.isInDialogue = "bird";
        DSentence dSentence;

        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;

        StopMotion(shibaScript, false);
        StopMotion(huskyScript, false);
        
        if (sentences.Count == 0) {
            if (dialogueIndex == 0) {
                setKeyCondition("notSoup");
            }
            else if (dialogueIndex == 2)
            {
                setKeyCondition("makingSoup");
                StartCoroutine("WaitForMakingSoup");
            }
            else if (dialogueIndex == 3)
            {
                setKeyCondition("haveSoup");
            }
            // pickupScript.AddItemToInventory(soup);
            // pickupScript.RemoveItemInInventory("key");
            EndDialogue();
            return;
        }
        

        if (!shibaScript.canMove && !huskyScript.canMove) {
            dSentence = sentences.Dequeue();
            chatBoxRenderer.sprite = chatSprite;
            spriteRenderer.sprite = dSentence.characterSprite;
            characterBubble.text = dSentence.sentence;
        }
    }

    public void EndDialogue() {
        dControllerScript.isInDialogue = "none";
        sentences.Clear();
        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;
        shibaScript.canMove = true;
        huskyScript.canMove = true;
        TriggerDialogue();
    }

}
