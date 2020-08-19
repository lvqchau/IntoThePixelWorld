using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DLumberjack : MonoBehaviour
{
    public DDialogue[] dialogues;
    private Queue<DSentence> sentences = new Queue<DSentence>();
    public TextMeshProUGUI characterBubble;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer chatBoxRenderer;
    public Sprite chatSprite;
    private string condition;
    private int dialogueIndex = 0;
    private int initialCount;
    private ShibaControl shibaScript;
    private DController dControllerScript;
    
    //each dialogue has own condition
    //Lumberjack: noPeace, donePeace, havePeace

    public void setCondition(string cond) {
        condition = cond;
        TriggerDialogue();
    }

    public string getCondition() {
        return condition;
    }

    void OnMouseDown() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (dControllerScript.isInDialogue == "none" ||
                dControllerScript.isInDialogue == "lumberjack") {
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
        condition = "noPeace";
        sentences = new Queue<DSentence>();
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
        GameObject dC = GameObject.Find("RayDetector");
        dControllerScript = dC.GetComponent<DController>();
        TriggerDialogue();
    }

    public void TriggerDialogue () {
        StartDialogues(dialogues);
    }

    private int SetDialogueIndex() {
        switch (condition) {
            case "donePeace": return 1;
            case "havePeace": return 2;
            //noPeace
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
        dControllerScript.isInDialogue = "lumberjack";
        DSentence dSentence;
        
        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;

        shibaScript.canMove = false;
        shibaScript.isMoving = false;
        
        if (sentences.Count == 0) {
            if (dialogueIndex == 1) {
                setCondition("havePeace");
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
