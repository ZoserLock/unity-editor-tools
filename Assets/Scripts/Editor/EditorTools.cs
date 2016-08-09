using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

public class DynamicScrollData
{
    public Vector2 scrollSize;
    public Rect scrollRect;

    public int visibleHeight;

    public int firstVisibleItem;
    public int lastVisibleItem;

    public int startSpace;
    public int endSpace;

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

        if (Event.current.type == EventType.Layout)
        {
            data.visibleHeight = Mathf.CeilToInt(data.scrollRect.height);

            if (data.visibleHeight == 0)
            {
                editor.Repaint();
            }

            int visibleItems = Mathf.CeilToInt(data.visibleHeight / (float)minH);

            data.firstVisibleItem = Mathf.FloorToInt(data.scrollSize.y / (float)minH);
            data.lastVisibleItem = Mathf.Clamp(Mathf.Min(elements.Count, data.firstVisibleItem + visibleItems), 0, elements.Count);
            data.firstVisibleItem = Mathf.Clamp(Mathf.Min(data.firstVisibleItem, data.lastVisibleItem - visibleItems), 0, elements.Count);

            data.startSpace = data.firstVisibleItem * minH;
            data.endSpace = (elements.Count - data.lastVisibleItem) * minH;
        }

        GUILayout.Space(data.startSpace);
        for (int a = data.firstVisibleItem; a < data.lastVisibleItem; ++a)
        {
            callback(elements[a], a);
        }

        GUILayout.Space(data.endSpace);
        EditorGUILayout.EndScrollView();
        if (Event.current.type == EventType.Repaint)
        {
            data.scrollRect = GUILayoutUtility.GetLastRect();
        }
    }
}
