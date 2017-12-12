using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollect : MonoBehaviour {

    public AudioSource source;
    public AudioClip collect;

    public delegate void Collected(string resource);
    public static event Collected onCollected;

    // Use this for initialization
    void Start () {
        source = GameObject.Find("EventSystem").GetComponent<AudioSource>();
	}

    public void Collect() {
        source.PlayOneShot(collect);
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
