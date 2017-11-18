# Unity Editor Tools
A set of Unity Custom Editor Layout tools.

## Dynamic Vertical Scroll View

The IMGUI system of unity works well when the amount of elements to render are small, but does not scale well with the amount of elements, making impossible to render more than 10k elements in an scroll view.

This script extend the normal **GUILayout ScrollView**  and allow you display a huge list just rendering the visible ones.

Tested with **500.000** elements.

#### How To use

```cs
EditorGUITools.DynamicScroll(Editor editor, DynamicScrollData data, List<T> elements, System.Func<T, int, int> layout, System.Action<T, int> drawFunction);
```

* editor: The current Editor.
* scrollData: The persistent data related to the scroll.
* elements: List<T> Of the elements to show.
* layout: System.Func<T, int, int> Function that should return the height of the element. Parameters: the element and the index. Return int representing the height in pixels.
* callback: Action<T, int> Function that draw GUILayout elements. Parameters: the element and the index.

#### Example code

```cs
        // Declared scrollData in the editor class definiton
        EditorGUITools.DynamicScrollData scrollData = new EditorGUITools.DynamicScrollData();

        // Inside the InspectorGUI function
        EditorGUITools.DynamicScroll(scrollData, _currentList,
        (element, index) =>
        {
            // The height in pixels of the current row. If some element have a different height here is the right place to change define it.
            return 28;
        },
        (element, index) =>
        {
            // The actual row that should height 28 pixels.
            GUILayout.BeginHorizontal("HelpBox");
            if (GUILayout.Button(element.text))
            {
                // Element Title
            }
            if(GUILayout.Button("X", GUILayout.ExpandWidth(false)))
            {
                // Remove Element
            }
            GUILayout.EndHorizontal();
        });
```
#### Screenshots
![Example](https://github.com/ZoserLock/unity-editor-tools/raw/master/Images/example.gif)
