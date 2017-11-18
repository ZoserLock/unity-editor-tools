using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

public class EditorGUITools
{
    public class DynamicScrollData
    {
        public Vector2 scrollPosition;
        public Rect scrollRect;

        public int contentHeight = 0;
        public int lastContentHeight = 0;

        public int firstVisibleItem = -1;
        public int lastVisibleItem = -1;

        public int startSpace = 0;
        public int endSpace = 0;

        public bool dirty = false;

        public float userScrollPosition = -1.0f;

        public void SetScrollPosition(float normalizedPosition)
        {
            userScrollPosition = normalizedPosition;
        }
    }

    /// <summary>
    /// Utility function to estimate the Height of the objects.
    /// </summary>
    public static void TestHeight(System.Action drawFunction)
    {
        int size = 0;
        GUILayout.BeginVertical();
        drawFunction();
        if (Event.current.type == EventType.Repaint)
        {
            size = (int)GUILayoutUtility.GetLastRect().height;
        }

        GUILayout.EndVertical();

        if (Event.current.type == EventType.Repaint)
        {
            Debug.LogWarning("Last Rect Height "+((GUILayoutUtility.GetLastRect().height) / 2 +" Cell Size: "+ size));
        }
    }

    /// <summary>
    /// Dynamic Scroll Function: Renders the element list but just render the visible objects in the scroll. layout funciton shoul return the exact height of the item. Draw the actual draw code.
    /// </summary>
    public static void DynamicScroll<T>(Editor editor, DynamicScrollData data, List<T> elements, System.Func<T, int, int> layout, System.Action<T, int> drawFunction)
    {
        if (drawFunction == null || layout == null || elements==null || elements.Count == 0)
        {
            EditorGUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
            return;
        }

        EditorGUILayout.BeginVertical();

        data.scrollPosition = EditorGUILayout.BeginScrollView(data.scrollPosition);

        int visibleHeight = Mathf.CeilToInt(data.scrollRect.height);

        if (Event.current.type == EventType.Layout)
        {
            data.lastContentHeight = data.contentHeight;

            data.contentHeight = 0;
            data.firstVisibleItem = -1;
            data.lastVisibleItem = -1;
            data.startSpace = 0;
            data.endSpace = 0;

            int endOffset = 0;

            for (int a = 0; a < elements.Count; ++a)
            {
                data.contentHeight += layout(elements[a], a);
                if (data.firstVisibleItem == -1 && data.scrollPosition.y < data.contentHeight)
                {
                    data.firstVisibleItem = a;
                    data.startSpace = data.contentHeight - layout(elements[a], a);
                }

                if (data.firstVisibleItem !=-1 && data.lastVisibleItem == -1 && visibleHeight + data.scrollPosition.y < data.contentHeight)
                {
                    data.lastVisibleItem = a;
                    endOffset = data.contentHeight;
                }
            }

            if(data.lastVisibleItem == -1 && data.firstVisibleItem != -1)
            {
                data.lastVisibleItem = elements.Count - 1;
                endOffset = data.contentHeight;
            }

            if(data.lastContentHeight != data.contentHeight)
            {
                data.dirty = true;
            }

            data.endSpace = Mathf.Clamp(data.contentHeight - endOffset, 0, data.contentHeight);
        }

        if (data.lastVisibleItem ==-1 || data.firstVisibleItem ==-1)
        {
            EditorGUILayout.EndScrollView();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
            return;
        }

        GUILayout.BeginVertical();

        GUILayout.Space(data.startSpace);

        for (int a = data.firstVisibleItem; a < data.lastVisibleItem + 1 && a < elements.Count; ++a)
        {
            drawFunction(elements[a], a);
        }

        GUILayout.Space(data.endSpace);

        GUILayout.EndVertical();

        EditorGUILayout.EndScrollView();

        if (Event.current.type == EventType.Repaint)
        {
            data.scrollRect = GUILayoutUtility.GetLastRect();

            if(data.dirty)
            {
                editor.Repaint();
                data.dirty = false;
            }

            if (data.userScrollPosition >= 0.0f)
            {
                data.scrollPosition.y = Mathf.FloorToInt(Mathf.Clamp01(data.userScrollPosition) * (data.contentHeight- visibleHeight));
                data.userScrollPosition = -1.0f;
                editor.Repaint();
                data.dirty = true;
            }
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndVertical();
    }
}
