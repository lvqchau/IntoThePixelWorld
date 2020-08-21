using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public SceneTransition sceneTransition;
    public void PlayGame() {
        sceneTransition.PlayAnimationTransition();
    }
 
    public void QuitGame() {
        Application.Quit();
    }
}