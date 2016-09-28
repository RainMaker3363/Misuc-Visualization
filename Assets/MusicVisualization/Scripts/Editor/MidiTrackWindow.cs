using UnityEngine;
using UnityEditor;
using System.Collections;

public class MidiTrackWindow : EditorWindow
{
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MidiTrackWindow), false, "Midi Track");

    }

    void OnGUI()
    {
        Rect rect;
        float TitleHeight = 30f;
        float MusicalScaleWidth = 60f;
        float TimeHeight = 50f;

        //============================================= Title Area ===========================================================
        
        // Create Title Area
        rect = new Rect(0, 0, position.width, TitleHeight);

        // GUI 영역을 만들어 구역을 설정한다.
        
        // 경계선을 보여주기 위한 더미박스
        GUI.Box(rect, "");
        
        // BeginArea를 만들면 그 후에 닫아줘야 한다.
        GUILayout.BeginArea(rect);
        GUILayout.EndArea();

        //============================================= Musical Area ===========================================================
        rect = new Rect(0, TitleHeight, MusicalScaleWidth, position.height - TitleHeight);

        GUI.Box(rect, "");

        // 수동으로 그려줘야 하는 영역은 GUI.BeginGroup을 써준다
        GUI.BeginGroup(rect);
        GUI.EndGroup();

        //============================================= Track List Area ===========================================================

        rect = new Rect(position.width * 0.75f, TitleHeight, position.width * 0.25f, position.height - TitleHeight);

        GUI.Box(rect, "");

        GUILayout.BeginArea(rect);
        GUILayout.EndArea();

        //============================================= Time Area ===========================================================

        rect = new Rect(MusicalScaleWidth, TitleHeight, position.width - (MusicalScaleWidth + (position.width * 0.25f)), TimeHeight);

        GUI.Box(rect, "");

        GUILayout.BeginArea(rect);
        GUILayout.EndArea();

        //============================================= Note Area ===========================================================

        rect = new Rect(MusicalScaleWidth, (TitleHeight + TimeHeight), position.width - (MusicalScaleWidth + (position.width * 0.25f)), position.height - (TitleHeight + TimeHeight));

        GUI.Box(rect, "");

        // 수동으로 그려줘야 하는 영역은 GUI.BeginGroup을 써준다
        GUI.BeginGroup(rect);
        GUI.EndGroup();
    }
}
