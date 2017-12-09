using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipCard : MonoBehaviour {

    bool bFlip = false;
    bool frontface = true;

    Selection plyrMgr;

    public Button flipButton;

    public delegate object FlipCurrentCard();
    public static event FlipCurrentCard onFlip;

    // Use this for initialization
    void Start () {
        plyrMgr = GameObject.Find("PlayerMgr").GetComponent<Selection>();
	}

    public void Flip()
    {
        if (frontface)
        {
            frontface = false;
        }
        else
        {
            frontface = true;
        }
        bFlip = true;
    }

    void cardFlip() {
        if (bFlip)
        {
            float flipSide = 180f;
            if (frontface)
            {
                flipSide = 0;
            }
            else
            {
                flipSide = 180;
            }
            GameObject flipObject = plyrMgr.GetCurrentCard();
            flipObject.transform.Rotate(Vector3.up * Time.deltaTime * 150);

            if (flipObject.transform.localEulerAngles.y >= 0 && flipObject.transform.localEulerAngles.y <= 90)
            {
                flipObject.transform.GetChild(4).gameObject.SetActive(false);
            }
            if (flipObject.transform.localEulerAngles.y >= 90 && flipObject.transform.localEulerAngles.y <= 180)
            {
                flipObject.transform.GetChild(4).gameObject.SetActive(true);
            }
            if (flipObject.transform.localEulerAngles.y >= 180 && flipObject.transform.localEulerAngles.y <= 270)
            {
                flipObject.transform.GetChild(4).gameObject.SetActive(true);
            }
            if (flipObject.transform.localEulerAngles.y >= 270 && flipObject.transform.localEulerAngles.y < 360)
            {
                flipObject.transform.GetChild(4).gameObject.SetActive(false);
            }

            if (flipSide == 180)
            {
                if (flipObject.transform.localEulerAngles.y >= 180)
                    bFlip = false;
            }

            if (flipSide == 0)
            {
                if (!(flipObject.transform.localEulerAngles.y >= 180))
                    bFlip = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        flipButton.interactable = !bFlip;
        cardFlip();
	}
}
