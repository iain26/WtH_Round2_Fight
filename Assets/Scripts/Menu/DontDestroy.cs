using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    public int characterHair;
    public int characterSkin;
    public int characterShirt;

    //   // Use this for initialization
    void Start()
    {

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //// Update is called once per frame
    void Update()
    {

    }
}
