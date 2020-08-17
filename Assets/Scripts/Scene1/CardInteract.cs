using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInteract : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D light2D;
    private ShibaControl shibaScript;
    public CanvasGroup carpetCanvas;
    private DFortune fortuneScript;
    public GameObject FortuneTeller;
    public Button card;
    public Button card2;
    public Button card3;
    private CardInteract cardOneFlip, cardTwoFlip, cardThreeFlip;
    public GameObject carpetUI, cardFront, cardBack;
    
    public float x, y, z;
    private float speed = 0.01f;
    public bool cardBackIsActive;
    public int timer;

    public void DeactivateClick() {
        card.interactable = false;
    }

    void Start() {
        cardBackIsActive = false;
        cardOneFlip = card.GetComponent<CardInteract>();
        cardTwoFlip = card2.GetComponent<CardInteract>();
        cardThreeFlip = card3.GetComponent<CardInteract>();
        fortuneScript = FortuneTeller.GetComponent<DFortune>();
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
    }

    public void StartFlip() {
        if (!cardBackIsActive) cardBackIsActive = true;
        ButtonFX cardSound = card.GetComponent<ButtonFX>();
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

}
