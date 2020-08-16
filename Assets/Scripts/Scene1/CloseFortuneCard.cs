using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseFortuneCard : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D light2D;
    private ShibaControl shibaScript;
    public CanvasGroup carpetCanvas;
    public Button card;
    public GameObject carpetUI, cardFront, cardBack;
    
    public float x, y, z;
    private float speed = 0.01f;
    private bool cardBackIsActive;
    private int timer;

    public void DeactivateClick() {
        card.interactable = false;
    }

    void Start() {
        cardBackIsActive = false;
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
    }

    void Update() {
        // if (Input.GetMouseButtonDown(0)) {
            // StartFlip();
        // }
    }

    public void StartFlip() {
        OnHoverCard();
        StartCoroutine(CalculateFlip());
    }

    public void Flip() {
        if (cardBackIsActive) {
            cardBack.SetActive(false);
            cardBackIsActive = false;
        } else {
            cardBack.SetActive(true);
            cardBackIsActive = true;
        }
    }

    IEnumerator CalculateFlip() {
        for (int i = 0; i < 180; i++) {
            yield return new WaitForSeconds(speed);
            transform.Rotate(new Vector3(x,y,z));
            timer++;

            if (timer==90 || timer==-90) {
                Flip();
            }
        }
        
        timer = 0;
    }

    public void OnHoverCard() {
        light2D.pointLightOuterRadius = 1.5f;
    }

    public void OnLeaveCard() {
        light2D.pointLightOuterRadius = 0f;
    }

    private void ClearCarpet() {
        carpetCanvas.interactable = false;
        carpetCanvas.alpha = 0;
        carpetCanvas.blocksRaycasts = false;

        carpetUI.SetActive(false);
    }

    public void CloseCarpet() {
        shibaScript.isMoving = false;
        shibaScript.canMove = false;
        //hide canvas group
        ClearCarpet();
        
        shibaScript.canMove = true;
    }

}
