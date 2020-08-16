using UnityEngine;
using UnityEngine.UI;

public class SimpleGraphic : Graphic {
    protected override void Start() {
        material.color = Color.white;
    }
}