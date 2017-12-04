using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Script : MonoBehaviour {

	public GameObject tutorial;
	public GameObject tutorial2;
	public GameObject tutorial3;
	public GameObject tutorial4;

	public float points = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{


		
	}

	public void Destroy()
	{
		Destroy (gameObject);
	}

	public void ActivateMessage ()
	{
		tutorial2.SetActive (true);
	}

	public void Points()
	{
		points = points + 1;
		if (points == 2)
		{
			tutorial3.SetActive (true);
		}
			
	}
	public void Points2()
	{
		points = points + 1;
		if (points == 2) {
			tutorial4.SetActive (true);
		}
	}
}
