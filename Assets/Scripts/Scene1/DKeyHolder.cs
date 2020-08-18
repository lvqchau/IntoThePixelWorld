using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DKeyHolder : MonoBehaviour
{
    public DDialogue[] dialogues;
    private Queue<DSentence> sentences = new Queue<DSentence>();
    public TextMeshProUGUI characterBubble;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer chatBoxRenderer;
    public Sprite chatSprite;

    public Sprite apple;
    public Pickup pickupScript;
    private string condition;

    private int dialogueIndex = 0;
    private int initialCount;
    private ShibaControl shibaScript;
    private DController dControllerScript;
    
    //each dialogue has own condition
    //KeyHolder: noKey, doneKey, haveKey

    public void setKeyCondition() {
        condition = "doneKey";
        TriggerDialogue();
        Debug.Log("gettingKeyInKeyHolder");
        
    }

    //DKeyHolder.setKeyCondition()

    void OnMouseDown() {
        // if (!EventSystem.current.IsPointerOverGameObject()) {
            if (dControllerScript.isInDialogue == "none" ||
                dControllerScript.isInDialogue == "keyholder") {
                if (sentences.Count == initialCount) {
                    shibaScript.SetTargetPosition();
                    shibaScript.Move();
                    StartCoroutine("WaitForDoneMoving");
                } else {
                    DisplayNextSentence();
                }
            }
        // }
    }

    IEnumerator WaitForDoneMoving() {
        yield return new WaitUntil(() => shibaScript.isMoving == false);
        DisplayNextSentence();
    }

    void Start() {
        condition = "noKey";
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

    private void StartDialogues(DDialogue[] dialogues) {
        if (condition == "doneKey") {
            dialogueIndex = 1;
        } else if (condition == "haveKey") {
            dialogueIndex = 2;
        }
        sentences.Clear();
        foreach (DSentence sentence in dialogues[dialogueIndex].sentences) {
            sentences.Enqueue(sentence);
        }    
        initialCount = sentences.Count;
    }

    public void DisplayNextSentence() {
        dControllerScript.isInDialogue = "keyholder";
        DSentence dSentence;

        spriteRenderer.sprite = null;
        characterBubble.text = "";
        chatBoxRenderer.sprite = null;

        shibaScript.canMove = false;
        shibaScript.isMoving = false;
        
        if (sentences.Count == 0) {
            if (dialogueIndex == 1) {
                pickupScript.AddItemToInventory(apple);
                pickupScript.RemoveItemInInventory("key");
            }
            EndDialogue();
            return;
        }
        shibaScript.anim.SetBool("isMove", false);

        if (shibaScript.canMove == false) {
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
        TriggerDialogue();
    }

}
