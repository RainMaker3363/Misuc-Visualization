using UnityEngine;
using UnityEditor;
using System.Collections;

//[CustomEditor(typeof(MidiPlayer))]
public class MidiPlayerEditor : Editor {

    SerializedProperty midi;
    SerializedProperty Music;
    SerializedProperty audioSource;
    SerializedProperty playDelayTime;
    SerializedProperty playSpeed;


    void OnEnable()
    {
        midi = serializedObject.FindProperty("midi");
        midi = serializedObject.FindProperty("Music");
        midi = serializedObject.FindProperty("audioSource");
        midi = serializedObject.FindProperty("playDelayTime");
        midi = serializedObject.FindProperty("playSpeed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(midi);
        EditorGUILayout.PropertyField(Music);
        EditorGUILayout.PropertyField(audioSource);
        EditorGUILayout.PropertyField(playDelayTime);
        EditorGUILayout.PropertyField(playSpeed);

        serializedObject.ApplyModifiedProperties();

        MidiPlayer midiPlayer = (MidiPlayer)target;

        // 현재 어플레케이션이 실행중인지 체크한다.
        if(Application.isPlaying == true)
        {
            GUILayout.BeginHorizontal();

            if(midiPlayer.isPlaying == true)
            {
                if (GUILayout.Button("Pause") == true)
                {
                    midiPlayer.Pause();
                }

                // 에디터에게 다시 랜더링을 시키라는 의미이다.
                // 대신 많이 호출하면 연산량이 많아지므로 주의
                EditorUtility.SetDirty(target);
            }
            else
            {
                if(midiPlayer.playTime == 0)
                {
                    if (GUILayout.Button("Play") == true)
                    {
                        midiPlayer.Play();
                    }
                }
                else
                {

                    if (GUILayout.Button("Resume") == true)
                    {
                        midiPlayer.Resume();
                    }
                }
            }

            if (GUILayout.Button("Stop") == true)
            {
                midiPlayer.Stop();
            }

            GUILayout.EndHorizontal();

            // 재생시간 Bar를 그려주는 역할이다.
            GUILayout.HorizontalSlider(midiPlayer.playTime, 0f, midiPlayer.totalTime);
            EditorGUILayout.LabelField(string.Format("Time : {0:F1} sec", midiPlayer.playTime));
        }
    }
}
