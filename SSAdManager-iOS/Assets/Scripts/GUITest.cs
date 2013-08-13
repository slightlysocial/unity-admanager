using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, 200, 50), "ON LOAD AD"))
		{
			SSAdManager.ShowOnLoad();
		}
		
		if(GUI.Button(new Rect(0, 100, 200, 50), "PAUSE AD"))
		{
			SSAdManager.ShowOnPause();
		}
		
		if(GUI.Button(new Rect(0, 200, 200, 50), "GAMEOVER AD"))
		{
			SSAdManager.ShowOnGameOver();
		}
		
		if(GUI.Button(new Rect(0, 300, 200, 50), "BANNER AD"))
		{
			SSAdManager.ShowBanner();
		}
		
		if(GUI.Button(new Rect(0, 400, 200, 50), "MORE GAMES"))
		{
			SSAdManager.ShowMoreGames();
		}
		
		if(GUI.Button(new Rect(0, 500, 200, 50), "RETURN AD"))
		{
			SSAdManager.ShowOnReturn();
		}
		
	}
}
