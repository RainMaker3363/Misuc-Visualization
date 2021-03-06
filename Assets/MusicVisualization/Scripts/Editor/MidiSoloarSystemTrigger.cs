﻿using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

[CustomEditor(typeof(SolarSystem))]
public class MidiSoloarSystemTrigger : Editor {

    private bool foldout = false;
    private bool foidout2 = false;

    private MidiPlayer _MidiPlayer;
    private MidiTrack[] _Tracks;
    private MidiNote[] _Notes;

    SerializedProperty eventNoteOn;
    SerializedProperty eventNoteOff;

    SerializedProperty eventPlay;
    SerializedProperty eventPause;
    SerializedProperty eventStop;

    void OnEnable()
    {
        eventNoteOn = serializedObject.FindProperty("eventNoteOn");
        eventNoteOff = serializedObject.FindProperty("eventNoteOff");

        eventPlay = serializedObject.FindProperty("eventPlay");
        eventPause = serializedObject.FindProperty("eventPause");
        eventStop = serializedObject.FindProperty("eventStop");

        _MidiPlayer = GameObject.FindObjectOfType<MidiPlayer>();

        if (_MidiPlayer != null)
        {
            _Tracks = _MidiPlayer.Tracks;

        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(eventNoteOn, new GUIContent("Note On"));
        EditorGUILayout.PropertyField(eventNoteOff, new GUIContent("Note Off"));

        EditorGUILayout.PropertyField(eventPlay, new GUIContent("Play"));
        EditorGUILayout.PropertyField(eventPause, new GUIContent("Pause"));
        EditorGUILayout.PropertyField(eventStop, new GUIContent("Stop"));

        serializedObject.ApplyModifiedProperties();


        SolarSystem trigger = (SolarSystem)target;

        foldout = EditorGUILayout.Foldout(foldout, "Instrument Filter");

        if(foldout)
        {
            if(GUILayout.Button("Check All"))
            {
                for (int i = 0; i < 129; i++)
                {
                    trigger.instrumentFilter[i] = true;
                }
            }

            if (GUILayout.Button("Clear All"))
            {
                for (int i = 0; i < 129; i++)
                {
                    trigger.instrumentFilter[i] = false;
                }
            }

            for (int i = 0; i < _Tracks.Length; i++)
            {
                bool newValue = EditorGUILayout.Toggle(_Tracks[i].InstrumentName, trigger.instrumentFilter[i]);
                //Debug.Log(trigger.instrumentFilter[i]);

                if (newValue != trigger.instrumentFilter[i])
                {
                    trigger.instrumentFilter[i] = newValue;

                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
        }

        foidout2 = EditorGUILayout.Foldout(foidout2, "Note Filter");

        if (foidout2)
        {
            if (GUILayout.Button("Check All"))
            {
                for (int i = 0; i < 128; i++)
                {
                    trigger.noteFilter[i] = true;
                }
            }

            if (GUILayout.Button("Clear All"))
            {
                for (int i = 0; i < 128; i++)
                {
                    trigger.noteFilter[i] = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("C"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 0)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("C#"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 1)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("D"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 2)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("D#"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 3)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("E"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 4)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("F"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 5)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("F#"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 6)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("G"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 7)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("G#"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 8)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("A"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 9)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("A#"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 10)
                        trigger.noteFilter[i] = true;
                }
            }
            if (GUILayout.Button("B"))
            {
                for (int i = 0; i < 128; i++)
                {
                    if ((int)(i % 12) == 11)
                        trigger.noteFilter[i] = true;
                }
            }

            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < 128; i++)
            {
                bool newValue = EditorGUILayout.Toggle(NoteNumberToString(i), trigger.noteFilter[i]);

                if (newValue != trigger.noteFilter[i])
                {
                    trigger.noteFilter[i] = newValue;

                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
        }
    }

    string NoteNumberToString(int number)
    {
        int index = (int)(number % 12);
        int octave = (int)(number / 12);

        switch (index)
        {
            case 0:
                return string.Format("C{0:d}", octave);

            case 1:
                return string.Format("C#{0:d}", octave);

            case 2:
                return string.Format("D{0:d}", octave);

            case 3:
                return string.Format("D#{0:d}", octave);

            case 4:
                return string.Format("E{0:d}", octave);

            case 5:
                return string.Format("F{0:d}", octave);

            case 6:
                return string.Format("F#{0:d}", octave);

            case 7:
                return string.Format("G{0:d}", octave);

            case 8:
                return string.Format("G#{0:d}", octave);

            case 9:
                return string.Format("A{0:d}", octave);

            case 10:
                return string.Format("A#{0:d}", octave);

            case 11:
                return string.Format("B{0:d}", octave);
        }

        return "Unknown";
    }
}
