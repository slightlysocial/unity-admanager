using UnityEngine;
using System.Collections;

public class VungleWarpper

{
	AndroidJavaClass jo; 
	
	public enum Gender
		{
		Male,
		Female
		}
	
	
	
	internal  AndroidJavaObject CurrentActivity() 
		{
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	}
	

	public  void init(string appkey)
		{
			jo= new AndroidJavaClass("com.plugin.vungleunityplugin");
			jo.CallStatic("Init",CurrentActivity(),appkey);
		}
		
	public void init(string appkey,int age,Gender gender)
		{
			jo= new AndroidJavaClass("com.plugin.vungleunityplugin");
			jo.CallStatic("Init",CurrentActivity(),appkey,age,(int)gender);
		}
		
	public  bool isVideoAvailable()
		{
			jo.CallStatic("isVideoAvalible");
			return jo.GetStatic<bool>("aval");
		}
		
	public void displayAdvert()
		{
			 jo.CallStatic("displayAdvert");
		}
		
	public void displayIncentivizedAdvert(bool showClose)
		{
				jo.SetStatic<bool>("showClose",showClose);
				jo.CallStatic("displayAdvert_Inc");
		}
		
	public void displayIncentivizedAdvert(bool showClose,string name)
		{
				jo.SetStatic<bool>("showClose",showClose);
				jo.SetStatic<string>("name",name);
				jo.CallStatic("displayAdvert_InNamec");
		}
		
	public bool getState()
		{
		return jo.GetStatic<bool>("showClose");	
		}
		
	public bool started()
		{
		return jo.GetStatic<bool>("hasStarted");	
		}
		
	}

