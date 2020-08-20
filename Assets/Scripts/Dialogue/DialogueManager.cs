using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
    public TextMeshProUGUI dialogueText;
    public Dialogue dialogue;
    private Queue<string> sentences;
    private string curSentence;
    public bool startNow = true;
    public float typingSpeed = 0.07f;

    public void TriggerDialogue () {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private string GetCurrentScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    void Start() {
        sentences = new Queue<string>();
        TriggerDialogue();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // wait here
            if (curSentence == dialogueText.text) {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        StartCoroutine(Type());
    }

    IEnumerator Type() {
        curSentence = sentences.Dequeue();
        int length = curSentence.ToCharArray().Length;
        for (int i = 0; i < length; i++) {
            dialogueText.text += curSentence.ToCharArray()[i];
            yield return new WaitForSeconds(typingSpeed);    
        }
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            dialogueText.alignment = TextAlignmentOptions.Center;
            if (GetCurrentScene() != "EndingScene")
                dialogueText.text = "NOW";
            else
                dialogueText.text = "BYE";
            EndDialogue();
            return;
        }
        dialogueText.text = "";  
        StartCoroutine(Type());
    }

    public void showVideo() {
        if (sentences.Count == 3) {
            Debug.Log("videonormal");
        }
        else if (sentences.Count == 2) {
            Debug.Log("videonormalLight");
        }
    }

    void EndDialogue() {
        if (GetCurrentScene() != "EndingScene") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            Application.Quit();
        }
    }
}
