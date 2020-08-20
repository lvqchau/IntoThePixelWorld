using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseChestCarpet : MonoBehaviour
{
    private ShibaControl shibaScript;
    private ShibaControl huskyScript;
    public CanvasGroup carpetCanvas;
    private DFinalChest chestScript;
    public Sprite defaultSprite;
    public GameObject Chest;
    public SpriteRenderer[] slots;
    public GameObject carpetUI;
    public GameObject wrongMessage;


    void Start()
    {
        chestScript = Chest.GetComponent<DFinalChest>();
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
        GameObject husky = GameObject.Find("husky");
        huskyScript = husky.GetComponent<ShibaControl>();
    }

    private void ClearCarpet()
    {
        carpetCanvas.interactable = false;
        carpetCanvas.alpha = 0;
        carpetCanvas.blocksRaycasts = false;

        carpetUI.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            slots[i].sprite = defaultSprite;
        }
        wrongMessage.SetActive(false);

        shibaScript.canMove = true;
        huskyScript.canMove = true;
    }

    public void CloseCarpet()
    {
        shibaScript.isMoving = false;
        shibaScript.canMove = false;
        huskyScript.isMoving = false;
        huskyScript.canMove = false;
        ClearCarpet();
    }

}
