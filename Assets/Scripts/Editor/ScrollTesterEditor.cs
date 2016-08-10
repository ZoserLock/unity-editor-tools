using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ScrollTester))]
public class ScrollTesterEditor : Editor
{
    Vector2 _scrollSize;

    List<TestElement> _elementListA = new List<TestElement>();
    List<TestElement> _elementListB = new List<TestElement>();

    TestElement _selected = null;

    bool _scrollSystemActivated = true;
    bool _showListA = true;

    DynamicScrollData _scrollData = new DynamicScrollData();

    void OnEnable()
    {
        _elementListA.Clear();
        for (int a = 0 ;a< 5000 ;++a)
        {
            _elementListA.Add(new TestElement("Element A: "+a));
        }

        _elementListB.Clear();
        for (int a = 0; a < 10; ++a)
        {
            _elementListB.Add(new TestElement("Element B: " + a));
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
        GUI.color = Color.blue;
        if (GUILayout.Button("Toggle List"))
        {
            _showListA = !_showListA;
        }
        GUI.color = old;

        old = GUI.color;
         
   
        if (GUILayout.Button("Add element"))
        {
            _elementListA.Add(new TestElement("Dynamic Element"));
            _selected = _elementListA[_elementListA.Count - 1];

            _scrollData.userScrollPosition = 2.0f; 

        }
        GUILayout.BeginVertical("HelpBox");
        if (_scrollSystemActivated)
        {
            List<TestElement> currentList = null;

            if(_showListA)
            {
                currentList = _elementListA;
            }
            else
            {
                currentList = _elementListB;
            }

            EditorTools.DynamicScroll(this,_scrollData, currentList, 20, (element, index) =>
            {
                if (_selected == element)
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

            for (int a = 0; a < _elementListA.Count; ++a)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Button(_elementListA[a].text);
                GUILayout.Button("X", GUILayout.ExpandWidth(false));
                GUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();
        }
        GUILayout.EndVertical();
        GUI.color = old;
    }
}
