using UnityEngine;
using System.Collections;

public class GlewGunScript : MonoBehaviour {

	[SerializeField]
	private int _glue;
	public int Glue
	{
		get { return _glue; }
		set { _glue = value; }
	}
	[SerializeField]
	private GameObject chargePrefab;
	private GlueCharge charge;
	public bool Loadet
	{
		get { return charge != null; }
	}
	void Start()
	{
		_glue = 100;
	}
	

	
	public void Reloade() 
	{
		this.transform.position = this.transform.parent.gameObject.transform.position;

		if(Glue > 0 && !Loadet)
			charge = ( GameObject.Instantiate(chargePrefab, this.transform.position, this.transform.rotation) as GameObject ).GetComponent<GlueCharge>();
		else
			charge.DoGlew();
	}

	public void Fire()
	{
		charge.fire(this.transform.forward);
		charge = null;
		Glue -= 1;
	}
}
