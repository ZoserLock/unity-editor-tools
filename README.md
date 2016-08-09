# Unity Editor Tools
A set of Unity Custom Editor Layout tools.

## Dynamic Vertical Scroll View
Script that extend the normal **GUILayout ScrollView**  and allow you display a huge list without rendering all elements.

Tested with **500.000** elements.

#### How To use
```cs
    EditorTools.DynamicScroll(Editor editor, DynamicScrollData scrollData, List<T> data, int minHeight, Action<T, int> callback);
```
* editor: The current Editor.
* scrollData: The persistent data related to the scroll.
* List<T> data: The elements to show.
* minHeight: The height of the smallest element.
* callback: The function that draw GUILayout elements. Parameters: the element and the index.

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
