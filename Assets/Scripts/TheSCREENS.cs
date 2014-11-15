using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheSCREENS : blindGUITexturedContainer
{
	
	 blindGUIAnimationState SHOWN,HIDDEN;
	 public blindGUIButton TheButton;
	 public blindGUIText   TheButtonText; 
	 public List<Texture2D> ScreenImages;
	 private int current = 0;

	 void Awake()
	 {

		 //SHOWN = new blindGUIAnimationState(m_size, m_scale, m_anchorPoint, m_offset, m_angle, 1);
		 //HIDDEN = new blindGUIAnimationState(m_size, m_scale, m_anchorPoint, m_offset, m_angle, 0);
		 TheButton = GameObject.Find("TheButton").GetComponent<blindGUIButton>();
		 TheButtonText = TheButton.transform.GetChild(0).gameObject.GetComponent<blindGUIText>();
	 }

	 public override void Start()
	 {
		 m_alpha = 0;
		 m_animationTime = 3;
		 TheButton.m_buttonClickDelegate = ClickTheButton;

	 }


	 public void SwitchToScreen(TheGUI.THE_MODE TheScreen)
	 {
		 if(TheScreen >= TheGUI.THE_MODE.THE_INSTRUCTIONS)
		 {
			 current = ( (int)TheScreen - (int)TheGUI.THE_MODE.THE_INSTRUCTIONS );
			 m_backgroundTexture = ScreenImages[current];
		 }
	 }


	 protected internal void TheUpdate()
	 {
		 if(Input.anyKeyDown)
			TheActive = TheGUI.THE_MODE.THE_MENU;
		 else
			TheActive = TheGUI.TheMode;

		 FlashingBackbutton();

	 }


	 private float BackButtonTexrAlpha = 0.5f;
	 private float advancer = 0.001f;
	 private bool invert = false;
	 private void FlashingBackbutton()
	 {
		 if(BackButtonTexrAlpha > 1 || BackButtonTexrAlpha < 0)
			 invert = !invert;
		 BackButtonTexrAlpha += invert ? -advancer : advancer;
		 TheButtonText.m_alpha = BackButtonTexrAlpha;
	 }

	 private void ClickTheButton(blindGUIButton theButton)
	 {
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
