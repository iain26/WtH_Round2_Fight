using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int typeIndex = 0;

    public bool userControlled = false;

    GameObject gameData;
    DontDestroy data;

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
    }
	
	// Update is called once per frame
	void Update ()
    {
        data.characterHair = hairIndex;
        data.characterSkin = skinIndex;
        data.characterShirt = shirtIndex;

        if (userControlled)
        {
                    Select(hair, hairIndex, "Hair");
                    Select(skin, skinIndex, "Skin");
                    Select(shirt, shirtIndex, "Shirt");
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

        if (hair[hairIndex].name.Contains("BLUE"))
            GetComponent<Card>().hairColour = "Cyan";
        if (hair[hairIndex].name.Contains("GREEN"))
            GetComponent<Card>().hairColour = "Green";
        if (hair[hairIndex].name.Contains("PINK"))
            GetComponent<Card>().hairColour = "Pink";

        if (shirt[shirtIndex].name.Contains("BLUE"))
            GetComponent<Card>().shirtColour = "Blue";
        if (shirt[shirtIndex].name.Contains("GREEN"))
            GetComponent<Card>().shirtColour = "Orange";
        if (shirt[shirtIndex].name.Contains("RED"))
            GetComponent<Card>().shirtColour = "Yellow";
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
