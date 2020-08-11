using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void HoverSound() {
        myFx.volume = 0.9f;
        myFx.PlayOneShot(hoverFx);
    }

    public void ClickSound() {
        myFx.volume = 0.3f;
        myFx.PlayOneShot(clickFx);
    }
}
