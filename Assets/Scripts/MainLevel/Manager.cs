using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    
    //stats to increment and decrease to affect gameplay
    public float fMoney = 25f;
    public float fSR = 25f;
    public float fXP = 0f;
    float xpLimit = 60f;
    float fPopulation = 1f;
    //the text in game to display stats above
    Text moneyT;
    Text srT;
    Text xpT;
    //sampling menu check and menu itself as well as button to select button
    bool sampling = false;
    GameObject samplingObject;
    GameObject samplingButton;
    //building menu check and menu itself as well as button to select button
    bool building = true;
    GameObject buildingMenuObject;
    GameObject BuildingButton;
    //building menu check and menu itself
    bool quitting = false;
    GameObject QuittingComfirmationObject;
    //check to see if coroutine has run only once 
    bool waited = true;
    //time limit for round and the current time 
    float gameTimeSecLimit = 60;
    float timeSec = 0;
    //leveled up notification if exceeded xp limit 
    public GameObject levelUpNoti;
    //current card displayed to player
    Card cardToCheck;
    //used to store what sring value to reference when creating the matrix table
    delegate string MatrixValues();
    MatrixValues firstElement;
    MatrixValues secondElement;

    // Use this for initialization
    void Start() {
        IntialiseObjects();
        RandomiseMatrix();
    }

    void RandomiseMatrix()
    {
        //yup you guessed it randomises the matrix header elements and makes sure the two elements cant be the same
        int rand1 = Random.Range(0, 3);
        int rand2;
        do
        {
            rand2 = Random.Range(0, 3);
        } while (rand1 == rand2);
        switch (rand1)
        {
            case 0:
                print("hair");
                firstElement = new MatrixValues(CheckHair);
                break;
            case 1:
                print("shirt");
                firstElement = new MatrixValues(CheckShirt);
                break;
            case 2:
                print("skin");
                firstElement = new MatrixValues(CheckSkin);
                break;
        }
        switch (rand2)
        {
            case 0:
                print("hair");
                secondElement = new MatrixValues(CheckHair);
                break;
            case 1:
                print("shirt");
                secondElement = new MatrixValues(CheckShirt);
                break;
            case 2:
                print("skin");
                secondElement = new MatrixValues(CheckSkin);
                break;
        }
    }

    //listeners for events from sources in other scripts and if necessary returns value to origin script
    private void OnEnable()
    {
        Selection.onSample += StatChange;
        Build.onPurchase += StatChange;
        Selection.canSample += GetSRCurrently;
        Selection.helpChance += CheckHelpChance;
    }

    private void OnDisable()
    {
        Selection.onSample -= StatChange;
        Build.onPurchase -= StatChange;
        Selection.canSample -= GetSRCurrently;
        Selection.helpChance -= CheckHelpChance;
    }

    //methods for returning strings from the card object displayed to player
    string CheckHair()
    {
        return cardToCheck.hairColour;
    }

    string CheckShirt()
    {
        return cardToCheck.shirtColour;
    }

    string CheckSkin()
    {
        return cardToCheck.skinColour;
    }


    int CheckHelpChance(Card currentCard)
    {
        //assigns card to that one that is to be checked i.e one that user sees
        cardToCheck = currentCard;
        //checks where that card places on the matrix and returns the value of help chance (higher the better)
        switch (firstElement())
        {
            case "1":
                switch (secondElement())
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
                switch (secondElement())
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
                switch (secondElement())
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

    //returns stats accessible by other scripts
    public float GetMoneyCurrently() { return fMoney; }

    public bool GetSRCurrently() { return fSR >= 10; }

    public float GetPopulationCurrently() { return fPopulation; }

    void StatChange(string stat, float change)
    {
        //changes the stats dependent on what string of stat was passed in and how much to change by (can be a negative value)
        switch (stat)
        {
            case "Money":
                fMoney += change;
                break;
            case "SR":
                fSR += change;
                break;
            case "XP":
                fXP += change;
                break;
            case "Population":
                fPopulation += change;
                break;
            case "Time":
                timeSec = 0f;
                break;
        }
    }

    void IntialiseObjects()
    {
        //finds the gameobject in unity editor and assigns them here
        samplingObject = GameObject.Find("SamplingPanel");
        buildingMenuObject = GameObject.Find("BuildingMenu");
        QuittingComfirmationObject = GameObject.Find("QuitComfirm");

        samplingButton = GameObject.Find("SamplingButton");
        BuildingButton = GameObject.Find("BuildingMenuButton");

        samplingObject.SetActive(sampling);
        samplingButton.SetActive(!sampling);

        BuildingButton.SetActive(!building);
        
        moneyT = GameObject.Find("Money").GetComponent<Text>();
        srT = GameObject.Find("SR").GetComponent<Text>();
        xpT = GameObject.Find("XP").GetComponent<Text>();
    }

    public void Building()
    {
        //if building menu off then turn sampling menu off and turn on sampling button, opposite for if building on
        if (!building)
        {
            sampling = false;
            building = true;

            BuildingButton.SetActive(false);
            samplingButton.SetActive(true);
        }
        else
        {
            building = false;

            BuildingButton.SetActive(true);
            samplingButton.SetActive(false);
        }
    }

    public void Sampling()
    { 
        //if samliong menu off then turn building menu off and turn on building button, opposite for if sampling on
        samplingButton.SetActive(false);
        BuildingButton.SetActive(true);
        if (!sampling)
        {
            building = false;
            sampling = true;


            samplingButton.SetActive(false);
            BuildingButton.SetActive(true);
        }
        else
        {
            sampling = false;
            building = true;

            samplingButton.SetActive(true);
            BuildingButton.SetActive(false);
        }
    }

    public void Quit()
    {
        //used to turn on quit comfirmation menu
        if (!quitting)
        {
            quitting = true;
        }
        else
        {
            quitting = false;
        }
    }

    void SetMeters()
    {
        //converts floats of stats to ints so they can be changed to strings and displayed without decimal points
        int iMoney = (int)fMoney;
        moneyT.text = iMoney.ToString();

        int iSR = (int)fSR;
        srT.text = iSR.ToString();

        int iXP = (int)fXP;
        xpT.text = iXP.ToString() + " / " + xpLimit;

        //sets fill amount of bar so can see the time left
        float displayTime = timeSec/ gameTimeSecLimit;
        GameObject.Find("TimeFillBar").GetComponent<Image>().fillAmount = 1f - displayTime;
    }

    IEnumerator OnWaitOff(GameObject gameThing)
    {
        //sets a gameobject in editor to be displayed for exactly one second
        if (waited)
        {
            waited = false;
            gameThing.SetActive(true);
            yield return new WaitForSeconds(1f);
            gameThing.SetActive(false);
            waited = true;
        }
    }

    void Timer()
    {
        //if exceeded the time limit then transistions to lose scene
        timeSec += Time.deltaTime;
        if (timeSec >= gameTimeSecLimit)
        {
            Application.LoadLevel("Lose");
        }
    }

    private void FixedUpdate()
    {
        
    }

    void CheckLevelUp()
    {
        //if level up then decrease xp, double xp limit and setr time back to original
        if (fXP >= xpLimit)
        {
            fXP -= xpLimit;
            xpLimit *= 2f;
            StartCoroutine(OnWaitOff(levelUpNoti));
            StatChange("Time", 0f);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        SetMeters();
        Timer();
        CheckLevelUp();
        //if there is an object then sets its activity 
        if (samplingObject != null)
            samplingObject.SetActive(sampling);
        else
            print("sampling panel gameobject is not active");

        if (QuittingComfirmationObject != null)
            QuittingComfirmationObject.SetActive(quitting);
        else
            print("Quit Comfirmation gameobject is not active");
    }
}
