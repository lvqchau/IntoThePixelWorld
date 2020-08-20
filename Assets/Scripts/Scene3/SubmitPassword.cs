using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPassword : MonoBehaviour
{
    public GameObject wrongMessage;
    public SpriteRenderer[] slots;

    void Start()
    {
        wrongMessage.SetActive(false);
    }

    public void CheckPassword()
    {
        if (slots[0].sprite.name != "1" || slots[1].sprite.name != "0" || slots[2].sprite.name != "3" || slots[3].sprite.name != "2")
        {
            wrongMessage.SetActive(true);
        }
        else
        {
            Debug.Log("correct");
        }
    }
}
