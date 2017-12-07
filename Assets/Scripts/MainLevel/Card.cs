using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour {
    
	public string hairColour;
	public string skinColour;
	public string shirtColour;

    public int helpHarmStat;
    public string statToEffect;

	public bool drawn = false;

	public delegate void CardToSample(Card card);
	public static event CardToSample onSample;

	// Use this for initialization
	void Start () {

        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                statToEffect = "Money";
                break;
            case 1:
                statToEffect = "Food";
                break;
        }
    }

    int CheckHelpStats(string trait1, string trait2)
    {
        switch (trait1)
        {
            case "Cyan":
                switch (trait2)
                {
                    case "Blue":
                        return 9;
                        break;
                    case "Orange":
                        return 7;
                        break;
                    case "Yellow":
                        return 5;
                        break;
                }
                break;
            case "Green":
                switch (trait2)
                {
                    case "Blue":
                        return 7;
                        break;
                    case "Orange":
                        return 5;
                        break;
                    case "Yellow":
                        return 3;
                        break;
                }
                break;
            case "Pink":
                switch (trait2)
                {
                    case "Blue":
                        return 5;
                        break;
                    case "Orange":
                        return 3;
                        break;
                    case "Yellow":
                        return 1;
                        break;
                }
                break;
        }
        return 0;
    }
	
	// Update is called once per frame
	void Update () {
		//if (!drawn) {
  //          transform.GetChild(4).gameObject.SetActive(true);
		//} else {
  //          //GetComponent<Image> ().sprite = front;
  //          transform.GetChild(4).gameObject.SetActive(false);
  //      }
        helpHarmStat = CheckHelpStats(hairColour, shirtColour);
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
