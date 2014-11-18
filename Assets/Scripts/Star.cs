using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    private float minIntensity = 0f;
    private float maxIntensity = 0.9f;
    private float timeWaitingDefault = 5.0f;
    private float timeWaitingCurrent = 0;
    private float timeRandom = 5.0f;
    private float timeShowHide = 1.0f;
    private float currentTime = 0f;
    private enum LIGHTSTATUS { NONE, SHOW, HIDE };
    private LIGHTSTATUS currentLight = LIGHTSTATUS.NONE;


	// Use this for initialization
	void Start () {
        DoRandomTime();
	}
	
	// Update is called once per frame
	void Update () {

        this.currentTime += Time.deltaTime;

        if (this.currentLight == LIGHTSTATUS.NONE && this.currentTime >= this.timeWaitingCurrent)
        {
            this.currentLight = LIGHTSTATUS.SHOW;
            this.light.enabled = true;
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
                this.light.enabled = false;
                DoRandomTime();
            }
        } 
	}

    void DoRandomTime()
    {
        this.timeWaitingCurrent = this.timeWaitingDefault + Random.Range(0, this.timeRandom);
    }

}
