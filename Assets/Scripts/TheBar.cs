using UnityEngine;
using System.Collections;



public class TheBar : blindGUITexturedContainer
{
	private blindGUITexturedContainer bar;
	public int MAX;
	[SerializeField]
	private float _value;
	public float Value
	{
		get { return (bar.m_size.x / (float)MAX); }
		set 
		{
			value = value > 1 ? 1 : value < 0 ? 0 : value;
			_value = value;
			bar.m_size.x = (float)MAX * value; 
		}
	}
	


	public GlewGunScript player;

	public override void Start()
	{
		base.Start();
		bar = this.transform.GetChild(0).gameObject.GetComponent<blindGUITexturedContainer>();
	}

	public void SetGlueAmount(int playerNumber,int value)
	{
		this.transform.GetChild(0).gameObject.GetComponent<blindGUITexturedContainer>().m_size.x = ( value / 100 ) * 365;
	}

	public override void UpdateLayout()
	{
		base.UpdateLayout();
	}





}
