﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour {

    int buildCount = 0;
    float offset = 0;

    public delegate void BuildExpense(string stat, float changeAmount);
    public static event BuildExpense onPurchase;

    public GameObject floorTemp;
    public GameObject moneyCollectable;
    public GameObject srCollectable;

    Manager mgr;
    Text moneyDisplay;
    GameObject buildingGrid;
    RawImage background;

    float moneyAmount = 100;

    bool waitForDrop = false;

    float speed = 20f;

    // Use this for initialization
    void Start () {
        mgr = GameObject.Find("GameMgr").GetComponent<Manager>();
        moneyDisplay = GameObject.Find("MoneyAmountDisplay").GetComponent<Text>();
        buildingGrid = GameObject.Find("BuildingGrid");
        background = GameObject.Find("Canvas").GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        ResourceCollect.onCollected += AddToResource;
    }

    private void OnDisable()
    {
        ResourceCollect.onCollected -= AddToResource;
    }

    void AddToResource(string resource)
    {
        int fRandom;
        switch (resource)
        {
            case "Money":
                fRandom = Random.Range(25 * buildCount, 40 * buildCount);
                onPurchase("Money", fRandom);
                break;
            case "SR":
                fRandom = Random.Range(5 * buildCount, 8 * buildCount);
                onPurchase("SR", fRandom);
                break;
        }
    }

    public void BuildFloor()
    {
        if (mgr.GetMoneyCurrently() >= moneyAmount)
        {
            onPurchase("Money", -moneyAmount);
            //onPurchase("Time", 0f);
            moneyAmount *= 2f;
            GameObject newFloor = GameObject.Instantiate(floorTemp);
            newFloor.name = "Floor " + buildCount;
            buildCount++;
            newFloor.transform.SetParent(buildingGrid.transform);
            newFloor.transform.localScale = new Vector3(1f, 1f, 1f);
            newFloor.transform.localPosition = new Vector3(0f, 0 + offset, 0f);
            offset += 160f;
        }
    }

    IEnumerator DropResource()
    {
        waitForDrop = true;
        yield return new WaitForSeconds(Random.Range(5,10));
        if(Random.Range(0, 2) > 0)
        {
            GameObject moneyDrop = GameObject.Instantiate(moneyCollectable);
            moneyDrop.name = "MoneyDrop";
            moneyDrop.transform.SetParent(GameObject.Find("DropParent").transform);
            moneyDrop.transform.localScale = new Vector3(1f, 1f, 1f);
            moneyDrop.transform.localPosition = new Vector3(0f, -69f, 0f);
        }
        else
        {
            GameObject srDrop = GameObject.Instantiate(srCollectable);
            srDrop.name = "SRDrop";
            srDrop.transform.SetParent(GameObject.Find("DropParent").transform);
            srDrop.transform.localScale = new Vector3(1f, 1f, 1f);
            srDrop.transform.localPosition = new Vector3(120f, -69f, 0f);
        }
        waitForDrop = false;
        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (buildCount > 0)
        {
            if (!waitForDrop)
                StartCoroutine(DropResource());
        }
        if (buildCount > 5)
        {
            if(buildingGrid.transform.localPosition.y > -220f - (160f * (buildCount - 5)))
            {
                float timeLastFrame = Time.deltaTime;
                background.uvRect = new Rect(0.07f, background.uvRect.y + Time.deltaTime * 0.0089f, 0.9f, 0.6f);
                buildingGrid.transform.localPosition = new Vector3(0f, buildingGrid.transform.localPosition.y - (Time.deltaTime * speed), 0f);
            }
                //buildingGrid.transform.localPosition = new Vector3(0f, -220f - (160f * (buildCount - 5)), 0f);
        }
        moneyDisplay.text = moneyAmount.ToString();
	}
}
