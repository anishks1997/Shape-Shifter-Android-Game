using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;


public class gameOver : MonoBehaviour {

	public GameObject AddObj;

	string subject = "Subject text";
	string body = "Actual text (Link)";

	#if UNITY_IPHONE

	[DllImport("__Internal")]
	private static extern void sampleMethod (string iosPath, string message);

	[DllImport("__Internal")]
	private static extern void sampleTextMethod (string message);

	#endif

	public void OnAndroidTextSharingClick()
	{

		StartCoroutine(ShareAndroidText());

	}
	IEnumerator ShareAndroidText()
	{
		yield return new WaitForEndOfFrame();
		//execute the below lines if being run on a Android device
		#if UNITY_ANDROID
		//Reference of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		//Reference of AndroidJavaObject class for intent
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		//call setAction method of the Intent object created
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		//set the type of sharing that is happening
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		//add data to be passed to the other activity i.e., the data to be sent
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		//intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
		//get the current activity
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//start the activity by sending the intent data
		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
		currentActivity.Call("startActivity", jChooser);
		#endif


	}


	public void OniOSTextSharingClick()
	{

		#if UNITY_IPHONE || UNITY_IPAD
		string shareMessage = "Wow I Just Share Text "; // change this 
		sampleTextMethod (shareMessage);

		#endif
	}

	// This is the part for Rating
	public void RateUs()
	{
		#if UNITY_ANDROID
		Application.OpenURL("market://details?id=1032952831467");

		/* I think this ID will be provided by PLAYSTORE, but for time-being attach this script
  to the button and check with some random URL */
		#elif UNITY_IPHONE
		Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_ID");
		#endif
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


				}



	public void ExitPress(){
		Application.Quit ();
	}



					
		
}
