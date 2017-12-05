using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public float fSatisfaction = 0.5f;
    public float fMoney = 25f;
    float fPopulation = 1f;
    float fFood = 10f;
    float fNumBuilding = 0f;

    Image satisfactionIm;
    Text moneyT;
    Text populationT;
    Text foodT;

    public float speed = 1f;

    bool sampling = true;

    GameObject samplingObject;
    GameObject samplingButton;

    bool building = false;

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
    float timeSec = 0;

    // Use this for initialization
    void Start () {
        //Screen.orientation = ScreenOrientation.LandscapeRight;
        IntialiseObjects();
    }

    private void OnEnable()
    {
        Selection.onSample += StatChange;
        Restoration.onPurchase += StatChange;
    }

    private void OnDisable()
    {
        Selection.onSample -= StatChange;
        Restoration.onPurchase -= StatChange;
    }

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
            case "Food":
                fFood += change;
                break;
            case "Population":
                fPopulation += change;
                break;
            case "Satisfaction":
                fSatisfaction += change;
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
        samplingButton.SetActive(false);

        //satisfactionIm = GameObject.Find("HappinessMeter").GetComponent<Image>();
        moneyT = GameObject.Find("Money").GetComponent<Text>();
        populationT = GameObject.Find("Population").GetComponent<Text>();
        //foodT = GameObject.Find("Food").GetComponent<Text>();

        gameTimeT = GameObject.Find("GameTimeT").GetComponent<Text>();
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
        BuildingButton.SetActive(false);
        samplingButton.SetActive(true);
        if (!building)
        {
            analysing = false;
            sampling = false;
            building = true;
        }
        else
        {
            building = false;
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
        }
        else
        {
            sampling = false;
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
        //satisfactionIm.fillAmount = fSatisfaction;
        float needle = fSatisfaction - 0.5f;
        //satisfactionIm.transform.SetPositionAndRotation(satisfactionIm.transform.position, new Quaternion(0,0, 90f*dial,0));
        //satisfactionIm.transform.localRotation = Quaternion.Euler(0, 0, -180f * needle);
        int iMoney = (int)fMoney;
        moneyT.text = iMoney.ToString();
        populationT.text = fPopulation.ToString();
        int iFood = (int)fFood;
        //foodT.text = iFood.ToString();

        string timeS;
        gameTimeSec += Time.deltaTime;
        if (gameTimeSec >= 60)
        {
            gameTimeSec -= 60;
            if(gameTimeSec < 60)
            {
                gameTimeMin++;
            }
        }
        float displayTime = 60f / timeSec;
        GameObject.Find("TimeFillBar").GetComponent<Image>().fillAmount = 1f - displayTime;
        //int gameTimeSecInt = (int)gameTimeSec;
        //timeS = gameTimeMin.ToString() + ":" + gameTimeSecInt.ToString();
        //if (gameTimeSec < 10)
        //{
        //    timeS = gameTimeMin.ToString() + ":0" + gameTimeSecInt.ToString();
        //}
        //gameTimeT.text = "Total Game Time:  " + timeS;
    }

    void CalculateSatisfaction()
    {
        //if (!starving)
        //{
        //    fSatisfaction += 0.01f * Time.deltaTime;
        //}
        //else
        //{
        //    fSatisfaction -= 0.01f * Time.deltaTime;
        //}

        //if(fSatisfaction > 1f)
        //{
        //    fSatisfaction = 1f;
        //}
        //if (fSatisfaction <= 0f)
        //{
        //    //Screen.orientation = ScreenOrientation.Portrait;
        //    Application.LoadLevel("Lose");
        //}
        //Debug.Log(fSatisfaction);

    }

    //IEnumerator TakeAwayFood()
    //{
    //    if (waited)
    //    {
    //        waited = false;
    //        yield return new WaitForSeconds(10f);
    //        if (fSatisfaction > 0f)
    //        {
    //            if (fSatisfaction > 0.5f)
    //            {
    //                if (fSatisfaction > 0.7f)
    //                {
    //                    fFood -= 3.5f * fPopulation;
    //                }
    //                else
    //                {
    //                    fFood -= 2.25f * fPopulation;
    //                }
    //            }
    //            else
    //            {
    //                fFood -= 1.5f * fPopulation;
    //            }
    //            if(fFood < 0)
    //            {
    //                starving = true;
    //            }
    //            else
    //            {
    //                starving = false;
    //            }
    //            if (fFood <= 0)
    //            {
    //                fFood = 0f;
    //            }
    //        }
    //        waited = true;
    //    }
    //}

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
        //fFood += (speed */* Random.Range(0.5f, 0.7f)*/ 0.3f) * Time.deltaTime * fNumBuilding;
    }

    // Update is called once per frame
    void Update ()
    {
        //StartCoroutine(TakeAwayFood());
        //fFood -= (speed * Random.Range(0.5f, 0.7f)) * Time.deltaTime * fPopulation;

        //fMoney += (speed * 0.75f) * Time.deltaTime * fPopulation;
        //fMoney -= (speed * 0.45f) * Time.deltaTime * fNumBuilding;
        CalculateSatisfaction();
        Timer();
        SetMeters();

        if (samplingObject != null)
            samplingObject.SetActive(sampling);
        else
            print("sampling panel gameobject is not active");

        if (buildingMenuObject != null)
            buildingMenuObject.SetActive(building);
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
