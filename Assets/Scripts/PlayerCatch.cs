using UnityEngine;
using System.Collections;

public class PlayerCatch : MonoBehaviour {

    private PlayerScript player;

	// Use this for initialization
	void Start () {
        player = this.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {


        switch (player.playerNumber)
        {
            case '1':
                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    Catch();
                break;
            case '2':
                if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                    Catch();
                break;
        }
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item" && player.catchObject != null)
        {
            player.catchObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Item" && player.catchObject == other)
        {
            player.catchObject = null;
        }

    }

    private void Catch()
    {

    }
}
