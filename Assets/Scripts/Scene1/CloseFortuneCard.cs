using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseFortuneCard : MonoBehaviour
{
    private ShibaControl shibaScript;
    public CanvasGroup carpetCanvas;
    private DFortune fortuneScript;
    public GameObject FortuneTeller;
    public Button card;
    public Button card2;
    public Button card3;
    private CardInteract cardOneFlip, cardTwoFlip, cardThreeFlip;
    public GameObject carpetUI;
    
    void Start() {
        cardOneFlip = card.GetComponent<CardInteract>();
        cardTwoFlip = card2.GetComponent<CardInteract>();
        cardThreeFlip = card3.GetComponent<CardInteract>();
        fortuneScript = FortuneTeller.GetComponent<DFortune>();
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
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
        if (cardOneFlip.timer == 0 && cardTwoFlip.timer == 0 && cardThreeFlip.timer == 0)
            ClearCarpet();
        
    }

}
