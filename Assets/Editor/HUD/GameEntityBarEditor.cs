using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEntityBar))]
public class GameEntityBarEditor : Editor
{
    SerializedObject barObject;

    SerializedProperty barType;
    SerializedProperty entityFrom;
    SerializedProperty entityTag;
    SerializedProperty targetEntityObject;

    SerializedProperty bloodBarColor;
    SerializedProperty shieldBarColor;
    SerializedProperty loadBulletBarColor;

    
    private void OnEnable()
    {
        barObject = new SerializedObject(target);
        if (barObject != null)
        {
            barType = barObject.FindProperty("barType");
            entityFrom = barObject.FindProperty("entityFrom");
            entityTag = barObject.FindProperty("EntityTag");
            targetEntityObject = barObject.FindProperty("targetEntityObject");

            bloodBarColor = barObject.FindProperty("BloodBarColor");
            shieldBarColor = barObject.FindProperty("ShieldBarColor");
            loadBulletBarColor = barObject.FindProperty("LoadBulletColor");
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

                EditorGUILayout.PropertyField(entityFrom);
                if (entityFrom.enumValueIndex == 0)
                {
                    EditorGUILayout.PropertyField(entityTag);
                }
                else if (entityFrom.enumValueIndex == 1)
                {
                    EditorGUILayout.PropertyField(targetEntityObject);
                }
            }
            barObject.ApplyModifiedProperties();
        }
    }

    private void BarTypeOnInspectorGUI(int index)
    {
        if (index == 0)
        {
            EditorGUILayout.PropertyField(bloodBarColor);
        }
        else if(index == 1)
        {
            EditorGUILayout.PropertyField(shieldBarColor);
        }
        else if(index == 2)
        {
            EditorGUILayout.PropertyField(loadBulletBarColor);
        }
    }
}
