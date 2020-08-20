using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DCrying : MonoBehaviour
{
    public DDialogue[] dialogues;
    public Animator anim;
    private Queue<DSentence> sentences = new Queue<DSentence>();
    public TextMeshProUGUI characterBubble;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer chatBoxRenderer;
    public Pickup pickupScript;
    public Sprite chatSprite;
    private string condition;
    private int dialogueIndex = 0;
    private int initialCount;
    private ShibaControl shibaScript;
    private DLumberjack lumberScript;
    private DController dControllerScript;
    
    //each dialogue has own condition
    //Crying: noApple, doneApple, haveApple

    public void setCondition(string cond) {
        condition = cond;
        TriggerDialogue();
    }

    void OnMouseDown() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (dControllerScript.isInDialogue == "none" ||
                dControllerScript.isInDialogue == "crying") {
                if (sentences.Count == initialCount) {
                    shibaScript.SetTargetPosition();
                    shibaScript.Move();
                    StartCoroutine("WaitForDoneMoving");
                } else {
                    DisplayNextSentence();
                }
            }
        }
    }

    IEnumerator WaitForDoneMoving() {
        yield return new WaitUntil(() => shibaScript.isMoving == false);
        DisplayNextSentence();
    }

    void Start() {
        condition = "noApple";
        sentences = new Queue<DSentence>();
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
        GameObject lumberjack = GameObject.Find("Lumberjack");
        lumberScript = lumberjack.GetComponent<DLumberjack>();
        GameObject dC = GameObject.Find("RayDetector");
        dControllerScript = dC.GetComponent<DController>();
        TriggerDialogue();
    }

    public void TriggerDialogue () {
        StartDialogues(dialogues);
    }

    private int SetDialogueIndex() {
        switch (condition) {
            case "doneApple": return 1;
            case "haveApple": return 2;
            //noApple
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

    public void DisplayNextSentence() {
        dControllerScript.isInDialogue = "crying";
        DSentence dSentence;
        
        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;

        shibaScript.canMove = false;
        shibaScript.isMoving = false;
        
        if (sentences.Count == 0) {
            if (dialogueIndex == 1) {
                anim.SetBool("haveApple", true);
                pickupScript.RemoveItemInInventory("Apple");
                setCondition("haveApple");
                lumberScript.setCondition("donePeace");
            }
            EndDialogue();
            return;
        }
        shibaScript.anim.SetBool("isMove", false);

        if (shibaScript.canMove == false) {
            dSentence = sentences.Dequeue();
            spriteRenderer.sprite = dSentence.characterSprite;
            chatBoxRenderer.sprite = chatSprite;
            characterBubble.text = dSentence.sentence;
        }
    }

    public void EndDialogue() {
        dControllerScript.isInDialogue = "none";
        sentences.Clear();
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;
        spriteRenderer.sprite = null;
        shibaScript.canMove = true;
        TriggerDialogue();
    }

}
