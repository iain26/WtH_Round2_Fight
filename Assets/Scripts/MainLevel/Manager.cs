using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public float fSatisfaction = 0.5f;
    public float fMoney = 25f;
    public float fSR = 25f;
    public float fXP = 0f;
    float xpLimit = 60f;
    float fPopulation = 1f;
    float fFood = 10f;
    float fNumBuilding = 0f;

    public List<string> objectives;

    Image satisfactionIm;
    Text moneyT;
    Text srT;
    Text xpT;
    Text populationT;
    Text foodT;

    public Text ObjectiveT;

    public float speed = 1f;

    bool sampling = false;

    GameObject samplingObject;
    GameObject samplingButton;

    bool building = true;

    GameObject buildingMenuObject;
    GameObject BuildingButton;

    bool analysing = false;

    GameObject censusMenuObject;

    bool quitting = false;

    GameObject QuittingComfirmationObject;

    bool waited = true;

    bool starving = false;

    Text gameTimeT;
    int gameTimeMin = 0;
    float gameTimeSec = 0;
    float gameTimeSecLimit = 60;
    float timeSec = 0;

    int randObjective;

    public GameObject levelUpNoti;

    // Use this for initialization
    void Start () {
        randObjective = Random.Range(0, objectives.Count);
        IntialiseObjects();
    }

    void SetObjectiveRandomly()
    {
        randObjective = Random.Range(0, objectives.Count);
    }

    private void OnEnable()
    {
        Selection.onSample += StatChange;
        Selection.setObj += SetObjectiveRandomly;
        Build.onPurchase += StatChange;
        Selection.canSample += GetSRCurrently;
    }

    private void OnDisable()
    {
        Selection.onSample -= StatChange;
        Selection.setObj -= SetObjectiveRandomly;
        Build.onPurchase -= StatChange;
        Selection.canSample -= GetSRCurrently;
    }

    public float GetMoneyCurrently() { return fMoney; }

    public bool GetSRCurrently() { return fSR >= 10; }

    public float GetPopulationCurrently() { return fPopulation; }

    void StatChange(string stat, float change)
    {
        switch (stat)
        {
            case "Building":
                fNumBuilding += change;
                break;
            case "Money":
                fMoney += change;
                break;
            case "SR":
                fSR += change;
                break;
            case "XP":
                fXP += change;
                break;
            case "Food":
                fFood += change;
                break;
            case "Population":
                fPopulation += change;
                break;
            case "Satisfaction":
                fSatisfaction += change;
                break;
            case "Time":
                timeSec = 0f;
                break;
        }
    }

    void IntialiseObjects()
    {
        samplingObject = GameObject.Find("SamplingPanel");
        buildingMenuObject = GameObject.Find("BuildingMenu");
        censusMenuObject = GameObject.Find("CensusMenu");
        QuittingComfirmationObject = GameObject.Find("QuitComfirm");

        samplingButton = GameObject.Find("SamplingButton");
        BuildingButton = GameObject.Find("BuildingMenuButton");

        samplingObject.SetActive(sampling);
        samplingButton.SetActive(!sampling);

        BuildingButton.SetActive(!building);
        
        moneyT = GameObject.Find("Money").GetComponent<Text>();
        srT = GameObject.Find("SR").GetComponent<Text>();
        //populationT = GameObject.Find("Population").GetComponent<Text>();
        xpT = GameObject.Find("XP").GetComponent<Text>();

        //gameTimeT = GameObject.Find("GameTimeT").GetComponent<Text>();
    }

    public void Analysing()
    {
        if (!analysing)
        {
            sampling = false;
            building = false;
            analysing = true;
        }
        else
        {
            analysing = false;
        }
    }

    public void Building()
    {
        if (!building)
        {
            analysing = false;
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
        samplingButton.SetActive(false);
        BuildingButton.SetActive(true);
        if (!sampling)
        {
            analysing = false;
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
        //float needle = fSatisfaction - 0.5f;
        int iMoney = (int)fMoney;
        moneyT.text = iMoney.ToString();

        int iSR = (int)fSR;
        srT.text = iSR.ToString();

        int iXP = (int)fXP;
        xpT.text = iXP.ToString() + " / " + xpLimit;

        //populationT.text = fPopulation.ToString();
        int iFood = (int)fFood;

        string timeS;
        gameTimeSec += Time.deltaTime;
        if (gameTimeSec >= gameTimeSecLimit)
        {
            gameTimeSec -= gameTimeSecLimit;
            if(gameTimeSec < gameTimeSecLimit)
            {
                gameTimeMin++;
            }
        }
        float displayTime = timeSec/ gameTimeSecLimit;
        GameObject.Find("TimeFillBar").GetComponent<Image>().fillAmount = 1f - displayTime;
        //int gameTimeSecInt = (int)gameTimeSec;
        //timeS = gameTimeMin.ToString() + ":" + gameTimeSecInt.ToString();
        //if (gameTimeSec < 10)
        //{
        //    timeS = gameTimeMin.ToString() + ":0" + gameTimeSecInt.ToString();
        //}
        //gameTimeT.text = "Total Game Time:  " + timeS;
    }

    IEnumerator OnWaitOff(GameObject gameThing)
    {
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
        string sTime;
        timeSec += Time.deltaTime;
        if (timeSec >= 60)
        {
            Application.LoadLevel("Lose");
        }
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        SetMeters();
        Timer();

        ObjectiveT.text = "Objective: Find " + objectives[randObjective];

        if(fXP >= xpLimit)
        {
            fXP -= xpLimit;
            xpLimit *= 2f;
            StartCoroutine(OnWaitOff(levelUpNoti));
            StatChange("Time", 0f);
        }

        if (samplingObject != null)
            samplingObject.SetActive(sampling);
        else
            print("sampling panel gameobject is not active");

        if (buildingMenuObject != null) { }
        //buildingMenuObject.SetActive(building);
        else
            print("building menu gameobject is not active");

        if (censusMenuObject != null)
            censusMenuObject.SetActive(analysing);
        else
            print("census menu gameobject is not active");

        if (QuittingComfirmationObject != null)
            QuittingComfirmationObject.SetActive(quitting);
        else
            print("Quit Comfirmation gameobject is not active");
    }
}
