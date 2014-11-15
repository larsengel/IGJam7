using UnityEngine;
using System.Collections;

public class RocketBase : MonoBehaviour {

	public int engines = 0;
	public int modules = 0;

	public int rocketNumber;

	public bool isCountdownStarted = false;
	public float countdownValue;

	public int maxCountdown = 30;
	public int minCountdown = 10;
	public int secondsPerEngine = 5;

	void Update()
	{
		if(isCountdownStarted)
		{
			countdownValue -= Time.deltaTime;
			if(countdownValue <= 0)
			{
				Debug.Log("WIN! Player " + rocketNumber); 
			}
		}
	}

	public int getModuleCnt()
	{
		return engines+modules;
	}

	Vector2 getModuleCoords()
	{
		int cnt = getModuleCnt () - 1;
		Vector2 newCoords;

		float width = renderer.bounds.size.x;
		float height = renderer.bounds.size.y;

		int dir = rocketNumber == 1 ? 1 : -1; 

		newCoords.x = transform.position.x - width / 2;
		newCoords.y = transform.position.y - dir * height / 4;

		newCoords.x += width/3 * (cnt%3);
		newCoords.y += height/8 * (int)(cnt/3) * dir;

		return newCoords;
	}

	public void placeItem(GameObject item)
	{
		if(item.GetComponent<Item>().type == 1)
		{
			engines++;
		}
		else
		{
			modules++;
		}
		Vector2 ItemCoords = getModuleCoords ();
		item.transform.position = ItemCoords;
		//item.transform.localScale
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerScript player = other.GetComponent<PlayerScript>();
			if(player.playerNumber == rocketNumber)
				player.rocketBase = this;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerScript player = other.GetComponent<PlayerScript>();
			if(player.playerNumber == rocketNumber)
				player.rocketBase = null;
		}
	}

	public bool isStartable()
	{
		return engines > 0;
	}

	public void startCountdown()
	{
		countdownValue = Mathf.Max(minCountdown, maxCountdown - engines * secondsPerEngine);
		isCountdownStarted = true;
	}

}
