using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

    public GameObject cardPrefab;
    public List<Card> deck = new List<Card>();
    public List<string> trait1 = new List<string>();
    public List<string> trait2 = new List<string>();
    int deckSize = 200;

    int count = 0;

    Selection game;

    public delegate void CardsToDraw();
    public static event CardsToDraw onDraw;

    // Use this for initialization
    void Start() {
        game = GameObject.Find("PlayerMgr").GetComponent<Selection>();
        AddCardToDeck();
    }

    public void AddCardToDeck() {
        deck.Clear();
        GameObject card = (GameObject)Instantiate(cardPrefab);
        card.name = "Card " + (count).ToString();
        card.transform.SetParent(GameObject.Find("Draw").transform);
        card.transform.position = new Vector3((float)Screen.width / 2f /*+ offset*/, (float)Screen.height / 2f, 0);
        card.transform.localScale = new Vector3(5f, 5f, 5f);
        deck.Add(card.GetComponent<Card>());
        count++;
    }
		
	
	// Update is called once per frame
	void Update () { 
	}

}
