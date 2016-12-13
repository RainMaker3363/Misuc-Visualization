using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MainVortex : MidiEventTrigger
{

    //private ParticleSystem RippleParticle;

    private bool RotationOn;

    public UnityEvent eventPlay;
    public UnityEvent eventPause;
    public UnityEvent eventStop;

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

        eventPlay.Invoke();
    }

    protected override void OnPause()
    {
        RotationOn = false;
        eventPause.Invoke();
    }

    protected override void OnResume()
    {
        RotationOn = true;
        eventPlay.Invoke();
    }

    protected override void OnStop()
    {
        RotationOn = false;
        eventStop.Invoke();
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
