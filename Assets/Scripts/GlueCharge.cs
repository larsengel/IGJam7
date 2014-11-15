using UnityEngine;
using System.Collections;

public class GlueCharge : MonoBehaviour
{
	public int MAXIMUM_RANGE=10;
	private bool Engaged;
	[SerializeField]
	private float speed;
	public float deltaSpeed
	{
		get { return speed * Time.deltaTime; }
	}
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

	// The Update-function:
	public void DoGlew()
	{
		if(Engaged)
			Hit=flow();
		if(Hit)
			GameObject.Destroy(this.gameObject);
	}

	public void fire(Vector2 direction)
	{
		Engaged = true;
		this.transform.forward = direction;
	}

	public bool flow()
	{
		Vector3 scale = this.gameObject.transform.transform.localScale;
		scale.y += deltaSpeed;
		this.gameObject.transform.localScale = scale; 
		return (scale.y > MAXIMUM_RANGE);
	}

	private void OnHit()
	{
		// todo: glew the hitten !
	}
}
