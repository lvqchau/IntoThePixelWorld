using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class DMoveScene : MonoBehaviour
{
    public TextMeshProUGUI textField;
    private string condition = "noPass";
    public SceneTransition sceneTransition;

    public void changeCondition() {
        condition = "pass";
    }

    private void moveToScene() {
        sceneTransition.PlayAnimationTransition();
    }

    private void OnCollisionEnter2D(Collision2D character) {
        if (condition == "noPass")
            textField.text = "You have unfinised business!";
        else if (condition == "pass") {
            moveToScene();
        }
    }

    private void OnCollisionExit2D(Collision2D character) {
        textField.text = "";
    }

    
}
