using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyEditorWindow : EditorWindow 
{
    [MenuItem("My Menu/Show My Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyEditorWindow), false, "My Window");

    }

    [MenuItem("My Menu/Add MyComponent")]
    public static void AddMyComponent()
    {
        if(Selection.activeGameObject != null)
        {
            Selection.activeGameObject.AddComponent<MyComponent>();
        }
        
    }

    [MenuItem("My Menu/Add MyComponent", true)]
    public static bool ValidateAddMyComponent()
    {
        if (Selection.activeGameObject == null)
            return false;
        else
            return true;
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        GUILayout.Button("My Button");
        GUILayout.Button("My Button2");
        GUILayout.Button("My Button3");
        GUILayout.Button("My Button4");

        Rect rectGUI;// = new Rect(100, 200, 100, 50);
        //GUI.Button(rectGUI, "MY Button 5");

        rectGUI = new Rect(100, 100, 150, 150);
        if(Selection.activeGameObject == null)
        {
            GUI.Label(rectGUI, "My Selection");
        }
        else
        {
            GUI.Label(rectGUI, Selection.activeGameObject.name);

            rectGUI = new Rect(100, 300, 150, 150);
            if(GUI.Button(rectGUI, "Add MyComponent"))
            {
                Selection.activeGameObject.AddComponent<MyComponent>();
            }
        }

        //GUI.Label(rectGUI, "Gameobjet Name");

        // 마우스 이벤트 체크
        if(Event.current.button == 1)
        {
            if(Event.current.type == EventType.mouseDown)
            {
                GenericMenu ContextMenu = new GenericMenu();
                ContextMenu.AddItem(new GUIContent("Menu 1"), true, Debug1);
                ContextMenu.AddItem(new GUIContent("Menu 2"), true, Debug2);
                ContextMenu.ShowAsContext();
            }
        }
    }

    void Debug1()
    {
        Debug.Log("Click Menu1");
    }

    void Debug2()
    {
        Debug.Log("Click Menu2");
    }
}
