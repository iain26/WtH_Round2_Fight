using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {
    
	public string hairColour;
	public string skinColour;
	public string shirtColour;

    public int helpHarmStat;

	public bool drawn = false;

	public delegate void CardToSample(Card card);
	public static event CardToSample onSample;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
       
    }

	public void DrawToHand (){
		if(drawn)
        {
            GetComponent<Image>().enabled = false;
            onSample (this);
        }
		if (!drawn) {
			//onDraw ();
		} 
	}
}
