using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Selection : MonoBehaviour {

	public AudioSource source;
	public AudioClip acceptedClip;
	public AudioClip rejectedClip;

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

    int randObjective;

    float time = 1.25f;

    Deck deckClass;

    //public delegate void SaveToExternal(string actionPerformed);
    //public static event SaveToExternal onAction;

    public delegate void CommunityStats(string stat, float changeAmount);
    public static event CommunityStats onSample;

    public delegate bool AbleToSample();
    public static event AbleToSample canSample;

    public delegate int HarmOrHelp(Card cardToCheck);
    public static event HarmOrHelp helpChance;

    public delegate void SetObjective();
    public static event SetObjective setObj;

    // Use this for initialization
    void Start ()
    {
        deckClass = GameObject.Find("GameMgr").GetComponent<Deck>();

        //sets positions cards can be on screen depending on screen size
        offsetIncre = ((float)Screen.width / 6.8f) * 1.2f;
        InitialisePositions();
		cardsToDraw = deckClass.deck;

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

    public GameObject GetCurrentCard() { return cardsToDraw[0].gameObject; }

    public void DeckToTable()
    {
        if (sampled)
        {
            sampled = false;
            for (int i = 0; i < 5; i++)
            {
                cardsOnTable = cardsToDraw;
                cardsToDraw.Remove(cardsToDraw[(cardsToDraw.Count - 1)]);
                drawnCards++;
            }
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
            if (helpChance(cardToHand) >= helpRandom)
            {
                StartCoroutine(EnableWaitDisable(acceptedAlert, time, new Color(0f, 213f, 255f), false));
                cardsInHand.Add(cardToHand.GetComponent<Card>());
                cardToHand.gameObject.transform.position = new Vector3(handPos.x + handOffset, handPos.y, 0f);
                cardToHand.gameObject.transform.SetParent(GameObject.Find("Hand").transform);
                cardToHand.gameObject.GetComponent<Button>().enabled = false;
                handOffset += offsetIncre;
                if (GameObject.Find("GameMgr").GetComponent<XMLWritinger>().isActiveAndEnabled)
                    XMLWritinger.WriteToXML(cardToHand.helpHarmStat.ToString(), "False", "Helped", System.DateTime.Now.ToString());
                onSample("Population", 1f);
                onSample("XP", 30f);
                setObj();
                cardsOnTable.Remove(cardToHand.GetComponent<Card>());
            }
            else
            {
                StartCoroutine(EnableWaitDisable(acceptedAlert, time, new Color(0f, 213f, 255f), false));
                cardsInHand.Add(cardToHand.GetComponent<Card>());
                cardToHand.gameObject.transform.position = new Vector3(handPos.x + handOffset, handPos.y, 0f);
                cardToHand.gameObject.transform.SetParent(GameObject.Find("Hand").transform);
                cardToHand.gameObject.GetComponent<Button>().enabled = false;
                handOffset += offsetIncre;
                if (GameObject.Find("GameMgr").GetComponent<XMLWritinger>().isActiveAndEnabled)
                    XMLWritinger.WriteToXML(cardToHand.helpHarmStat.ToString(), "False", "Harmed", System.DateTime.Now.ToString());
                onSample("Population", 1f);
                onSample("XP", 5);
                cardsOnTable.Remove(cardToHand.GetComponent<Card>());
            }
		} else
        {
            if (GameObject.Find("GameMgr").GetComponent<XMLWritinger>().isActiveAndEnabled)
                XMLWritinger.WriteToXML(cardToHand.helpHarmStat.ToString(), "True", "N/A", System.DateTime.Now.ToString());
            Handheld.Vibrate();
            StartCoroutine(EnableWaitDisable(rejectedAlert, time, new Color(255f, 0f, 0f), true));
            cardsInRejection.Add (cardToHand.GetComponent<Card> ());
			cardToHand.gameObject.transform.position = rejectionPos;
			cardToHand.gameObject.transform.SetParent (GameObject.Find ("Rejection").transform);
			cardToHand.gameObject.GetComponent<Button> ().enabled = false;
            cardsOnTable.Remove (cardToHand.GetComponent<Card> ());
        }
		sampled = true;
	}

    IEnumerator EnableWaitDisable(GameObject alert, float time, Color color, bool rejected)
    {
        if(rejected)
        {
			source.PlayOneShot (rejectedClip);
        }
        if (!rejected)
        {
			source.PlayOneShot (acceptedClip);
        }
        alert.SetActive(true);
        for (int i = 0; i < alert.transform.childCount; i++)
        {
            alert.transform.GetChild(i).GetComponent<Image>().color = color;
        }
        yield return new WaitForSeconds(time);
        alert.SetActive(false);
    }

	public void Discard (){
        Destroy(GameObject.Find("Draw").transform.GetChild(0).gameObject);
        deckClass.AddCardToDeck();
        cardsOnTable.Clear ();
    }

    public void PickCard()
    {
        if (canSample())
        {
            onSample("SR", -10);
            GameObject.Find("Draw").transform.GetChild(0).GetComponent<Card>().DrawToHand();
            deckClass.AddCardToDeck();
        }
    }

    // Update is called once per frame
    void Update () {
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

		//checks screen width each frame and 
		offsetIncre = ((float)Screen.width / 6.8f) * 1.2f;
	}
}
