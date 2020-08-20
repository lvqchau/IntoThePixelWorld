using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameController : MonoBehaviour
{
    private int fileIndex;
    public SpriteRenderer imageContainer;
    public Sprite[] fileOptions;


    void Start()
    {
        fileIndex = 0;
    }

    void OnMouseDown()
    {
        if (gameObject.name == "downbutton")
        {
            if (fileIndex == 0)
            {
                fileIndex = 3;
            }
            else
            {
                fileIndex = fileIndex - 1;
            }
        }
        else
        {
            fileIndex = (fileIndex + 1) % 4;
        }
        Debug.Log(fileIndex);
        RenderSprite();
    }

    void RenderSprite()
    {
        imageContainer.sprite = fileOptions[fileIndex];
    }
}
