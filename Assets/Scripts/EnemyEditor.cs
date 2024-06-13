using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    SerializedProperty patternsProperty;
    private bool[] foldouts;

    private void OnEnable()
    {
        patternsProperty = serializedObject.FindProperty("patterns");
        foldouts = new bool[patternsProperty.arraySize];
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(patternsProperty, new GUIContent("Patterns"), true);

        if (patternsProperty.isExpanded)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < patternsProperty.arraySize; i++)
            {
                SerializedProperty patternProperty = patternsProperty.GetArrayElementAtIndex(i);
                foldouts[i] = EditorGUILayout.Foldout(foldouts[i], patternProperty.objectReferenceValue != null ? patternProperty.objectReferenceValue.name : "Null");

                if (foldouts[i])
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(patternProperty);

                    if (patternProperty.objectReferenceValue != null)
                    {
                        Editor patternEditor = CreateEditor(patternProperty.objectReferenceValue);
                        patternEditor.OnInspectorGUI();
                    }

                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
