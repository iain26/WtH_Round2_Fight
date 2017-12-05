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
        //for (int i = 0; i < deckSize; i++) {
        AddCardToDeck();
        //}
        //foreach (Card card in deck)
        //{
        //    card.transform.localScale = new Vector3(1f, 1f, 1f);
        //}
    }

    public void AddCardToDeck() {
        //float offset = 0;
        //float offsetIncre = ((float)Screen.width / 6.8f) * 1.2f;
        //for (int i = 0; i < 5; i++)
        //{
        deck.Clear();
        GameObject card = (GameObject)Instantiate(cardPrefab);
        card.name = "Card " + (count).ToString();
        card.transform.SetParent(GameObject.Find("Draw").transform);
        card.transform.position = new Vector3((float)Screen.width / 2f /*+ offset*/, (float)Screen.height / 1.8f, 0);
        card.transform.localScale = new Vector3(5f, 5f, 5f);
        deck.Add(card.GetComponent<Card>());
        count++;
        //}
        // onDraw();
    }
		
	
	// Update is called once per frame
	void Update () { 
		//foreach (Card card in deck) {
		//	card.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
  //      }
	}

}
