﻿using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor
{
    // 유니티 캐쉬(cach)에서 값을 받아오는 역할이다.
    SerializedProperty intvariable;
    SerializedProperty floatvariable;
    SerializedProperty gameObjects;

    void OnEnable()
    {
        Debug.Log("Click");

        intvariable = serializedObject.FindProperty("intVariable");
        floatvariable = serializedObject.FindProperty("floatVariable");
        gameObjects = serializedObject.FindProperty("gameObjects");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(intvariable, new GUIContent("var1"));
        EditorGUILayout.PropertyField(floatvariable, new GUIContent("var2"));

        EditorGUILayout.PropertyField(gameObjects, new GUIContent("List"), true);

        // 자동 관리
        serializedObject.ApplyModifiedProperties();


        // 수동 관리
        MyComponent myComponent = (MyComponent)target;

        //myComponent.intVariable = EditorGUILayout.IntField("int variable", myComponent.intVariable);
        //myComponent.floatVariable = EditorGUILayout.Slider("float variable", myComponent.floatVariable, 0f, 100f);
        //myComponent.intvar = EditorGUILayout.IntField("int var", myComponent.intvar);
        int a = EditorGUILayout.IntField("int var", myComponent.intvar);
        
        if(a != myComponent.intvar)
        {
            myComponent.intvar = a;
            // 씬 변경시 변동 사항 저장 알림 여부
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        if(GUILayout.Button("DoSomething"))
        {
            myComponent.DoSomething();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        //if (GUILayout.Button("Show My Window"))
        //{
        //    MyEditorWindow.ShowWindow();
        //    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        //}
    }

}
