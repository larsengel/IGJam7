using UnityEngine;
using System.Collections;

public class TheCountdown : blindGUITexturedContainer
{
	private bool isCounting = false;

	public int currentDigit = 30;

	private float timer=30;

	public Texture2D[] DigitImages;

	public blindGUITexturedContainer digits;



	public override void Start()
	{
		base.Start();
		digits = this.transform.GetChild(0).gameObject.GetComponent<blindGUITexturedContainer>();
		m_alpha = 0;
	}

	public void TheUpdate()
	{
		if(isCounting)
		{
			timer -= Time.deltaTime;

			if(timer <= 0)
				StopCountdown();

			if((int)timer != currentDigit)
			{
				currentDigit = (int)timer;
				digits.m_backgroundTexture = DigitImages[currentDigit];
			}
		}
	}

	public void StartCountdown(int player,int startvalue)
	{
		if(player == 1)
			this.m_offset.x = -435;
		else if(player == 2)
			this.m_offset.y = 435;
		else
			return;

		timer = startvalue;
		digits.m_backgroundTexture = DigitImages[startvalue-1];
		m_enabled = isCounting = true;

		GameObject.Find("ScriptContainer/Countdown").GetComponent<AudioSource>().Play();
		m_alpha = 1;
	}

	public void StopCountdown()
	{
		m_alpha = 0;
		this.m_enabled = isCounting = false;
		GameObject.Find("ScriptContainer/Countdown").GetComponent<AudioSource>().Stop();
	}
}
