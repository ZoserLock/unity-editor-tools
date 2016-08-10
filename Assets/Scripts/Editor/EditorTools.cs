using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

public class DynamicScrollData
{
    public Vector2  scrollSize;
    public Rect     scrollRect;
    public Rect     totalRect;

    public int      elementCount = 0;

    public float userScrollPosition = -1.0f;
}

public class EditorTools
{
	public static void DynamicScroll<T>(Editor editor,DynamicScrollData data,List<T> elements,int minH, System.Action<T, int> callback)
    {
        if (callback == null || elements.Count == 0)
        { 
            return;
        }

        data.scrollSize = EditorGUILayout.BeginScrollView(data.scrollSize, GUILayout.ExpandHeight(false));
        GUILayout.BeginVertical();
        int visibleHeight = Mathf.CeilToInt(data.scrollRect.height);

        int visibleItems = Mathf.CeilToInt(visibleHeight / (float)minH);

        int firstVisibleItem = Mathf.FloorToInt(data.scrollSize.y / (float)minH);
        int lastVisibleItem = Mathf.Clamp(Mathf.Min(elements.Count, firstVisibleItem + visibleItems), 0, elements.Count);

        firstVisibleItem = Mathf.Clamp(Mathf.Min(firstVisibleItem, lastVisibleItem - visibleItems), 0, elements.Count);

        int startSpace = firstVisibleItem * minH;
        int endSpace = (elements.Count - lastVisibleItem) * minH;
        
        GUILayout.Space(startSpace);
        for (int a = firstVisibleItem; a < lastVisibleItem; ++a)
        {
            callback(elements[a], a);
        }

        GUILayout.Space(endSpace);

        GUILayout.EndVertical();
        if (Event.current.type == EventType.Repaint)
        {
            data.totalRect = GUILayoutUtility.GetLastRect();
        }
        EditorGUILayout.EndScrollView();

        if (Event.current.type == EventType.Repaint)
        {
            data.scrollRect = GUILayoutUtility.GetLastRect();

            if (data.elementCount != elements.Count)
            {
                editor.Repaint();
                data.elementCount = elements.Count;
            }

            if (data.userScrollPosition >= 0.0f)
            {
                editor.Repaint();
                data.scrollSize.y = Mathf.FloorToInt(Mathf.Clamp01(data.userScrollPosition) * data.totalRect.height);
                data.userScrollPosition = -1.0f;
            }
        }
    }
}
