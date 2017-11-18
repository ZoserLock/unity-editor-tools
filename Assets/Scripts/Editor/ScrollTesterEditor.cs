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
    List<TestElement> _currentList  = new List<TestElement>();

    TestElement _selected = null;
    TestElement _removeNext = null;

    bool _scrollSystemActivated = true;
    bool _showListA = true;

    EditorGUITools.DynamicScrollData _scrollData = new EditorGUITools.DynamicScrollData();

    void OnEnable()
    {
        _elementListA.Clear();
        for (int a = 0 ;a < 500000 ;++a)
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

        if (Event.current.type == EventType.Layout)
        {
            if (_showListA)
            {
                _currentList = _elementListA;
            }
            else
            {
                _currentList = _elementListB;
            }

            if (_removeNext != null)
            {
                _currentList.Remove(_removeNext);
                _removeNext = null;
            }
        }

        Color old = GUI.color;

        if (GUILayout.Button("Set Center"))
        {
            _scrollData.userScrollPosition = 0.5f;
        }

        if (GUILayout.Button("Set bellow"))
        {
            _scrollData.userScrollPosition = 1.0f;
        }

        if (GUILayout.Button("Set 0"))
        {
            _scrollData.userScrollPosition = 0;
        }

        if (_scrollSystemActivated)
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
        GUI.color = Color.grey;
        if (GUILayout.Button("Toggle List"))
        {
            _showListA = !_showListA;
        }
        GUI.color = old;

        old = GUI.color;
         
   
        if (GUILayout.Button("Add element"))
        {
            _currentList.Add(new TestElement("Dynamic Element"));
            _selected = _currentList[_currentList.Count - 1];
            _scrollData.userScrollPosition = 1.0f; 
        }

        GUILayout.BeginVertical("HelpBox");
        if (_scrollSystemActivated)
        {
            EditorGUITools.DynamicScroll(this,_scrollData, _currentList,
            (element, index) =>
            {
                if (_selected == element)
                {
                    return 128;
                }

                return 28;
            },
            (element, index) =>
            {
                if (_selected == element)
                {
                    GUILayout.BeginVertical("HelpBox");
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(element.text))
                    {
                        _selected = null;
                    }
                    if (GUILayout.Button("X", GUILayout.ExpandWidth(false)))
                    {
                        _removeNext = element;
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(100);
                    GUILayout.EndVertical();
             
                }
                else
                {
                    GUILayout.BeginHorizontal("HelpBox");
                    if (GUILayout.Button(element.text))
                    {
                        _selected = element;
                    }
                    if(GUILayout.Button("X", GUILayout.ExpandWidth(false)))
                    {
                        _removeNext = element;
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

        if (GUILayout.Button("Set Center"))
        {
            _scrollData.userScrollPosition = 0.5f;
        }

        if (GUILayout.Button("Set bellow"))
        {
            _scrollData.userScrollPosition = 1.0f;
        }

        if (GUILayout.Button("Set 0"))
        {
            _scrollData.userScrollPosition = 0;
        }
        GUI.color = old;
    }
}
