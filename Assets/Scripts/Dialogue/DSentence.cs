using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DSentence {
    public string characterName;
    [TextArea(3, 10)]
    public string sentence;
    public Sprite characterSprite;
}
