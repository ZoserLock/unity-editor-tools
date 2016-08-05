# Unity Editor Tools
A set of Unity Custom Editor Layout tools.

## Dynamic Vertical Scroll View
Script that extend the normal **GUILayout ScrollView**  and allow you display a huge list without rendering all elements.

#### Example code

```cs
        scrollObject.RenderList(exampleList, (element, index) =>
        {
            // Your GUILayout commands goes here.
            // Example:
            GUILayout.BeginHorizontal();
            GUILayout.Button(element);
            GUILayout.EndVertical();
        });
```
#### Know Issues
* Elements of variable height are unsupported.
* The first time the scroll is rendered may flicker.
