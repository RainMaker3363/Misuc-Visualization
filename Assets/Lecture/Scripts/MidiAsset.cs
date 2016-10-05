using UnityEngine;
using System.Collections;

public class MidiAsset : ScriptableObject
{
    [SerializeField]
    private MidiFile _midiFile;

    public void FileLoad(string path)
    {
        _midiFile = new MidiFile(path);
    }

    public string FileName
    {
        get
        {
            return _midiFile.FileName;
        }
    }

    public float PulseTime
    {
        get
        {
            return (_midiFile.Time.Tempo / _midiFile.Time.Quarter) / 1000000f;
        }
    }

    public float TotalTime
    {
        get
        {
            return _midiFile.TotalPulses * PulseTime;
        }
    }

    public int Numerator
    {
        get
        {
            return _midiFile.Time.Numerator;
        }
    }

    public int Denominator
    {
        get
        {
            return _midiFile.Time.Denominator;
        }
    }

    public int PPQN
    {
        get
        {
            return _midiFile.Time.Quarter;
        }
    }

    public int BPM
    {
        get
        {
            return (int)(60000000f / _midiFile.Time.Tempo);
        }
    }

    public MidiTrack[] Tracks
    {
        get
        {
            return _midiFile.Tracks.ToArray();
        }
    }
}
