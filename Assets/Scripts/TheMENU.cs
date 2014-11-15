using UnityEngine;
using System.Collections;

public class TheMENU : blindGUITexturedContainer
{
	private blindGUIButton[] buttons = new blindGUIButton[3];
	private const TheGUI.THE_MODE _theID = TheGUI.THE_MODE.THE_MENU;


	public override void Start()
	{
		for(int i=0 ; i < this.transform.childCount ; i++)
		{
			buttons[i] = this.transform.GetChild(i).gameObject.GetComponent<blindGUIButton>();
			buttons[i].m_buttonClickDelegate = OnSellect;
		}

	}

	private void OnSellect(blindGUIButton sender)
	{
		switch(sender.name)
		{
		case "start":
			TheGUI.TheMode = TheGUI.THE_MODE.THE_GAME;
			break;
		case "instructions":
			TheGUI.TheMode = TheGUI.THE_MODE.THE_INSTRUCTIONS;
			break;
		case "exit":
			TheGUI.TheMode = TheGUI.THE_MODE.THE_EXIT;
			break;
		}
	}

	public void TheUpdate()
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

	public TheGUI.THE_MODE TheActive
	{
		get
		{
			return m_enabled ? (TheGUI.THE_MODE)_theID : TheGUI.THE_MODE.THE_NONE;
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
