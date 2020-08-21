using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPassword : MonoBehaviour
{
    public GameObject wrongMessage;
    public GameObject congratMessage;
    public SpriteRenderer[] slots;
    public GameObject Chest;
    private DFinalChest chestScript;
    private PickupSoup pickupScript;

    void Start()
    {
        wrongMessage.SetActive(false);
        congratMessage.SetActive(false);
        chestScript = Chest.GetComponent<DFinalChest>();
        GameObject crate = GameObject.Find("crate");
        pickupScript = crate.GetComponent<PickupSoup>();
    }

    public void CheckPassword()
    {
        if (slots[0].sprite.name != "1" || slots[1].sprite.name != "0" || slots[2].sprite.name != "3" || slots[3].sprite.name != "2")
        {
            congratMessage.SetActive(false);
            wrongMessage.SetActive(true);
        }
        else
        {
            wrongMessage.SetActive(false);
            congratMessage.SetActive(true);
            chestScript.setCondition("unlocked");
            Sprite bowls = Resources.Load<Sprite>("Bowls");
            pickupScript.AddItemToInventory(bowls);
        }
    }
}
