using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseFortuneCard : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D light2D;
    private ShibaControl shibaScript;
    public CanvasGroup carpetCanvas;
    private DFortune fortuneScript;
    public GameObject FortuneTeller;
    public Button card;
    public Button card2;
    public Button card3;
    private CloseFortuneCard cardOneFlip, cardTwoFlip, cardThreeFlip;
    public GameObject carpetUI, cardFront, cardBack;
    
    public float x, y, z;
    private float speed = 0.01f;
    public bool cardBackIsActive;
    private bool isFlip;
    private int timer;

    public void DeactivateClick() {
        card.interactable = false;
    }

    void Start() {
        isFlip = false;
        cardBackIsActive = false;
        cardOneFlip = card.GetComponent<CloseFortuneCard>();
        cardTwoFlip = card2.GetComponent<CloseFortuneCard>();
        cardThreeFlip = card3.GetComponent<CloseFortuneCard>();
        fortuneScript = FortuneTeller.GetComponent<DFortune>();
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
    }

    void Update() {
        // if (Input.GetMouseButtonDown(0)) {
            // StartFlip();
        // }
    }

    public void StartFlip() {
        if (!cardBackIsActive) cardBackIsActive = true;
        ButtonFX cardSound = card.GetComponent<ButtonFX>();
        isFlip = true;
        if (cardTwoFlip.cardBackIsActive || cardThreeFlip.cardBackIsActive) {
            cardSound.myFx.Stop();
            
        } else {
            // fortuneScript.setCondition("notWin");
            fortuneScript.setCondition("haveChosen");
            StartCoroutine(CalculateFlip());
        }
    }

    public void Flip() {
        
        if (!cardBackIsActive) {
            cardBack.SetActive(false);
        } else {
            cardBack.SetActive(true);
        }
    }

    IEnumerator CalculateFlip() {
        for (int i = 0; i < 180; i++) {
            yield return new WaitForSeconds(speed);
            transform.Rotate(new Vector3(x,y,z));
            timer++;
            isFlip = true;
            cardBackIsActive = true;
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
        if (fortuneScript.getCondition() == "noPlay") {
            fortuneScript.setCondition("notPlay");
        }
        fortuneScript.TriggerDialogue();
        carpetCanvas.interactable = false;
        carpetCanvas.alpha = 0;
        carpetCanvas.blocksRaycasts = false;

        carpetUI.SetActive(false);

        shibaScript.canMove = true;
    }

    public void CloseCarpet() {
        shibaScript.isMoving = false;
        shibaScript.canMove = false;
        Debug.Log(cardOneFlip.timer);
        Debug.Log(cardTwoFlip.timer);
        Debug.Log(cardThreeFlip.timer);
        if (cardOneFlip.timer == 0 && cardTwoFlip.timer == 0 && cardThreeFlip.timer == 0)
            ClearCarpet();
        
    }

}
