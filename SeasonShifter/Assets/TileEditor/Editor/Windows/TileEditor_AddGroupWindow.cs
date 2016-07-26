using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_AddGroupWindow : EditorWindow {

    private string name = "";

    public void Init()
    {
        this.minSize = new Vector2(170, 50);
        this.maxSize = new Vector2(190, 70);
    }

    void OnGUI()
    {
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Group Name:");
        EditorGUILayout.BeginHorizontal();
            name = EditorGUILayout.TextField(name);
            if (GUILayout.Button("Add"))
            {
                AddGroup();
            }
        EditorGUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
    }

    void AddGroup()
    {
        TileEditor_SpriteCollection.AddGroup(name);
        this.Close();
    }
}
