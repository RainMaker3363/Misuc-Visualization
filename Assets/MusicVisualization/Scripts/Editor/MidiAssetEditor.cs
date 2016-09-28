using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

[CustomEditor(typeof(MidiAsset))]
public class MidiAssetEditor : Editor
{
    private bool _foldout;

    void OnEnable()
    {
        //Debug.Log("Click");

        _foldout = true;
    }

    public override void OnInspectorGUI()
    {
        MidiAsset midiAsset = (MidiAsset)target;

        GUILayout.Label("File Name : " + Path.GetFileNameWithoutExtension(midiAsset.FileName));
        GUILayout.Label(string.Format("Total Time : {0:f} Sec", midiAsset.TotalTime));

        // 그룹을 만들어 보여준다.
        _foldout = EditorGUILayout.Foldout(_foldout, "Time Signiture");
        
        if(_foldout == true)
        {
            // 들여쓰기 용도
            // Level이 높을수록 밖으로 들여쓰기가 된다
            EditorGUI.indentLevel++;
            GUILayout.Label(string.Format("PPQN : {0:d}", midiAsset.PPQN));
            GUILayout.Label(string.Format("Pulse : {0:f} Sec", midiAsset.PulseTime));
            GUILayout.Label(string.Format("BPM : {0:d}", midiAsset.BPM)); ;
            GUILayout.Label(string.Format("Numerator : {0:d}", midiAsset.Numerator));
            GUILayout.Label(string.Format("Denominator : {0:d}", midiAsset.Denominator));
            EditorGUI.indentLevel--;
        }
        
        if(GUILayout.Button("Track Viewer") == true)
        {
            MidiTrackWindow.ShowWindow();
        }
    }
}
