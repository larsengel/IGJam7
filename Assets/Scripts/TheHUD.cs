using UnityEngine;
using System.Collections;



public class TheHUD : blindGUITexturedContainer
{
	private blindGUIAnimationState astate;
	private blindGUIText[] TextDisplay = new blindGUIText[6];
	public static TheHUD Turtlecount;
	private const int _theID = 1;

	void Awake()
	{
		for(int i=0 ; i < transform.childCount ; i++)
			TextDisplay[i] = transform.GetChild(i).GetComponent<blindGUIText>();
		
		TextDisplay[0].m_text = "Turtles Left:";
		TextDisplay[1].m_text = "0";
		TextDisplay[2].m_text = "Turtles Collected:";
		TextDisplay[3].m_text = "0";
		TextDisplay[4].m_text = "Turtles Died:";
		TextDisplay[5].m_text = "0";

		Turtlecount = this;
	}

	void Start()
	{
		astate = new blindGUIAnimationState(this);
	}

	public void SetLeftTurtlesNumber(int number)
	{
		TextDisplay[1].m_text = number.ToString();
	}

	public void SetCollectedTurtelsNumber(int number)
	{
		TextDisplay[3].m_text = number.ToString();
	}
	
	public void SetDiedTurtelsNumber(int number)
	{
		TextDisplay[5].m_text = number.ToString();
	}

	public bool Enable()
	{
		if(m_alpha < 1)
			m_alpha =1;

		return m_alpha == 1;
	}

	public bool Disable()
	{
		if(m_alpha > 0)
			m_alpha =0;

		return !( m_alpha == 0 );
	}

	protected internal void TheUpdate()
	{
		TheActive = TheGUI.TheMode;
	}




	//public bool _IEnabled=true;

	public TheGUI.THE_MODE TheActive
	{
		get
		{
			return m_enabled ? (TheGUI.THE_MODE)_theID : TheGUI.THE_MODE.NONE;
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
