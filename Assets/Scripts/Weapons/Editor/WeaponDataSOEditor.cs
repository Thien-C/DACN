using Hung.Weapons;
using Hung.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataSOEditor : Editor
{
    private static List<Type> dataComTypes = new List<Type>();

    private WeaponDataSO dataSO;
    private bool showForeceUpdateButtons;
    private bool showAddComponentButtons;
    private void OnEnable()
    {
        dataSO = target as WeaponDataSO;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Set Number Attacks"))
        {
            foreach (var item in dataSO.ComponentData)
            {
                item.InitializeAttackData(dataSO.NumberOfAttacks);
            }
        }

        showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Component");

        if (showAddComponentButtons)
        {
            foreach (var dataComType in dataComTypes)
            {
                if (GUILayout.Button(dataComType.Name))
                {
                    var comp = Activator.CreateInstance(dataComType) as ComponentData;

                    if (comp == null)
                    {
                        return;
                    }

                    comp.InitializeAttackData(dataSO.NumberOfAttacks);

                    dataSO.AddData(comp);

                    EditorUtility.SetDirty(dataSO);
                }
            }
        }

        showForeceUpdateButtons = EditorGUILayout.Foldout(showForeceUpdateButtons, "Force Update Buttons");

        if (showForeceUpdateButtons)
        {
            if (GUILayout.RepeatButton("Force Update Component Names"))
            {
                foreach (var item in dataSO.ComponentData)
                {
                    item.SetComponentName();
                }
            }
            if (GUILayout.RepeatButton("Force Update Attack Names"))
            {
                foreach (var item in dataSO.ComponentData)
                {
                    item.SetAttackDataName();
                }
            }
        }
    }

    [DidReloadScripts]
    private static void OnReCompile()
    {
        var asssemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = asssemblies.SelectMany(assembly => assembly.GetTypes());
        var filteredTypes = types.Where(
            type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );
        dataComTypes = filteredTypes.ToList();
    }
}
