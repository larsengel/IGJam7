using UnityEngine;
using System.Collections;



public class TheBar : blindGUITexturedContainer
{


	private const int _theID = 1;
	public static TheBar TheLiveBar;
	public PlayerScript player;

	void Awake()
	{

		

		TheLiveBar = this;
	}

	void Start()
	{
		
	}

	public void SetGlueAmount(int playerNumber,int value)
	{
		this.transform.GetChild(0).gameObject.GetComponent<blindGUITexturedContainer>().m_size.x = ( value / 100 ) * 365;
	}







}
