using UnityEngine;
using System.Collections;

public class SolarSystem : MidiEventTrigger
{

    //private ParticleSystem RippleParticle;
    //private GameObject MainCamera;
    //private GameObject CameraPos;
    private bool RotationOn;

    // Use this for initialization
    void Start()
    {
        //RippleParticle = GetComponent<ParticleSystem>();
        //MainCamera = GameObject.FindWithTag("MainCamera");
        //CameraPos = GameObject.Find("CameraPos");
        RotationOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(RotationOn == true)
        {
            //MainCamera.transform.position = CameraPos.transform.position;
            //MainCamera.transform.LookAt(this.transform.position);
            this.transform.Rotate(new Vector3(0, 50.0f, 0f) * Time.deltaTime);
        }
    }

    protected override void OnPlay()
    {
        RotationOn = true;
    }

    protected override void OnPause()
    {
        RotationOn = false;
    }

    protected override void OnResume()
    {
        RotationOn = true;
    }

    protected override void OnStop()
    {
        RotationOn = false;
    }

    protected override void OnNoteOn()
    {
        //RippleParticle.Play();
    }

    protected override void OnNoteOff()
    {
        //RippleParticle.Stop();
    }
}
