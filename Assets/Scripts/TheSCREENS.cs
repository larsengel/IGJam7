using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheSCREENS : blindGUITexturedContainer
{
	 public blindGUIButton TheButton;
	 public blindGUIText   TheButtonText; 
	 public List<Texture2D> ScreenImages;
	 private int current = 0;

	 void Awake()
	 {
		 TheButton = GameObject.Find("TheButton").GetComponent<blindGUIButton>();
		 TheButtonText = TheButton.transform.GetChild(0).gameObject.GetComponent<blindGUIText>();
	 }

	 public override void Start()
	 {
		 base.Start();
		 m_alpha = 0;
		 TheButton.m_buttonClickDelegate = ClickTheButton;
	 }


	 public void SwitchToScreen(TheGUI.THE_MODE TheScreen)
	 {
		 if(TheScreen >= TheGUI.THE_MODE.THE_INSTRUCTIONS)
		 {
			 

				// this.TheButton.m_pressImage = this.TheButton.m_hoverImage = this.TheButton.m_idleImage = ScreenImages[3];
		//	 else if(current >= (int)TheGUI.THE_MODE.THE_PLAYER1_WINSCREEN)
			 
			 current = ( (int)TheScreen - (int)TheGUI.THE_MODE.THE_INSTRUCTIONS );

			 if(TheScreen == TheGUI.THE_MODE.THE_PLAYER1_WINSCREEN || TheScreen == TheGUI.THE_MODE.THE_PLAYER2_WINSCREEN)
			 {
				 this.TheButton.m_pressImage = this.TheButton.m_hoverImage = this.TheButton.m_idleImage = ScreenImages[4];
				 GameObject.Find("ScriptContainer/Countdown").GetComponent<AudioSource>().Stop();
			 }
			 else
				 this.TheButton.m_pressImage = this.TheButton.m_hoverImage = this.TheButton.m_idleImage = ScreenImages[3];

			 m_backgroundTexture = ScreenImages[current];
			// this.TheButton.m_idleImage = ScreenImages[current + 3];
			
		 }
	 }


	 protected internal void TheUpdate()
	 {
		 if(!GameMaster.GameIsRunning)
		 {
			 if(Input.anyKeyDown)
				 TheActive = TheGUI.THE_MODE.THE_MENU;
		//	 else
				// TheActive = TheGUI.TheMode;
		 }
	 }





	 private void ClickTheButton(blindGUIButton theButton)
	 {
		 if(GameMaster.GameIsRunning)
		 {
			 GameMaster.GameIsRunning = false;
			 Application.LoadLevel("level1");
		 }
		 TheGUI.TheMode = TheGUI.THE_MODE.THE_MENU;
	 }


	 public bool Enable()
	 {
		 if(m_alpha<1)
			 m_alpha =1;

		 return m_alpha == 1;
	 }

	 public bool Disable()
	 {
		 if(m_alpha > 0)
			 m_alpha = 0;

		 return !( m_alpha == 0 );
	 }






	 private const int _theID = (int)TheGUI.THE_MODE.THE_INSTRUCTIONS;
	


	//public bool _IEnabled = false;


	public TheGUI.THE_MODE TheActive
	{
		get
		{
			return m_enabled ? (TheGUI.THE_MODE)(_theID + current) :  TheGUI.THE_MODE.THE_NONE;
		}
		set
		{
			if(value >= (TheGUI.THE_MODE)_theID)
			{
				SwitchToScreen(value);
				m_enabled = Enable();
			}
			else
				m_enabled = Disable();
		}
	}
	 


}
