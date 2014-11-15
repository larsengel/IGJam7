using UnityEngine;
using System.Collections;

public class GlueCharge : MonoBehaviour
{
	
	private bool Engaged;
	[SerializeField]
	private float speed;
	private bool _hit;
	public bool Hit
	{
		get { return _hit; }
		set 
		{
			if(_hit != value && value)
			{
				_hit = value;
				OnHit();
			}
		}
	}

	void Start ()
	{
		_hit = false;
		Engaged = false;
	}
	

	public void DoGlew()
	{
		if(Engaged)
			Hit=flow();
		if(Hit)
			GameObject.Destroy(this.gameObject);
	}

	public void fire(Vector2 direction)
	{ 
		this.transform.forward = direction;
	}

	public bool flow()
	{
		transform.position += (transform.forward * Time.deltaTime * speed);

		//todo: return true if hit something..
		return false;
	}

	private void OnHit()
	{
		// todo: glew the hitten !
	}
}
