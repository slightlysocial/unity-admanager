using UnityEngine;
using System.Collections;

public class TestScriptGUI : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		StartCoroutine(SSAdManager.LoadPList());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, 300, 100), "Show On Load"))
		{
			SSAdManager.ShowOnLoad();
		}
		
		if(GUI.Button(new Rect(0, 200, 300, 100), "Show On Pause"))
		{
			SSAdManager.ShowOnPause();
		}
		
		if(GUI.Button(new Rect(0, 400, 300, 100), "Show On Return"))
		{
			SSAdManager.ShowOnReturn();
		}
		
		if(GUI.Button(new Rect(0, 600, 300, 100), "Show On Gameover"))
		{
			SSAdManager.ShowOnGameOver();
		}
		
		if(GUI.Button(new Rect(0, 800, 300, 100), "Show Banner"))
		{
			SSAdManager.ShowBanner();
		}
		
		if(GUI.Button(new Rect(0, 1000, 300, 100), "Show More Games"))
		{
			SSAdManager.ShowMoreGames();
		}
		
		
		
		
		
	}
}
