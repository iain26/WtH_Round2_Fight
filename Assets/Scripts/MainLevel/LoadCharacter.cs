using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour {

    GameObject gameData;
    DontDestroy data;
    CharacterCreation characterTraits;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("GameData") != null)
        {
            gameData = GameObject.Find("GameData");
            data = gameData.GetComponent<DontDestroy>();
        }
        else
            print("There is no game data");

        characterTraits = gameObject.GetComponent<CharacterCreation>();
        characterTraits.hairIndex = data.characterHair;
        characterTraits.skinIndex = data.characterSkin;
        characterTraits.shirtIndex = data.characterShirt;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
