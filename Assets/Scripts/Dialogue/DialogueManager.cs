using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
    public TextMeshProUGUI dialogueText;
    public Dialogue dialogue;
    private Queue<string> sentences;
    private string curSentence;
    public bool startNow = true;
    public float typingSpeed = 0.05f;

    // public RawImage rawImage; 
    public GameObject rawImage; 
    public VideoClip videoNormal;
    public VideoClip videoGlitch;
    public GameObject videoObject;
    public AudioSource audioSource;

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
        if (GetCurrentScene() == "Intro") {
            if (sentences.Count == 3 || sentences.Count == 2) yield return new WaitForSeconds(4);    
        }
        curSentence = sentences.Dequeue();
        int length = curSentence.ToCharArray().Length;
        for (int i = 0; i < length; i++) {
            dialogueText.text += curSentence.ToCharArray()[i];
            yield return new WaitForSeconds(typingSpeed);    
        }
    }

    public void DisplayNextSentence() {
        if (GetCurrentScene() == "Intro") {
            if (sentences.Count == 3) {
                StartCoroutine(ShowVideo("normal"));
            } else if (sentences.Count == 2) {
                StartCoroutine(ShowVideo("light"));
            }   
        }
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

    IEnumerator ShowVideo(string type) {
        rawImage.SetActive(true);
        VideoPlayer videoPlayer = videoObject.GetComponent<VideoPlayer>();
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        if (type == "normal") {
            videoPlayer.clip = videoNormal;
        } else {
            videoPlayer.clip = videoGlitch;
        }
        videoPlayer.Play();
        yield return new WaitForSeconds(4);
        videoPlayer.Pause();
        rawImage.SetActive(false);
    }

    void EndDialogue() {
        if (GetCurrentScene() != "EndingScene") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            Application.Quit();
        }
    }
}
