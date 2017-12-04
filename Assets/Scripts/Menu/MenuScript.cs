using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public AudioSource source;
	//public AudioClip hover;
	public AudioClip click;
	public AudioClip secondClick;

	// Use this for initialization
	void Start () {
		
	}

	//Load scenes
	public void LoadMain_Menu()
	{
		StartCoroutine (LoadingMain_Menu());
	}

	IEnumerator LoadingMain_Menu()
	{
		yield return new WaitForSeconds (1f);
		Application.LoadLevel ("Main_Menu");
	}

	public void LoadTutorial_Check()
	{
		StartCoroutine (LoadingTutorial_Check());
	}

	IEnumerator LoadingTutorial_Check()
	{
		yield return new WaitForSeconds (1f);
		Application.LoadLevel ("Tutorial_Check");
	}

	public void LoadTutorial()
	{
		StartCoroutine (LoadingTutorial());
	}

	IEnumerator LoadingTutorial()
	{
		yield return new WaitForSeconds (1f);
		Application.LoadLevel ("Tutorial");
	}

	public void LoadCharacterCreation() 
	{
		StartCoroutine (LoadingCharacterCreation());
	}

	IEnumerator LoadingCharacterCreation()
	{
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene("CharacterCreation");
	}

	public void LoadDraw_Phase()
	{
		StartCoroutine (LoadingDraw_Phase());
	}

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadingDraw_Phase()
	{
		yield return new WaitForSeconds (1f);
		Application.LoadLevel ("Draw_Phase");
	}



	public void LoadSettings()
	{
		StartCoroutine (LoadingSettings());
	}

	IEnumerator LoadingSettings()
	{
		yield return new WaitForSeconds (0.3f);
		Application.LoadLevel ("Settings");
	}

	//public void OnHover()
	//{
	//	source.PlayOneShot (hover);
	//}

	public void OnClick()
	{
		source.PlayOneShot (click);
	}

	public void OnArrow()
	{
		source.PlayOneShot (secondClick);
	}

	// Update is called once per frame
	void Update () {

	}
}
