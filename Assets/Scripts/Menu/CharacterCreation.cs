using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour {

    public List<GameObject> hair = new List<GameObject>();
    GameObject hairObject;
    public int hairIndex;

    public List<GameObject> skin = new List<GameObject>();
    GameObject skinObject;
    public int skinIndex;

    public List<GameObject> shirt = new List<GameObject>();
    GameObject shirtObject;
    public int shirtIndex;

    //public List<string>

    int typeIndex = 0;

    public bool userControlled = false;

    GameObject gameData;
    DontDestroy data;

    public Sprite grapefuit;
    public Sprite watermelon;
    public Sprite banana;

    //public bool showBackOfCard = true;

    // Use this for initialization
    void Start () {
        if (GameObject.Find("GameData") != null) {
            gameData = GameObject.Find("GameData");
            data = gameData.GetComponent<DontDestroy>();
                }
        else
            print("There is no game data");

        skinObject = gameObject.transform.GetChild(1).gameObject;
        for (int i = 0; i < skinObject.transform.childCount; i++)
        {
            skin.Add(skinObject.transform.GetChild(i).gameObject);
        }
        skinIndex = Random.Range(0, skin.Count);
        skin[skinIndex].SetActive(true);

        hairObject = gameObject.transform.GetChild(2).gameObject;
        for (int i = 0; i < hairObject.transform.childCount; i++)
        {
            hair.Add(hairObject.transform.GetChild(i).gameObject);
        }
        hairIndex = Random.Range(0, hair.Count);
        hair[hairIndex].SetActive(true);

        shirtObject = gameObject.transform.GetChild(3).gameObject;
        for (int i = 0; i < shirtObject.transform.childCount; i++)
        {
            shirt.Add(shirtObject.transform.GetChild(i).gameObject);
        }
        shirtIndex = Random.Range(0, shirt.Count);
        shirt[shirtIndex].SetActive(true);

        int randFaction = Random.Range(0, 3);
        int randEducation = Random.Range(0, 3);
        int randOccupation = Random.Range(0, 3);

        switch (randFaction)
        {
            case 0:
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = grapefuit;
                transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Grapefruit Guardians";
                break;
            case 1:
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = watermelon;
                transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Watermelon Warriors";
                break;
            case 2:
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = banana;
                transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Banana Bandits";
                break;
        }

        switch (randEducation)
        {
            case 0:
                transform.GetChild(4).GetChild(2).GetComponent<Text>().text = "High School Graduate";
                break;
            case 1:
                transform.GetChild(4).GetChild(2).GetComponent<Text>().text = "College Degree";
                break;
            case 2:
                transform.GetChild(4).GetChild(2).GetComponent<Text>().text = "PhD";
                break;
        }
        switch (randOccupation)
        {
            case 0:
                transform.GetChild(4).GetChild(3).GetComponent<Text>().text = "Journalist";
                break;
            case 1:
                transform.GetChild(4).GetChild(3).GetComponent<Text>().text = "Emergency Services";
                break;
            case 2:
                transform.GetChild(4).GetChild(3).GetComponent<Text>().text = "Retired";
                break;
        }
        transform.GetChild(4).gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (userControlled)
        {
            data.characterHair = hairIndex;
            data.characterSkin = skinIndex;
            data.characterShirt = shirtIndex;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                typeIndex--;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                typeIndex++;
            }
            if (typeIndex > 2)
                typeIndex = 0;
            if (typeIndex < 0)
                typeIndex = 2;
        }


        Select(hair, hairIndex, "Hair");
        Select(skin, skinIndex, "Skin");
        Select(shirt, shirtIndex, "Shirt");

        if (hair[hairIndex].name.Contains("BLUE"))
            GetComponent<Card>().hairColour = "1";
        if (hair[hairIndex].name.Contains("GREEN"))
            GetComponent<Card>().hairColour = "2";
        if (hair[hairIndex].name.Contains("PINK"))
            GetComponent<Card>().hairColour = "3";

        if (shirt[shirtIndex].name.Contains("BLUE"))
            GetComponent<Card>().shirtColour = "1";
        if (shirt[shirtIndex].name.Contains("GREEN"))
            GetComponent<Card>().shirtColour = "2";
        if (shirt[shirtIndex].name.Contains("RED"))
            GetComponent<Card>().shirtColour = "3";

        if (skin[skinIndex].name.Contains("BLACK"))
            GetComponent<Card>().skinColour = "1";
        if (skin[skinIndex].name.Contains("TAN"))
            GetComponent<Card>().skinColour = "2";
        if (skin[skinIndex].name.Contains("WHITE"))
            GetComponent<Card>().skinColour = "3";


    }

    public void HairRight()
    {
        hairIndex++;
    }

    public void HairLeft()
    {
        hairIndex--;
    }

    public void SkinRight()
    {
        skinIndex++;
    }

    public void SkinLeft()
    {
        skinIndex--;
    }

    public void ShirtRight()
    {
        shirtIndex++;
    }

    public void ShirtLeft()
    {
        shirtIndex--;
    }

    void Select(List<GameObject> visual, int index , string type)
    {  

        for (int i = 0; i < visual.Count; i++)
        {
            visual[i].SetActive(false);
        }

        if (index >= visual.Count)
            index = 0;
        if (index < 0)
            index = visual.Count - 1;
        visual[index].SetActive(true);

        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;
            case "Shirt":
                shirtIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
        }
    }
}
