using UnityEngine;
using UnityEditor;
 
public class TileEditor_Menu
{
    [MenuItem("Window/TileEditor")]
    private static void ShowTileEditor()
    {
        TileEditor_Window window = (TileEditor_Window)EditorWindow.GetWindow(typeof(TileEditor_Window));
        window.Init();
    }
}
