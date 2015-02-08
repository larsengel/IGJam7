using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketBase : MonoBehaviour {

	public int engines = 0;
	public int modules = 0;

	public int rocketNumber;

	public bool isCountdownStarted = false;
	public float countdownValue;

	public int maxCountdown = 30;
	public int minCountdown = 10;
	public int secondsPerEngine = 5;

	public List<GameObject> placedItems = new List<GameObject>();

	public bool isPlayerOnBase = false;

	void Update()
	{
			if(isCountdownStarted)
			{
				countdownValue -= Time.deltaTime;
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

		if(dir == -1)
			newCoords.y = (transform.position.y-0.2f) - dir * height / 4;
		else
			newCoords.y = transform.position.y - dir * height / 4;

		newCoords.x += width/3f * (cnt%3);
		newCoords.x += 0.1f * width;
		newCoords.y += height/8 * (int)(cnt/3) * dir;

		return newCoords;
	}

	public void placeItem(GameObject item)
	{
		if(item.GetComponent<Item>().type == 1)
			engines++;
		else
			modules++;
		
		Vector3 theScale = item.transform.localScale;
		theScale.x = 0.35f;
		theScale.y = 0.35f;
		item.transform.localScale = theScale;
        Vector2 ItemCoords = getModuleCoords();
        ItemCoords.x += item.renderer.bounds.extents.x;
        ItemCoords.y += item.renderer.bounds.extents.y;


		item.transform.position = ItemCoords;
		item.transform.rotation = Quaternion.identity;
		placedItems.Add(item);
		GameObject.Find("ScriptContainer/ModuleToRocket").GetComponent<AudioSource>().Play();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerScript player = other.GetComponent<PlayerScript>();
			if(player.playerNumber == rocketNumber)
			{
				player.rocketBase = this;
				isPlayerOnBase = true;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerScript player = other.GetComponent<PlayerScript>();
			if(player.playerNumber == rocketNumber)
			{
				player.rocketBase = null;
				isPlayerOnBase = false;
			}
		}
	}

	public bool isStartable()
	{
        return (getModuleCnt() >= 6 && engines > 1);
	}

	public void startCountdown()
	{
		countdownValue = Mathf.Max(minCountdown, maxCountdown - engines * secondsPerEngine);
		isCountdownStarted = true;
		GameObject.Find("ScriptContainer/Engine").GetComponent<AudioSource>().Play();
		if(this.rocketNumber == 1)
			GameObject.Find("DustA").GetComponent<Animator>().SetBool("started", true);
		if(this.rocketNumber == 2)
			GameObject.Find("DustB").GetComponent<Animator>().SetBool("started", true);

		//TheGUI.TheHud.TheDowncounter.StartCountdown(rocketNumber, (int)countdownValue);
	}

	bool isExplosionStarted = false;

	void OnGUI()
	{
		if(isStartable() && isPlayerOnBase && !isCountdownStarted)
			GUI.TextField(new Rect(10, 10, 200, 20), "Press Y to Launch Rocket!", 25);

		if(isCountdownStarted)
		{
	//		GUI.TextField(new Rect(10, 10, 200, 20), "Countdown started...", 25);
	//		GUI.TextField(new Rect(Screen.width-250, 10, 200, 20), (int)countdownValue+"", 25);
		}
		if(countdownValue <= 0 && isCountdownStarted)
		{
      //      GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "WIN! Player " + rocketNumber + "(" + (rocketNumber == 1 ? "red" : "blue") + ")", 25);
			//Time.timeScale = 0;
			
			if(rocketNumber==1)
				Endscreen.winner = 1;
			if(rocketNumber == 2)
				Endscreen.winner = 2;

			Application.LoadLevel("endscreen");
		}
		if(placedItems.Count == 0 && isCountdownStarted)
		{
    //        GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "WIN! Player " + (rocketNumber == 1 ? 2 : 1) + "(" + (rocketNumber == 2 ? "red" : "blue") + ")", 25);

            if (!isExplosionStarted)
            {
                if (rocketNumber == 1)
                    GameObject.Find("ExplosionA").GetComponent<Animator>().SetBool("explosion", true);
                if (rocketNumber == 2)
                    GameObject.Find("ExplosionB").GetComponent<Animator>().SetBool("explosion", true);
                isExplosionStarted = true;
                GameObject.Find("ScriptContainer/Explosion").GetComponent<AudioSource>().Play();
            }


		}
	}
	
}
