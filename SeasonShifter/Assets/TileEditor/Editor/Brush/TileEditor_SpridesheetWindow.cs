using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class TileEditor_SpritesheetWindow : EditorWindow {

    public Texture2D spriteSheet;
    private Vector2 size;
    private float sizeMultiplier = 0.5f;
    private int sliceHeight;
    private int sliceWidth;
    private List<SpriteMetaData> spriteList = new List<SpriteMetaData>();

    private int numberOfVerticalSlices = 0;
    private int numberOfHorizontalSlices = 0;
    private Rect imageRect = new Rect(0, 0, 0, 0);


    public void Init()
    {
        this.size = new Vector2(300, 200);
        spriteSheet = new Texture2D(295, 195);
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Width:");
            sliceWidth = EditorGUILayout.IntField(sliceWidth);
            GUILayout.Label("Height:");
            sliceHeight = EditorGUILayout.IntField(sliceHeight);
            if(GUILayout.Button("Slice"))
            {
                SliceSprite();
                //TileEditor_SpriteCollection.ImportSpritesheet(AssetDatabase.GetAssetPath(spriteSheet));
            }
        sizeMultiplier = EditorGUILayout.Slider(sizeMultiplier, 0.1f, 4);
        EditorGUILayout.EndHorizontal();
        spriteSheet =   EditorGUILayout.ObjectField(spriteSheet, typeof(Texture2D), GUILayout.Width(size.x), GUILayout.Height(size.y)) as Texture2D;
        imageRect = GUILayoutUtility.GetLastRect();
        size = new Vector2(spriteSheet.width, spriteSheet.height);
        size *= sizeMultiplier;

        if (GUI.changed)
        {
            //Debug.Log("Changed!");
            this.minSize = size + new Vector2(5, 80);

            if(sliceWidth!=0)numberOfVerticalSlices = spriteSheet.width / sliceWidth; else { numberOfVerticalSlices = 0; }
            if (sliceHeight != 0) numberOfHorizontalSlices = spriteSheet.height / sliceHeight; else { numberOfHorizontalSlices = 0; }
        }


        Handles.BeginGUI();
        for(int i=0;i<=numberOfHorizontalSlices;i++)
        {
            float lineHeight = imageRect.yMin + (sliceHeight * sizeMultiplier) * i;
            Handles.DrawLine(new Vector3(5, lineHeight), new Vector3(size.x, lineHeight));
        }
        for (int i = 0; i <= numberOfVerticalSlices; i++)
        {
            float lineWidth = imageRect.xMin + (sliceWidth * sizeMultiplier) * i;
            Handles.DrawLine(new Vector3(lineWidth, 21), new Vector3(lineWidth, size.y));
        }

        Handles.EndGUI();
    }

   


    
    void SliceSprite()
    {
        
        List<Rect> spriteRects = new List<Rect>();

        //Store the rect information of the slices
        //Debug.Log("Texturesize: " + spriteSheet.width + " / " + spriteSheet.height);
        int countX = 0, countY = 0;
        for (int i=0;i<spriteSheet.width -sliceWidth; i += sliceWidth)
        {
            for (int j = 0; j < spriteSheet.height -sliceHeight; j += sliceHeight)
            {
                spriteRects.Add(new Rect(countX*sliceWidth,countY*sliceHeight,countX*sliceWidth+sliceWidth, countY*sliceHeight+sliceHeight));
                countY++;
            }
            countY = 0;
            countX++;
        }

       /*foreach(Rect rect in spriteRects)
        {
            Debug.Log(rect);
        }*/

        List<Color[,]> pixelInformation = new List<Color[,]>();

        //Get the pixelinformation of every rect
        foreach(Rect rect in spriteRects)
        {
            Color[,] tempPixel = new Color[sliceWidth,sliceHeight];
            countX = 0; countY = 0;
            for(int x = (int)rect.xMin; x < rect.width; x++)
            {
                for (int y = (int)rect.yMin; y < rect.height; y++)
                {
                    tempPixel[countX,countY] = spriteSheet.GetPixel(x, y);
                    countY++;
                }
                countY = 0;
                countX++;
            }
            pixelInformation.Add(tempPixel);
        }

        //Create new sprites with the pixelinformation
        Sprite newSprite = new Sprite(); 
        foreach (Color[,] pixelBlock in pixelInformation)
        {
            Texture2D newTexture = new Texture2D(sliceWidth, sliceHeight, TextureFormat.ARGB32, false);
            for (int x=0; x<sliceWidth;x++)
            {
                for (int y = 0; y < sliceHeight; y++)
                {
                    newTexture.SetPixel(x, y, pixelBlock[x, y]);
                }
            }

            newTexture.Apply();

            newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
            TileEditor_SpriteCollection.spriteGroupCollection[TileEditor_SpriteCollection.activeGroupIndex].AddSprite(newSprite);
            
        }
        //Save the new sprites
        TileEditor_SpriteCollection.Save();

    }
}
