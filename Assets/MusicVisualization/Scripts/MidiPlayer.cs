using UnityEngine;
using System.Collections;

// Eidtor 모드에서도 아래의 함수들이 작동되게 만든다.
//[ExecuteInEditMode]
public class MidiPlayer : MonoBehaviour {

    public MidiAsset midi;

    public AudioClip Music;
    public AudioSource audioSource;

    public float playDelayTime = 0.0f;
    public float playSpeed = 1.0f;

    // Midi 이벤트를 받아서 처리하는 역할
    private MidiEventTrigger[] _triggers;

    private bool _isPlaying = false;
    private float _playTime = 0.0f;
    private float _totalTime = 0.0f;
    private float _audioDelayTime;
    private float _midiDelayTime;

    private bool _audioStarted = false;

    private MidiTrack[] _tracks;
    private int[] _noteIndex;
    private float _pulseTime;

	// Use this for initialization
	void Start () {

        //audioSource.clip = Music;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(_isPlaying == true)
        {
            if(_audioDelayTime > 0f)
            {
                _audioDelayTime -= Time.deltaTime;
            }
            else
            {
                if(_audioStarted == false)
                {
                    _audioStarted = true;

                    if (audioSource != null)
                    {
                        audioSource.Play();
                    }

                }

            }


            if (_midiDelayTime > 0f)
            {
                _midiDelayTime -= Time.deltaTime;
            }
            else
            {
                _playTime += (Time.deltaTime * playSpeed);

                for (int i = 0; i < _tracks.Length; i++)
                {
                    int noteCount = _tracks[i].Notes.Count;

                    for (int j = _noteIndex[i]; j < noteCount; j++)
                    {
                        MidiNote note = _tracks[i].Notes[j];
                        float sTime = note.StartTime * _pulseTime;
                        float eTime = note.EndTime * _pulseTime;

                        if (_playTime < sTime)
                        {
                            break;
                        }
                        else if(_playTime >= sTime && _playTime <= eTime)
                        {
                            // MIDI Event Call
                            foreach (MidiEventTrigger trigger in _triggers)
                            {
                                trigger.NoteOn(_tracks[i].Instrument, note.Number);
                            }
                        }
                        
                        if (_playTime > eTime)
                        {
                            _noteIndex[i] = j + 1;

                            // MIDI Event Call
                            foreach (MidiEventTrigger trigger in _triggers)
                            {
                                trigger.NoteOff(_tracks[i].Instrument, note.Number);
                            }
                        }
                            
                    }
                }
            }
            //_playTime += Time.deltaTime;
        }
	}

    public void Play()
    {
        if(midi == null)
        {
            return;
        }

        if (Music != null && audioSource != null)
        {
            audioSource.clip = Music;
        }

        if(audioSource != null)
        {
            audioSource.Play();
        }

        _isPlaying = true;
        _playTime = 0.0f;
        _totalTime = midi.TotalTime;
        _pulseTime = midi.PulseTime;

        if(playDelayTime == 0f)
        {
            _audioDelayTime = 0.0f;
            _midiDelayTime = 0f;
        }
        else if(playDelayTime > 0.0f)
        {
            _audioDelayTime = playDelayTime;
            _midiDelayTime = 0f;
        }
        else
        {
            _audioDelayTime = 0f;
            _midiDelayTime = -playDelayTime;
        }

        _audioStarted = false;

        _tracks = midi.Tracks;
        _noteIndex = new int[_tracks.Length];

        for (int i = 0; i < _noteIndex.Length; i++)
            _noteIndex[i] = 0;


        // Find Event Trigger
        _triggers = GameObject.FindObjectsOfType<MidiEventTrigger>();
        Debug.Log(_triggers.Length);

        // MIDI Event Call
        foreach(MidiEventTrigger trigger in _triggers)
        {
            trigger.Play();
        }
    }

    public void Pause()
    {
        _isPlaying = false;

        if (audioSource != null)
        {
            audioSource.Pause();
        }

        // MIDI Event Call

        foreach (MidiEventTrigger trigger in _triggers)
        {
            trigger.Pause();
        }
    }

    public void Resume()
    {
        _isPlaying = true;

        if (audioSource != null)
        {
            audioSource.UnPause();
        }

        // MIDI Event Call
        foreach (MidiEventTrigger trigger in _triggers)
        {
            trigger.Resume();
        }
    }

    public void Stop()
    {
        _isPlaying = false;
        _playTime = 0.0f;

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // MIDI Event Call
        foreach (MidiEventTrigger trigger in _triggers)
        {
            trigger.Stop();
        }
    }

    public bool isPlaying
    {
        get
        {
            return _isPlaying;
        }
    }

    public float totalTime
    {
        get
        {
            return _totalTime;
        }
    }

    public float playTime
    {
        get
        {
            return _playTime;
        }
    }
}
