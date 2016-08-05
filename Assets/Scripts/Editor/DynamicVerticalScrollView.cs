using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class DynamicVerticalScrollView
{
    private Vector2 _scrollSize;
    private Rect _scrollRect;
    private Rect _elementRect;

    private int _visibleHeight;
    private int _elementSize;

    private int _visibleItems;
    private int _firstVisibleItem;
    private int _lastVisibleItem;

    private int _startSpace;
    private int _endSpace;

    private Editor _editor;

    public DynamicVerticalScrollView(Editor parentEditor)
    {
        _editor = parentEditor;
    }

    public void RenderList<T>(List<T> elements,System.Action<T,int> callback)
    {
        if(callback == null || elements.Count==0 || _editor == null)
        {
            return;
        }

        _scrollSize = EditorGUILayout.BeginScrollView(_scrollSize);

        if (Event.current.type == EventType.Layout)
        {
            _visibleHeight = Mathf.CeilToInt(_scrollRect.height);
            _elementSize   = Mathf.CeilToInt(_elementRect.height);

            if(_elementSize == 0 || _visibleHeight == 0)
            {
                _editor.Repaint();
                _elementSize = 20;
            }

            _visibleItems = Mathf.CeilToInt(_visibleHeight / (float)_elementSize);

            _firstVisibleItem = Mathf.FloorToInt(_scrollSize.y / (float)_elementSize);
            _lastVisibleItem = Mathf.Min(elements.Count, _firstVisibleItem + _visibleItems);

            _firstVisibleItem = Mathf.Min(_firstVisibleItem, _lastVisibleItem - _visibleItems);

            _startSpace = _firstVisibleItem * _elementSize;
            _endSpace = (elements.Count - _lastVisibleItem) * _elementSize;
        }

        GUILayout.Space(_startSpace);

        for (int a = _firstVisibleItem; a < _lastVisibleItem; ++a)
        {
            callback(elements[a],a);
        }

        if(_firstVisibleItem!= _lastVisibleItem)
        {
            if (Event.current.type == EventType.Repaint)
            {
                _elementRect = GUILayoutUtility.GetLastRect();
            }
        }

        GUILayout.Space(_endSpace);
        EditorGUILayout.EndScrollView();
        if (Event.current.type == EventType.Repaint)
        {
            _scrollRect = GUILayoutUtility.GetLastRect();
        }
    }
}
