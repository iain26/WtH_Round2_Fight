using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restoration : MonoBehaviour {

	public AudioSource source;
	public AudioClip buy;

    Manager mgr;

    public delegate void Build(string stat, float changeAmount);
    public static event Build onPurchase;

    bool renovated = false;

    Color32 transpa = new Color32(255, 255, 255, 80);
    Color32 fullyVis = new Color32(255, 255, 255, 255);

    // Use this for initialization
    void Start () {
        mgr = GameObject.Find("GameMgr").GetComponent<Manager>();
        GetComponent<Image>().color = transpa;
    }

    public void Restore()
    {
        if (mgr.fMoney >= 10f && !renovated)
        {
            renovated = true;
            onPurchase("Money", -10f);
            onPurchase("Building", 1f);
            GetComponent<Image>().color = fullyVis;
            GetComponent<Button>().enabled = false;
			source.PlayOneShot (buy);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
