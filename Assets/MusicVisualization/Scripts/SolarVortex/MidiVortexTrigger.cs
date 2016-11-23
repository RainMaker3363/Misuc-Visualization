using UnityEngine;
using System.Collections;

public class MidiVortexTrigger : MidiEventTrigger
{

    private ParticleSystem RippleParticle;

    // Use this for initialization
    void Start()
    {
        RippleParticle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnPlay()
    {
    }

    protected override void OnPause()
    {

    }

    protected override void OnResume()
    {

    }

    protected override void OnStop()
    {

    }

    protected override void OnNoteOn()
    {
        RippleParticle.Play();
    }

    protected override void OnNoteOff()
    {
        RippleParticle.Stop();
    }
}
