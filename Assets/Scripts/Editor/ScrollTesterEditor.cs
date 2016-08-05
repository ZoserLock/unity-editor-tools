using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ScrollTester))]
public class ScrollTesterEditor : Editor
{

    Vector2 _scrollSize;

    List<string> _buttonNames = new List<string>();

    bool _scrollSystemActivated = true;

    DynamicVerticalScrollView _dynamicVerticalScrollView;

    void OnEnable()
    {
        _dynamicVerticalScrollView = new DynamicVerticalScrollView(this);

        _buttonNames.Clear();
        for (int a = 0 ;a< 5000 ;++a)
        {
            _buttonNames.Add("Button: "+a);
        }
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Color old = GUI.color;

        if(_scrollSystemActivated)
        {
            GUI.color = Color.green;
            if (GUILayout.Button("Toggle Scroll System [ENABLED]"))
            {
                _scrollSystemActivated = !_scrollSystemActivated;
            }
        }
        else
        {
            GUI.color = Color.red;
            if (GUILayout.Button("Toggle Scroll System [DISABLED]"))
            {
                _scrollSystemActivated = !_scrollSystemActivated;
            }
        }

        GUI.color = old;

        if (_scrollSystemActivated)
        {
            _dynamicVerticalScrollView.RenderList(_buttonNames, (element, index) =>
            {
                GUILayout.BeginHorizontal();
                GUILayout.Button(element);
                GUILayout.Button("X",GUILayout.ExpandWidth(false));
                GUILayout.EndVertical();
            });
        }
        else
        {
            _scrollSize = EditorGUILayout.BeginScrollView(_scrollSize);

            for (int a = 0; a < _buttonNames.Count; ++a)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Button(_buttonNames[a]);
                GUILayout.Button("X", GUILayout.ExpandWidth(false));
                GUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
