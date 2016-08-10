# Unity Editor Tools
A set of Unity Custom Editor Layout tools.

## Dynamic Vertical Scroll View
Script that extend the normal **GUILayout ScrollView**  and allow you display a huge list without rendering all elements.

Tested with **500.000** elements.

#### How To use
```cs
EditorTools.DynamicScroll(Editor, ScrollData, Elements, MinHeight, Callback);
```
* Editor: The current Editor.
* ScrollData: The persistent data related to the scroll.
* Elements: List<T> Of the elements to show.
* MinHeight: The height of the smallest element.
* Callback: Action<T, int> Function that draw GUILayout elements. Parameters: the element and the index.

#### Example code

```cs
       // Declared scrollData in the editor class definiton
       DynamicScrollData scrollData = new DynamicScrollData;

       // OnInspectorGUI()
       EditorTools.DynamicScroll(this,scrollData, _objectList,minObjectHeight, (element, index) =>
       {
            // Your GUILayout commands goes here.
            // Example:
            GUILayout.BeginHorizontal();
            GUILayout.Button(element);
            GUILayout.EndVertical();
        });
```
#### Know Issues
* The first time the scroll is rendered may flicker.
