using UnityEngine;
using System.Collections;

public class TheGUI : MonoBehaviour
{
    /*
	public enum THE_MODE
	{NONE=0,THE_HUD,THE_CREDSCREEN}
	public static TheCreditsScreen TheCredits;
	public static TheHUD TheHud;
	public static THE_MODE TheMode
	{
		get { return (THE_MODE)(TheCredits.TheActive > 0 ? TheCredits.TheActive : TheHud.TheActive > 0 ? TheHud.TheActive : 0); }
		set
		{
			TheCredits.TheActive = value;
			TheHud.TheActive = value;
		}
	}



	void Start () 
	{
		TheCredits = GameObject.Find("TheCredScreen").gameObject.GetComponent<TheCreditsScreen>();
		TheHud = GameObject.Find("TheHUD").gameObject.GetComponent<TheHUD>();
		TheMode = THE_MODE.NONE;
	}

	public static void TheUpdate()
	{
		TheHud.TheUpdate();
		TheCredits.TheUpdate();
	}
    */
}
