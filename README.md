# Unity Editor Tools
A set of Unity Custom Editor Layout tools.

## Dynamic Vertical Scroll View
Script that extend the normal **GUILayout ScrollView**  and allow you display a huge list without rendering all elements.

Tested with **500.000** elements.

#### Example code

```cs
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
