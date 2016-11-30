using UnityEngine;
using System.Collections;

public class MainVortex : MidiEventTrigger
{

    //private ParticleSystem RippleParticle;

    private bool RotationOn;

    // Use this for initialization
    void Start()
    {
        //RippleParticle = GetComponent<ParticleSystem>();

        RotationOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(RotationOn == true)
        {
            
            this.transform.Rotate(new Vector3(0f, 0f, 100.0f) * Time.deltaTime);
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
