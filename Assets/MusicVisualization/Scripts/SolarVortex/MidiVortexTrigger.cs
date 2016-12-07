using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MidiVortexTrigger : MidiEventTrigger
{

    //public ParticleSystem RippleParticle;
    public UnityEvent eventPlay;
    public UnityEvent eventPause;
    public UnityEvent eventStop;

    // Use this for initialization
    void Start()
    {
        //RippleParticle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnPlay()
    {
        //RippleParticle.Play();
        eventPlay.Invoke();
    }

    protected override void OnPause()
    {
        //RippleParticle.Pause();
        eventPause.Invoke();
    }

    protected override void OnResume()
    {
        //RippleParticle.Play();
        //eventPlay.Invoke();
    }

    protected override void OnStop()
    {
        //RippleParticle.Stop();
        //eventPause.Invoke();
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
