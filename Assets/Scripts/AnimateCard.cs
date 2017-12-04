using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateCard : MonoBehaviour {

    Image cardBackground;
    SpriteRenderer animationImage;

    // Use this for initialization
    void Start () {
        cardBackground = GetComponent<Image>();
        animationImage = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        cardBackground.sprite = animationImage.sprite;
	}
}
