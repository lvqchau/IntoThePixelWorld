using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
    public TextMeshProUGUI nameText;
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
        if (dialogueText.text == curSentence) {
            //wait here
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("hi");
        if (dialogue.name != "none")
            nameText.text = dialogue.name;
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
            // Debug.Log(letter);
            dialogueText.text += curSentence.ToCharArray()[i];
            // if (i == length-1) {
                // yield return new WaitForSeconds(10);
            // } else {
            yield return new WaitForSeconds(typingSpeed);    
            // }
        }
        // foreach (char letter in curSentence.ToCharArray()) {
        //     Debug.Log(letter);
        //     dialogueText.text += letter;
        //     yield return new WaitForSeconds(typingSpeed);
        // }
    }

    public void DisplayNextSentence() {
        // string sentence;
        if (sentences.Count == 0) {
            dialogueText.text = "";
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
        if (GetCurrentScene() == "Intro") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
