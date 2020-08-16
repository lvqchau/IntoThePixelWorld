using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject carpetUI;

    // Start is called before the first frame update
    void Start()
    {
        carpetUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    void Resume() {

    }

    void Pause() {

    }
}
