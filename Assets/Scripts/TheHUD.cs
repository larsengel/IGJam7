using UnityEngine;
using System.Collections;

public class TheHUD : blindGUITexturedContainer
{
	
	private const TheGUI.THE_MODE _theID = TheGUI.THE_MODE.THE_GAME;



	public override void Start()
	{

	}

	public bool Enable()
	{
		if(m_alpha < 1)
			m_alpha = 1;

		return m_alpha == 1;
	}

	public bool Disable()
	{
		if(m_alpha > 0)
			m_alpha = 0;

		return !( m_alpha == 0 );
	}

	protected internal void TheUpdate()
	{
		TheActive = TheGUI.TheMode;
	}

	public TheGUI.THE_MODE TheActive
	{
		get
		{
			return m_enabled ? _theID : TheGUI.THE_MODE.THE_NONE;
		}
		set
		{
			if(value == _theID)
				m_enabled = Enable();
			else
				m_enabled = Disable();

		}
	}
	


}
