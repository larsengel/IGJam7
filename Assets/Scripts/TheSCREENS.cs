using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheSCREENS : blindGUITexturedContainer
{

	blindGUIAnimationState SHOWN,HIDDEN;
	public blindGUIButton TheButton;
	public List<Texture2D> ScreenImages;
	private int current = 0;
	void Awake()
	{

		SHOWN = new blindGUIAnimationState(m_size, m_scale, m_anchorPoint, m_offset, m_angle, 1);
		HIDDEN = new blindGUIAnimationState(m_size, m_scale, m_anchorPoint, m_offset, m_angle, 0);
		TheButton = GameObject.Find("TheButton").GetComponent<blindGUIButton>();
		
	}
	void Start()
	{
		m_alpha = 0;
		m_animationTime = 3;
		TheButton.m_buttonClickDelegate = ClickTheButton;

	}


	public void SwitchToScreen(int number)
	{
		current = number;
		m_backgroundTexture = ScreenImages[current];
	}

	protected internal void TheUpdate()
	{
		TheActive = TheGUI.TheMode;
	
	}

	private void ClickTheButton(blindGUIButton theButton)
	{
		TheGUI.TheMode = TheGUI.THE_MODE.THE_HUD;
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







	private const int _theID = 2;
	


	//public bool _IEnabled = false;


	public TheGUI.THE_MODE TheActive
	{
		get
		{
			return m_enabled ? (TheGUI.THE_MODE)_theID :  TheGUI.THE_MODE.NONE;
		}
		set
		{
			if(value == (TheGUI.THE_MODE)_theID)
				m_enabled = Enable();
			else
				m_enabled = Disable();
		}
	}


}
