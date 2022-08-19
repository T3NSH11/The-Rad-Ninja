using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveManager))] // set the script to override the manager's default inspector
public class SaveManagerEditor : Editor // a class for testing functionality in the savemanager.
{
    //SerializedProperty checkpoint; 
    Vector3 save1Checkpoint, save2Checkpoint, save3Checkpoint; // properties in inspector representing the values to write to the save files.
    float save1Health, save2Health, save3Health;


    //public override void OnInspectorGUI()
    //{
    //    SaveManager saveManager = (SaveManager)target; // get reference to the target script (script this is an editor for) by typecasting the target object

    //    serializedObject.Update(); // update the inspector UI


    //    // small explanation text box
    //    EditorGUILayout.HelpBox("This class is used to handle saving and loading from the 3 different slots. " +
    //        "To use it, simply set the values of the save file with its corresponding input fields below, and click 'Save Values to Slot'." +
    //        "You can also click 'Save Current State at Slot' to simply save without editing values." +
    //        "Feel free to use this to test different aspects of the game (see SaveManagerEditor for more info).", MessageType.Info);

    //    EditorGUILayout.Space(10f); // add some space to keep inspector neat

    //    // controls for saving
    //    save1Checkpoint = EditorGUILayout.Vector3Field("Save No.1 Checkpoint", save1Checkpoint);
    //    save1Health = EditorGUILayout.FloatField("Save No.1 Health", save1Health); // create an inspector field with the given type to take in user input
    //    EditorGUILayout.Space();

    //    if (GUILayout.Button("Save Values to Slot 1")) // executes save with user inputted values if button is clicked
    //        saveManager.SaveToSlot(saveManager.saveSlot1, save1Checkpoint, save1Health, SaveLoad.filePath1);

    //    if (GUILayout.Button("Save Current State at Slot 1"))
    //        saveManager.SaveToSlot1();

    //    if (GUILayout.Button("Load Slot 1"))
    //        saveManager.LoadSlot1();

        
    //    EditorGUILayout.Space(15f);



    //    save2Checkpoint = EditorGUILayout.Vector3Field("Save No.2 Checkpoint", save2Checkpoint);
    //    save2Health = EditorGUILayout.FloatField("Save No.2 Health", save2Health);
    //    EditorGUILayout.Space();

    //    if (GUILayout.Button("Save Values to Slot 2")) 
    //        saveManager.SaveToSlot(saveManager.saveSlot2, save2Checkpoint, save2Health, SaveLoad.filePath2);

    //    if (GUILayout.Button("Save Current State at Slot 2"))
    //        saveManager.SaveToSlot2();

    //    if (GUILayout.Button("Load Slot 2"))
    //        saveManager.LoadSlot2();


    //    EditorGUILayout.Space(15f);



    //    save3Checkpoint = EditorGUILayout.Vector3Field("Save No.3 Checkpoint", save3Checkpoint);
    //    save3Health = EditorGUILayout.FloatField("Save No.3 Health", save3Health);
    //    EditorGUILayout.Space();

    //    if (GUILayout.Button("Save Values to Slot 3"))
    //        saveManager.SaveToSlot(saveManager.saveSlot3, save3Checkpoint, save3Health, SaveLoad.filePath3);

    //    if (GUILayout.Button("Save Current State at Slot 3"))
    //        saveManager.SaveToSlot3();

    //    if (GUILayout.Button("Load Slot 3"))
    //        saveManager.LoadSlot3();


    //    EditorGUILayout.Space();

    //    serializedObject.ApplyModifiedProperties(); // apply the editor changes.

    //}

}
