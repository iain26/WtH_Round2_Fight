using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour 
{

	AudioSource audioSource;
	public bool AudioBegin = false;

	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }



	void Update () 
	{
		if (!AudioBegin) 
		{
			audioSource.Play ();
			AudioBegin = true;
		} 
	}
}


