using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Injection))]
public class InjectionEditor : Editor
{
    private SerializedProperty injectionObjects;

    private ReorderableList reorderableList;


    public void OnEnable()
    {
        injectionObjects = serializedObject.FindProperty("injectionObjects");

        reorderableList = new ReorderableList(serializedObject, injectionObjects);

        reorderableList.drawHeaderCallback = OnDrawHeader;

        reorderableList.drawElementCallback = OnDrawElement;

        reorderableList.onAddCallback += OnAddElement;
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
        SerializedProperty target = element.FindPropertyRelative("m_target");
        SerializedProperty component = element.FindPropertyRelative("m_component");
        SerializedProperty name = element.FindPropertyRelative("m_name");

        OnDrawTarget(target, rect);

        OnDrawPopup(target, component, rect);

        OnDrawText(name, rect);

    }

    private void OnDrawHeader(Rect rect)
    { 
        EditorGUI.LabelField(rect, "Component Inject");
    }

    private void OnAddElement(ReorderableList list)
    {
        list.serializedProperty.arraySize++;
        list.index = list.serializedProperty.arraySize - 1;
        SerializedProperty item = list.serializedProperty.GetArrayElementAtIndex(list.index);
        item.FindPropertyRelative("m_target").objectReferenceValue = null;
        item.FindPropertyRelative("m_name").stringValue = string.Empty;
        item.FindPropertyRelative("m_component").objectReferenceValue = null;
    }

    private void OnDrawTarget(SerializedProperty target, Rect rect)
    {
        Rect objectRect = rect;
        objectRect.y += 2;
        objectRect.height = 20;
        objectRect.width *= 0.3f;
        target.objectReferenceValue = EditorGUI.ObjectField(objectRect, target.objectReferenceValue, typeof(Object), true);
    }

    private void OnDrawPopup(SerializedProperty target, SerializedProperty component, Rect rect)
    {
        Rect objectRect = rect;
        objectRect.y += 2;
        objectRect.height = 20;
        objectRect.width *= 0.3f;
        objectRect.x += rect.width * 0.3f + 5;
        string[] contents = new string[0] { };
        Component[] components = null;
        int popupIndex = 0;

        if (target.objectReferenceValue != null)
        {
            GameObject go = target.objectReferenceValue as GameObject;
            components = go.GetComponents<Component>();
            List<string> list = new List<string>();
            foreach (var item in components)
            {
                list.Add(item.GetType().Name);
            }

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == component.objectReferenceValue)
                {
                    popupIndex = i;
                    break;
                }
            }
            contents = list.ToArray();
        }
        popupIndex = EditorGUI.Popup(objectRect, popupIndex, contents);
        component.objectReferenceValue = components == null ? null : components[popupIndex];
    }

    private void OnDrawText(SerializedProperty name, Rect rect)
    {
        Rect objectRect = rect;
        objectRect.y += 2;
        objectRect.height = 20;
        objectRect.x += (rect.width * 0.3f + 5) * 2;
        objectRect.width = (rect.width - objectRect.x) + 10;
        name.stringValue = EditorGUI.TextField(objectRect, name.stringValue);
    }
}
