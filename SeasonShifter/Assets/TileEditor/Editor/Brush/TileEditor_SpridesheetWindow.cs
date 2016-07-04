using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_SpritesheetWindow : EditorWindow {

    public Texture2D spriteSheet;
    private Vector2 size;
    private float sizeMultiplier = 0.5f;

    public void Init()
    {
        this.size = new Vector2(300, 200);
        spriteSheet = new Texture2D(295, 195);
    }

    void OnGUI()
    {
        sizeMultiplier = EditorGUILayout.Slider(sizeMultiplier, 0.1f, 4);
        spriteSheet =   EditorGUILayout.ObjectField(spriteSheet, typeof(Texture2D), GUILayout.Width(size.x), GUILayout.Height(size.y)) as Texture2D;
        size = new Vector2(spriteSheet.width, spriteSheet.height);
        size *= sizeMultiplier;

        if (GUI.changed)
        {
            Debug.Log("Changed!");
            this.minSize = size + new Vector2(5, 80);
        }
        
    }
}
