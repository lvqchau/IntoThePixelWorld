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

    public void changeCondition() {
        condition = "pass";
    }

    private void moveToScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnCollisionEnter2D(Collision2D character) {
        Debug.Log("Enter");
        if (condition == "noPass")
            textField.text = "You have unfinised business!";
        else if (condition == "pass") {
            moveToScene();
        }
    }

    private void OnCollisionExit2D(Collision2D character) {
        Debug.Log("Leaving");
        textField.text = "";
    }

    
}
