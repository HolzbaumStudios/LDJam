using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TileEditor_DisplayBrushCollection : MonoBehaviour {

	////
    ///  THIS SCRIPT MAY NOT BE DELETED. IT IS ACCESSED BY THE BRUSH COLLECTION EDITOR TO DISPLAY THE STATIC SCRIPT BRUSHCOLLECTION
    ///
    ////

    //Load brush collection
    void Start()
    {
        Debug.Log("Load brushes");
        TileEditor_BrushCollection.LoadBrushCollection();
    }
}
