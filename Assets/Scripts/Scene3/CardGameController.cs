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

    private int getCurrentFileIndex()
    {
        for (int i = 0; i < 4; i++)
        {
            if (fileOptions[i] == imageContainer.sprite)
            {
                return i;
            }
        }
        return 0;
    }

    void OnMouseDown()
    {
        fileIndex = getCurrentFileIndex();
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
        RenderSprite();
    }

    void RenderSprite()
    {
        imageContainer.sprite = fileOptions[fileIndex];
    }
}
