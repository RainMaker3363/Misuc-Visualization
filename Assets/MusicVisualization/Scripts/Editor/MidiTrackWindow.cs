using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class MidiTrackWindow : EditorWindow
{
    static private MidiAsset _midi;
    private Vector2 _TrackListScroll = Vector2.zero;
    private Vector2 _NoteAreaScroll = Vector2.zero;
    private bool[] _enableTracks;

    const float TitleHeight = 30f;
    const float MusicalScaleWidth = 60f;
    const float TimeHeight = 50f;
    const float GridX = 0.5f;
    const float GridY = 30f;


    public static void ShowWindow(MidiAsset midi)
    {
        _midi = midi;
        EditorWindow.GetWindow(typeof(MidiTrackWindow), false, "Midi Track");

    }

    void OnGUI()
    {

        if (_midi == null)
            return;

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
        GUI.BeginGroup(rect);

        DrawTitleArea(rect.width, rect.height);

        GUI.EndGroup();

        //============================================= Musical Area ===========================================================
        rect = new Rect(0, TitleHeight + TimeHeight, MusicalScaleWidth, position.height - TitleHeight);

        GUI.Box(rect, "");

        // 수동으로 그려줘야 하는 영역은 GUI.BeginGroup을 써준다
        GUI.BeginGroup(rect);

        DrawMusicalScaleArea(rect.width, rect.height);

        GUI.EndGroup();

        //============================================= Track List Area ===========================================================

        rect = new Rect(position.width * 0.75f, TitleHeight, position.width * 0.25f, position.height - TitleHeight);

        GUI.Box(rect, "");

        GUILayout.BeginArea(rect);

        DrawTrackListArea();

        GUILayout.EndArea();

        //============================================= Time Area ===========================================================

        rect = new Rect(MusicalScaleWidth, TitleHeight, position.width - (MusicalScaleWidth + (position.width * 0.25f)), TimeHeight);

        GUI.Box(rect, "");

        GUILayout.BeginArea(rect);

        DrawTimeArea(rect.width, rect.height);

        
        GUILayout.EndArea();

        //============================================= Note Area ===========================================================

        rect = new Rect(MusicalScaleWidth, (TitleHeight + TimeHeight), position.width - (MusicalScaleWidth + (position.width * 0.25f)), position.height - (TitleHeight + TimeHeight));

        GUI.Box(rect, "");

        // 수동으로 그려줘야 하는 영역은 GUI.BeginGroup을 써준다
        GUI.BeginGroup(rect);

        DrawNoteArea(rect.width, rect.height);

        GUI.EndGroup();
    }

    void DrawTitleArea(float width, float height)
    {
        Rect rect = new Rect(0, 0, width, height);
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Bold;

        GUI.Label(rect, Path.GetFileNameWithoutExtension(_midi.FileName), style);
        //EditorGUILayout.LabelField(Path.GetFileNameWithoutExtension(_midi.FileName));
    }

    void DrawMusicalScaleArea(float width, float height)
    {
        Rect rect = new Rect(0, 0, width, height);

        GUIStyle style = new GUIStyle(GUI.skin.label);

        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Italic;

        // 노트 스크롤을 계산

        int sNote = (int)(_NoteAreaScroll.y / GridY);
        int eNote = (int)((_NoteAreaScroll.y + height) / GridY);

        float sy = -(_NoteAreaScroll.y % GridY);

        Rect rect2 = new Rect(0, sy, width, GridY);

        for (int i = sNote; i <= eNote; i++)
        {
            GUI.Box(rect2, "");
            GUI.Label(rect2, NoteNumberToString(i), style);
            rect2.y += GridY;
        }
    }

    void DrawTrackListArea()
    {
        if(_enableTracks == null)
        {
            _enableTracks = new bool[_midi.Tracks.Length];

            for(int i =0; i<_enableTracks.Length; i++)
            {
                _enableTracks[i] = true;
            }
        }
        

        // 내부적으로 스크롤을 사용하기 위한 함수.
        _TrackListScroll = EditorGUILayout.BeginScrollView(_TrackListScroll);
        for (int i = 0; i < _midi.Tracks.Length; i++ )
        {
            _enableTracks[i] = GUILayout.Toggle(_enableTracks[i], _midi.Tracks[i].InstrumentName);
        }

        EditorGUILayout.EndScrollView();
    }

    void DrawTimeArea(float width, float height)
    {
        Rect areaRect = new Rect(0, 0, width, height);
        int sTime = (int)(_NoteAreaScroll.x / GridX);
        int eTime = (int)((_NoteAreaScroll.x + width) / GridX);

        //=================================================================================================
        // Draw Text Area
        //=================================================================================================
        GUIStyle style = new GUIStyle(GUI.skin.label);

        style.alignment = TextAnchor.MiddleLeft;
        style.fontStyle = FontStyle.Italic;

        int sText = (int)(sTime / 100);
        int eText = (int)(eTime / 100);

        float TextW = GridX * 100;
        float TextH = height * 0.4f;

        float sTextX = -(_NoteAreaScroll.x % TextW);

        Rect textRect = new Rect(sTextX, 0, TextW, TextH);

        for (int i = sText; i <= eText; i++)
        {
            GUI.Label(textRect, string.Format("{0:f1}", i * 0.1f), style);
            textRect.x += TextW;
        }

        //=================================================================================================
        // Draw Line Area
        //=================================================================================================

        int sLine = (int)(sTime / 10);
        int eLine = (int)(eTime / 10);

        float LineW = GridX * 10;
        float LineH = height * 0.6f;

        float sLineX = -(_NoteAreaScroll.x % LineW);

        Texture2D lineTexture = new Texture2D(1, 1);
        lineTexture.SetPixel(0, 0, Color.black);
        lineTexture.Apply();

        Rect pixelRect = new Rect(0, 0, 1, 1);
        int longLineY = (int)TextH;
        int shortLineY = (int)(longLineY + LineH * 0.5f);
        int eLineY = (int)(height);

        //Rect lineRect = new Rect(0, 0, LineW, LineH);

        for (int i = sLine; i <= eLine; i++)
        {
            pixelRect.x = sLineX;

            if(i % 10 == 0) //  Long Line
            {
                for (int j = longLineY; j <= eLineY; j++ )
                {
                    pixelRect.y = j;

                    GUI.DrawTexture(pixelRect, lineTexture);
                }
                    
            }
            else            //  Short Line
            {
                for (int j = shortLineY; j <= eLineY; j++)
                {
                    pixelRect.y = j;

                    GUI.DrawTexture(pixelRect, lineTexture);
                }
            }

            sLineX += LineW;
        }
    }

    void DrawNoteArea(float width, float height)
    {
        Rect VeiwRect = new Rect(0, 0, width, height);
        Rect ContextRect =  new Rect(0, 0, GridX * (_midi.TotalTime * 1000f), GridY * 128);

        _NoteAreaScroll = GUI.BeginScrollView(VeiwRect, _NoteAreaScroll, ContextRect);

        Rect ScrollRect = new Rect(_NoteAreaScroll.x, _NoteAreaScroll.y, width, height);

        // 1픽셀 텍스쳐를 만들어준다.
        Texture2D BoxTexture = new Texture2D(1,1);
        BoxTexture.SetPixel(0, 0, Color.blue);

        // 반드시 써줘야 텍스쳐가 적용된다.
        BoxTexture.Apply();

        for(int i = 0; i< _midi.Tracks.Length; i++)
        {
            if(_enableTracks[i] == false)
            {
                continue;
            }

            MidiNote[] notes = _midi.Tracks[i].Notes.ToArray();

            for(int j = 0; j< notes.Length; j++)
            {
                float sTime = notes[j].StartTime * (_midi.PulseTime * 1000f);
                float eTime = notes[j].EndTime * (_midi.PulseTime * 1000f);

                // number는 음계번호를 의미한다
                Rect NoteRect = new Rect(GridX * sTime, GridY * notes[j].Number, GridX * (eTime - sTime), GridY);

                // 서로의 Rect 영역이 겹쳐있는지 확인해주는 함수
                if (ScrollRect.Overlaps(NoteRect) == true)
                {
                    GUI.DrawTexture(NoteRect, BoxTexture);
                }
                
            }
        }

        GUI.EndScrollView();
    }

    string NoteNumberToString(int number)
    {
        int Index = (int)(number % 12);
        int Octave = (int)(number / 12);

        switch(Index)
        {
            case 0:
                {
                    return string.Format("C{0:d}", Octave);
                }

            case 1:
                {
                    return string.Format("C#{0:d}", Octave);
                }

            case 2:
                {
                    return string.Format("D{0:d}", Octave);
                }

            case 3:
                {
                    return string.Format("D#{0:d}", Octave);
                }

            case 4:
                {
                    return string.Format("E{0:d}", Octave);
                }

            case 5:
                {
                    return string.Format("F{0:d}", Octave);
                }

            case 6:
                {
                    return string.Format("F#{0:d}", Octave);
                }

            case 7:
                {
                    return string.Format("G{0:d}", Octave);
                }

            case 8:
                {
                    return string.Format("G#{0:d}", Octave);
                }

            case 9:
                {
                    return string.Format("A{0:d}", Octave);
                }

            case 10:
                {
                    return string.Format("A#{0:d}", Octave);
                }

            case 11:
                {
                    return string.Format("B{0:d}", Octave);
                }

        }


        return "UnKnown";
    }
}
