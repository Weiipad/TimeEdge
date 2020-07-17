using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Bar))]
public class BarEditor : Editor
{
    SerializedObject barObject;

    SerializedProperty barType;
    SerializedProperty bloodBarColor;
    SerializedProperty shieldBarColor;
    SerializedProperty loadBulletBarColor;
    SerializedProperty barDirect;
    SerializedProperty entityTag;
    SerializedProperty mirror;
    private void OnEnable()
    {
        barObject = new SerializedObject(target);
        if (barObject != null)
        {
            barType = barObject.FindProperty("barType");
            bloodBarColor = barObject.FindProperty("BloodBarColor");
            shieldBarColor = barObject.FindProperty("ShieldBarColor");
            loadBulletBarColor = barObject.FindProperty("LoadBulletColor");

            barDirect = barObject.FindProperty("barDirect");
            entityTag = barObject.FindProperty("EntityTag");
            mirror = barObject.FindProperty("Mirror");
        }
    }

    public override void OnInspectorGUI()
    {
        if (barObject != null)
        {
            barObject.Update();
            if (barType != null)
            {
                EditorGUILayout.PropertyField(barType);
                BarTypeOnInspectorGUI(barType.enumValueIndex);
                EditorGUILayout.PropertyField(barDirect);
                EditorGUILayout.PropertyField(entityTag);
                EditorGUILayout.PropertyField(mirror);
            }
            barObject.ApplyModifiedProperties();
        }
    }

    private void BarTypeOnInspectorGUI(int index)
    {
        if (index == 0)
        {
            return;
        }
        else if (index == 1)
        {
            EditorGUILayout.PropertyField(bloodBarColor);
        }
        else if(index == 2)
        {
            EditorGUILayout.PropertyField(shieldBarColor);
        }
        else if(index == 3)
        {
            EditorGUILayout.PropertyField(loadBulletBarColor);
        }

    }
}
