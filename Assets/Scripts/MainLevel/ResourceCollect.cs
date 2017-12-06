using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollect : MonoBehaviour {

    public delegate void Collected(string resource);
    public static event Collected onCollected;

    // Use this for initialization
    void Start () {
		
	}

    public void Collect() {
        if(gameObject.name == "MoneyCollectable" || gameObject.name == "MoneyDrop")
            onCollected("Money");
        if (gameObject.name == "SRCollectable" || gameObject.name == "SRDrop")
            onCollected("SR");
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
