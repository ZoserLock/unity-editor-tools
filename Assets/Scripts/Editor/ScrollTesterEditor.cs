using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ScrollTester))]
public class ScrollTesterEditor : Editor
{
    Vector2 _scrollSize;

    List<TestElement> _buttonNames = new List<TestElement>();

    TestElement _selected = null;

    bool _scrollSystemActivated = true;

    DynamicScrollData _scrollData = new DynamicScrollData();

    void OnEnable()
    {
        _buttonNames.Clear();
        for (int a = 0 ;a< 10000 ;++a)
        {
            _buttonNames.Add(new TestElement("Element: "+a));
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

        old = GUI.color;
         
   
        if (GUILayout.Button("Add element"))
        {
            _buttonNames.Add(new TestElement("Dynamic Element"));
        }
        GUILayout.BeginVertical("HelpBox");
        if (_scrollSystemActivated)
        {
            EditorTools.DynamicScroll(this,_scrollData, _buttonNames,20, (element, index) =>
            {
                if(_selected == element)
                {
                    GUILayout.BeginVertical("HelpBox");
                    GUILayout.BeginHorizontal();
                    GUILayout.Button(element.text);
                    if (GUILayout.Button("X", GUILayout.ExpandWidth(false)))
                    {
                        _selected = null;
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(100);
                    GUILayout.EndVertical();
                }else
                {
                    GUILayout.BeginHorizontal("HelpBox");
                    GUILayout.Button(element.text);
                    if(GUILayout.Button("X", GUILayout.ExpandWidth(false)))
                    {
                        _selected = element;
                    }
                    GUILayout.EndHorizontal();
                }
           
            });
        }
        else
        {
            _scrollSize = EditorGUILayout.BeginScrollView(_scrollSize,GUILayout.ExpandHeight(false));

            for (int a = 0; a < _buttonNames.Count; ++a)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Button(_buttonNames[a].text);
                GUILayout.Button("X", GUILayout.ExpandWidth(false));
                GUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();
        }
        GUILayout.EndVertical();
        GUI.color = old;
    }
}
