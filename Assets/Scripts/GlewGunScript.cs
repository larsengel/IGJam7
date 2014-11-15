using UnityEngine;
using System.Collections;

public class GlewGunScript : MonoBehaviour 
{

	[SerializeField]
	private int _glue;
	public int Glue
	{
		get { return _glue; }
		set { _glue = value; }
	}
	[SerializeField]
	private GameObject chargePrefab;
	private GameObject charge;
	public bool Loadet
	{
		get { return charge != null; }
	}

	void Start()
	{
		_glue = 100;
	}

public	float test;
	private float rotator
	{
		get 
		{
			return test =( (float)( (int)this.transform.parent.gameObject.GetComponent<PlayerScript>().gunDirection )*3 );
		}
	}
	
	public void Reloade() 
	{
		this.transform.position = this.transform.parent.gameObject.transform.position;
		this.transform.Rotate(this.transform.forward, rotator);

		if(Loadet)
			charge.GetComponent<GlueCharge>().DoGlew();
		else if(Glue > 0)
			charge = GameObject.Instantiate(chargePrefab, this.transform.position, this.transform.rotation) as GameObject;

	}

	public void Fire()
	{
		if(Loadet)
		{
			Debug.Log("loadedt");
			charge.GetComponent<GlueCharge>().fire(this.transform.forward);
			charge = null;
			TheHUD.TheLiveBar.SetGlueAmount(this.transform.parent.GetComponent<PlayerScript>().playerNumber,this.Glue);
		}
	}
}
