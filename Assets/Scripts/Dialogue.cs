using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Pass into Dialogue when we want new Dialogue
public class Dialogue {
    public string name; //name of npc
    public GameObject character;
    [TextArea(3, 10)]
    public string[] sentences;
    // public GameObject[] videos;
}
