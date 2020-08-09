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
    private Queue<string> sentences;

    private string GetCurrentScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name; //Intro, Menu
    }

    void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        if (dialogue.name != "none")
            nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        InvokeRepeating("DisplayNextSentence", 1f, 5f);
    }

    public void DisplayNextSentence() {
        string sentence;
        if (sentences.Count == 0) {
            EndDialogue();
            CancelInvoke("DisplayNextSentence");
            return;
        }
        sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void showVideo() {
        if (sentences.Count == 3) {
            //show video for 3s
            Debug.Log("videonormal");
        }
        else if (sentences.Count == 2) {
            //show video light for 5s
            Debug.Log("videonormalLight");
        }
    }

    void EndDialogue() {
        if (GetCurrentScene() == "Intro") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        Debug.Log("end dialogue");
    }
}
