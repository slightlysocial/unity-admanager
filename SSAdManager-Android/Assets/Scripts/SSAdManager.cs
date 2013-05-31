using UnityEngine;
using System.Collections;

public class SSAdManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public static void showChartBoostFullScreenAd()
	{
		if(SSAdInitializer.ChartBoostActiveStaticFlag)
			ChartBoostAndroid.showInterstitial(null);	
	}
	
	public static void showChartBoostMoreGamesAd()
	{
		if(SSAdInitializer.ChartBoostActiveStaticFlag)
			ChartBoostAndroid.showMoreApps();
	}
}
