using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSoundScript : MonoBehaviour
{
    void Start()
    {

    }

    private static BgSoundScript instance = null;
    public static BgSoundScript Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
