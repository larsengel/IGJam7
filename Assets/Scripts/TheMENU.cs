using UnityEngine;
using System.Collections;

public class TheMENU : blindGUITexturedContainer
{
	private blindGUIButton[] buttons = new blindGUIButton[3];
	private const TheGUI.THE_MODE _theID = TheGUI.THE_MODE.THE_MENU;
	private bool buttonCanChanged=true;
	private enum Buttons{ start = 0, instructions = 1, exit = 2 }
	private Buttons ActiveButton;


	public override void Start()
	{
		base.Start();
		for(int i=0 ; i < this.transform.childCount ; i++)
		{
			buttons[i] = this.transform.GetChild(i).gameObject.GetComponent<blindGUIButton>();
			buttons[i].m_buttonClickDelegate = OnSellect;
		}

	}

	private void OnSellect(blindGUIButton sender)
	{
		ActiveButton = (Buttons)System.Enum.Parse(typeof(Buttons),sender.name);
		SellectMenuOption();
	}


	private void GetControllerInput()
	{
		if(TheActive == _theID)
		{
            if (buttonCanChanged)
			{
                float axis = Input.GetAxis("PlayerAControl") + Input.GetAxis("PlayerBControl");
                if (axis < -0.1f)
				{
					buttons[(int)ActiveButton].m_pushed = false;
					if(++ActiveButton > Buttons.exit)
						ActiveButton = Buttons.start;
					buttonCanChanged = false;
					buttons[(int)ActiveButton].m_pushed = true;
				}
                else if (axis > 0.1f)
				{
					buttons[(int)ActiveButton].m_pushed = false;
					if(--ActiveButton < Buttons.start)
						ActiveButton = Buttons.exit;
					buttonCanChanged = false;
					buttons[(int)ActiveButton].m_pushed = true;
				}
			}
			else
			{
				if(Input.GetAxis("PlayerAControl") == 0)
					buttonCanChanged = true;
			}

			if(Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Return))
				SellectMenuOption();
		}
	}

	private void SellectMenuOption()
	{
		switch(ActiveButton)
		{
		    case Buttons.start:
			    TheGUI.TheMode = TheGUI.THE_MODE.THE_GAME;
			    GameMaster.GameIsRunning = true;

                GameObject.Find("ScriptContainer/Menu").GetComponent<AudioSource>().Pause();
                GameObject.Find("ScriptContainer").GetComponent<AudioSource>().Play();

			    break;
		    case Buttons.instructions:
			    TheGUI.TheMode = TheGUI.THE_MODE.THE_INSTRUCTIONS;
			    break;
		    case Buttons.exit:
			    TheGUI.TheMode = TheGUI.THE_MODE.THE_EXIT;
			    break;
		}
        GameObject.Find("ScriptContainer/Click").GetComponent<AudioSource>().Play();

	}

	public void TheUpdate()
	{
		if(!GameMaster.GameIsRunning)
			GetControllerInput();
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
