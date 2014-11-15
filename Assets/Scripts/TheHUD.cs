using UnityEngine;
using System.Collections;



public class TheHUD : blindGUITexturedContainer
{


	private const int _theID = 1;
	public static TheHUD TheLiveBar;
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






*/

}
