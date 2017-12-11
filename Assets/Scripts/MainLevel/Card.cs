using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {
    
	public string hairColour;
	public string skinColour;
	public string shirtColour;
    //public string Colour;
    //public string skinColour;
    //public string shirtColour;
    //public string hairColour;
    //public string skinColour;
    //public string shirtColour;

    //Dictionary<string, string> Matrix1 = new Dictionary<string, string>();
    //Dictionary<string, string> Matrix2 = new Dictionary<string, string>();

    public int helpHarmStat;

	public bool drawn = false;

	public delegate void CardToSample(Card card);
	public static event CardToSample onSample;

	// Use this for initialization
	void Start () {
        
    }

    public int CheckHelpStats(string trait1, string trait2)
    {
        switch (trait1)
        {
            case "1":
                switch (trait2)
                {
                    case "1":
                        return 9;
                        break;
                    case "2":
                        return 7;
                        break;
                    case "3":
                        return 5;
                        break;
                }
                break;
            case "2":
                switch (trait2)
                {
                    case "1":
                        return 7;
                        break;
                    case "2":
                        return 5;
                        break;
                    case "3":
                        return 3;
                        break;
                }
                break;
            case "3":
                switch (trait2)
                {
                    case "1":
                        return 5;
                        break;
                    case "2":
                        return 3;
                        break;
                    case "3":
                        return 1;
                        break;
                }
                break;
        }
        return 0;
    }
	
	// Update is called once per frame
	void Update () {
        //helpHarmStat = CheckHelpStats(hairColour, shirtColour);
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
