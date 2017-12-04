using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Selection : MonoBehaviour {

	public AudioSource source;
	public AudioClip accepted;
	public AudioClip rejected;

	float offset = 0f;
	float handOffset = 0f;
	float offsetIncre = 0f;

	int drawnCards = 1;

	Vector3 deckPos;
	Vector3 tablePos;
	Vector3 handPos;
	Vector3 discardPos;
	Vector3 rejectionPos;

	bool sampled = true;

	public List<Card> cardsToDraw = new List<Card>();
	public List<Card> cardsOnTable = new List<Card>();
	public List<Card> cardsInDiscard = new List<Card>();
	public List<Card> cardsInHand = new List<Card>();
	public List<Card> cardsInRejection = new List<Card>();

    GameObject rejectedAlert;
    GameObject acceptedAlert;

    public float scale = 1.25f;
    
    float time = 1.25f;

    Deck deckClass;

    //public delegate void SaveToExternal(string actionPerformed);
    //public static event SaveToExternal onAction;

    public delegate void CommunityStats(string stat, float changeAmount);
    public static event CommunityStats onSample;

    // Use this for initialization
    void Start () {
        deckClass = GameObject.Find("GameMgr").GetComponent<Deck>();

        //sets positions cards can be on screen depending on screen size
        offsetIncre = ((float)Screen.width / 6.8f) * 1.2f;
        InitialisePositions();
		cardsToDraw = deckClass.deck;
        //DeckToTable();

        rejectedAlert = GameObject.Find("RejectedAlert");
        acceptedAlert = GameObject.Find("AcceptedAlert");

        rejectedAlert.SetActive(false);
        acceptedAlert.SetActive(false);
    }

    void InitialisePositions()
    {
        deckPos = new Vector3((float)Screen.width / 10f, (float)Screen.height / 1.25f + 1000, 0);
        tablePos = new Vector3((float)Screen.width / 7f, (float)Screen.height / 2.25f, 0);
        handPos = new Vector3((float)Screen.width / 10f, (float)Screen.height / 15f - 1000, 0);
        discardPos = new Vector3((float)Screen.width / 4f, (float)Screen.height / 1.25f + 1000, 0);
        rejectionPos = new Vector3((float)Screen.width / 2f, (float)Screen.height / 1.25f + 1000, 0);
    }

	void OnEnable(){
		Deck.onDraw += DeckToTable;
		Card.onSample += TableToHand;
	}

	void OnDisable(){
		Deck.onDraw -= DeckToTable;
		Card.onSample -= TableToHand;
	}

    public void DeckToTable()
    {
        //StartCoroutine(MoveCard(card, handPos));
        if (sampled)
        {
            // onAction("Drew Cards");
            sampled = false;
            for (int i = 0; i < 5; i++)
            {
                //cardsOnTable.Add(cardsToDraw[(cardsToDraw.Count - 1)]);
                cardsOnTable = cardsToDraw;
                cardsToDraw.Remove(cardsToDraw[(cardsToDraw.Count - 1)]);
                drawnCards++;
            }
            //offset = 0f;
        }
    }

    IEnumerator MoveCard(GameObject card, Vector3 target){
		while(card.transform.position != target)
		{
			if(card.transform.position == target){Debug.Log ("target reached");}
			card.transform.position = Vector3.Lerp (card.transform.position, target, 0.1f);
			yield return 0;
		}
		yield return 0;
	}

	void TableToHand(Card cardToHand){
		int rejectRandom = Random.Range (0, 3);
        int helpRandom = Random.Range(1, 10);
        if (rejectRandom > 0)
        {
            if (cardToHand.helpHarmStat >= helpRandom)
            {
                StartCoroutine(EnableWaitDisable(acceptedAlert, time));
                cardsInHand.Add(cardToHand.GetComponent<Card>());
                cardToHand.gameObject.transform.position = new Vector3(handPos.x + handOffset, handPos.y, 0f);
                cardToHand.gameObject.transform.SetParent(GameObject.Find("Hand").transform);
                cardToHand.gameObject.GetComponent<Button>().enabled = false;
                handOffset += offsetIncre;
                XMLWritinger.WriteToXML(cardToHand.helpHarmStat.ToString(), "False", "Helped", System.DateTime.Now.ToString());
                //cardToHand.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
                onSample("Money", Random.Range(10, 25));
                onSample("Population", 1f);
                onSample("Satisfaction", 0.05f);
                cardsOnTable.Remove(cardToHand.GetComponent<Card>());
            }
            else
            {
                StartCoroutine(EnableWaitDisable(acceptedAlert, time));
                cardsInHand.Add(cardToHand.GetComponent<Card>());
                cardToHand.gameObject.transform.position = new Vector3(handPos.x + handOffset, handPos.y, 0f);
                cardToHand.gameObject.transform.SetParent(GameObject.Find("Hand").transform);
                cardToHand.gameObject.GetComponent<Button>().enabled = false;
                handOffset += offsetIncre;
                XMLWritinger.WriteToXML(cardToHand.helpHarmStat.ToString(), "False", "Helped", System.DateTime.Now.ToString());
                onSample("Money", Random.Range(5, 10));
                onSample("Population", 1f);
                onSample("Satisfaction", -0.05f);
                cardsOnTable.Remove(cardToHand.GetComponent<Card>());
            }
		} else
        {
            XMLWritinger.WriteToXML(cardToHand.helpHarmStat.ToString(), "True", "N/A", System.DateTime.Now.ToString());
            Handheld.Vibrate();
            StartCoroutine(EnableWaitDisable(rejectedAlert, 1.667f));
            cardsInRejection.Add (cardToHand.GetComponent<Card> ());
			cardToHand.gameObject.transform.position = rejectionPos;
			cardToHand.gameObject.transform.SetParent (GameObject.Find ("Rejection").transform);
			cardToHand.gameObject.GetComponent<Button> ().enabled = false;
            cardsOnTable.Remove (cardToHand.GetComponent<Card> ());
            //Discard();
        }
		sampled = true;
	}

    IEnumerator EnableWaitDisable(GameObject alert, float time)
    {
        if(alert.name == "RejectedAlert")
        {
			source.PlayOneShot (rejected);
        }
        if (alert.name == "AcceptedAlert")
        {
			source.PlayOneShot (accepted);
        }
        alert.SetActive(true);
        yield return new WaitForSeconds(time);
        alert.SetActive(false);
    }

	public void Discard (){
        //		for(int i = 0; i < 5; i++)
        //foreach( cardOnTable in cardsOnTable){
        //Destroy(cardOnTable.gameObject);
        //cardsInDiscard.Add (cardOnTable);
        //cardOnTable.gameObject.transform.position = discardPos;
        //cardOnTable.gameObject.transform.SetParent (GameObject.Find ("Discard").transform);
        //cardOnTable.gameObject.GetComponent<Button>().enabled = false;
        //cardOnTable.drawn = false;

        Destroy(GameObject.Find("Draw").transform.GetChild(0).gameObject);
        deckClass.AddCardToDeck();
        cardsOnTable.Clear ();
       // DeckToTable();
    }

    public void PickCard()
    {
        GameObject.Find("Draw").transform.GetChild(0).GetComponent<Card>().DrawToHand();
        deckClass.AddCardToDeck();
    }

    //void ShuffleDiscardBackToDeck()
    //{
    //    foreach (Card cardInDiscard in cardsInDiscard)
    //    {
    //        cardInDiscard.transform.position = deckPos;
    //        cardInDiscard.transform.GetComponent<Button>().enabled = true;
    //        cardsToDraw.Add(cardInDiscard);
    //        cardInDiscard.transform.SetParent(GameObject.Find("Draw").transform);
    //    }
    //    cardsInDiscard.Clear();
    //}

    // Update is called once per frame
    void Update () {
        //if(cardsToDraw.Count < 5){
        //ShuffleDiscardBackToDeck();
        //}
        if(cardsToDraw[0].gameObject.activeInHierarchy)
        {
            if (sampled == true)
            {
                offset = 0;
            }
            foreach(Card cardOnTable in cardsOnTable)
            {
                if(cardOnTable != null)
                {
                    cardOnTable.gameObject.transform.position = new Vector3(tablePos.x + offset, tablePos.y, 0f);
                    cardOnTable.drawn = true;
                    cardOnTable.transform.localScale = new Vector3(scale, scale, scale);
                    cardOnTable.gameObject.transform.SetParent(GameObject.Find("Table").transform);
                    offset += offsetIncre;
                }
            }
        }
		//CheckWin ();

		//checks screen width each frame and 
		offsetIncre = ((float)Screen.width / 6.8f) * 1.2f;
	}
}
