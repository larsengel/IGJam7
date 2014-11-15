using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    private Light light;
    private float minIntensity = 0f;
    private float maxIntensity = 0.7f;
    private float timeWaiting = 10f;
    private float timeRandom = 15f;
    private float timeShowHide = 1.0f;
    private float currentTime = 8f;
    private enum LIGHTSTATUS { NONE, SHOW, HIDE };
    private LIGHTSTATUS currentLight = LIGHTSTATUS.NONE;


	// Use this for initialization
	void Start () {
        this.light = this.GetComponent<Light>();
        timeWaiting += Random.Range(0, timeRandom);
	
	}
	
	// Update is called once per frame
	void Update () {

        this.currentTime += Time.deltaTime;

        if(this.currentLight == LIGHTSTATUS.NONE && this.currentTime >= this.timeWaiting)
        {
            this.currentLight = LIGHTSTATUS.SHOW;
        }

        if(this.currentLight == LIGHTSTATUS.SHOW)
        {
            float diff = (this.maxIntensity - this.minIntensity) / this.timeShowHide;
            this.light.intensity += diff * Time.deltaTime;

            if(this.light.intensity >= this.maxIntensity)
            {
                this.light.intensity = this.maxIntensity;
                this.currentLight = LIGHTSTATUS.HIDE;
            }
        }
        else if(this.currentLight == LIGHTSTATUS.HIDE)
        {
            float diff = (this.maxIntensity - this.minIntensity) / this.timeShowHide;
            this.light.intensity -= diff * Time.deltaTime;

            if(this.light.intensity <= this.minIntensity)
            {
                this.light.intensity = this.minIntensity;
                this.currentLight = LIGHTSTATUS.NONE;
                currentTime = 0;

            }
        } 




	}
}
