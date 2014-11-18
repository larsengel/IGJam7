using UnityEngine;
using System.Collections;

public class TheHUD : blindGUITexturedContainer
{
	
	private const TheGUI.THE_MODE _theID = TheGUI.THE_MODE.THE_GAME;

	private TheBar[] energyBars = new TheBar[2];

	public TheCountdown TheDowncounter;

	public override void Start()
	{
		base.Start();
		energyBars[0] = transform.GetChild(0).gameObject.GetComponent<TheBar>();
		energyBars[1] = transform.GetChild(1).gameObject.GetComponent<TheBar>();
		TheDowncounter = GetComponentInChildren<TheCountdown>();
	}

	public bool Enable()
	{
		if(m_alpha < 1)
			m_alpha = 1;
		//TheGUI.TheMenu.GetComponent<AudioSource>().Stop();
		//GameObject.Find("ScriptContainer").GetComponent<AudioSource>().Play();
		return m_alpha == 1;
	}

	public bool Disable()
	{
		if(m_alpha > 0)
			m_alpha = 0;
		//GameObject.Find("ScriptContainer").GetComponent<AudioSource>().Stop();
		//TheGUI.TheMenu.GetComponent<AudioSource>().Play();
		return !( m_alpha == 0 );
	}

	protected internal void TheUpdate()
	{
	//	TheActive = TheGUI.TheMode;
		energyBars[0].Value = energyBars[0].player.timeCurrent;
		energyBars[0].UpdateLayout();
		energyBars[1].Value = energyBars[1].player.timeCurrent;
		energyBars[1].UpdateLayout();
		TheDowncounter.TheUpdate();
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
