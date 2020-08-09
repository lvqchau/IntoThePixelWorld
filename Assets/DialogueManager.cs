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
    // private Queue<GameObject> videos;

    private string GetCurrentScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name; //Intro, Menu
    }

    void Start() {
        sentences = new Queue<string>();
        // videos = new Queue<GameObject>();
    }

    public void StartDialogue(Dialogue dialogue) {
        if (dialogue.name != "none")
            nameText.text = dialogue.name;
        sentences.Clear();
        // videos.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        // foreach (GameObject video in dialogue.videos) {
        //     videos.Enqueue(video);
        // }

        InvokeRepeating("DisplayNextSentence", 1f, 5f);
    }

    public void DisplayNextSentence() {
        string sentence;
        if (sentences.Count == 0) {
            EndDialogue();
            CancelInvoke("DisplayNextSentence");
            return;
        }
        // if (GetCurrentScene() == "Intro") {
        //     showVideo();
        // }
        sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void showVideo() {
        // GameObject video = videos.Dequeue();
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
