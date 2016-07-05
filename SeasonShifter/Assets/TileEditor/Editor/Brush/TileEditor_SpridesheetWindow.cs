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
            }
        sizeMultiplier = EditorGUILayout.Slider(sizeMultiplier, 0.1f, 4);
        EditorGUILayout.EndHorizontal();
        spriteSheet =   EditorGUILayout.ObjectField(spriteSheet, typeof(Texture2D), GUILayout.Width(size.x), GUILayout.Height(size.y)) as Texture2D;
        size = new Vector2(spriteSheet.width, spriteSheet.height);
        size *= sizeMultiplier;

        if (GUI.changed)
        {
            //Debug.Log("Changed!");
            this.minSize = size + new Vector2(5, 80);
        }
    }

   


    
    void SliceSprite()
    {
        int numberOfVerticalSlices = spriteSheet.width / sliceWidth;
        int numberOfHorizontalSlices = spriteSheet.height / sliceHeight;

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
                    //Debug.Log(rect);
                    //Debug.Log("Set pixel X: " + x + " Y: " + y);
                    //Debug.Log("Rect: " + rect);
                    //Debug.Log("Rect MIn and max: " + rect.xMin + ", " + rect.yMin + ", " + rect.width + ", " + rect.height + ", ");
                    //Debug.Log("Set pixel Count X: " + countX + "Count Y: " + countY);
                    tempPixel[countX,countY] = spriteSheet.GetPixel(x, y);
                    countY++;
                }
                countY = 0;
                countX++;
            }
            pixelInformation.Add(tempPixel);
        }

        //Create new sprites with the pixelinformation
        foreach(Color[,] pixelBlock in pixelInformation)
        {
            Texture2D newTexture = new Texture2D(sliceWidth, sliceHeight);
            for(int x=0; x<sliceWidth;x++)
            {
                for (int y = 0; y < sliceHeight; y++)
                {
                    newTexture.SetPixel(x, y, pixelBlock[x, y]);
                }
            }


            Sprite newSprite = new Sprite();
            newSprite = Sprite.Create(newTexture, new Rect(0, 0, sliceWidth, sliceHeight), new Vector2(0.5f, 0.5f), newTexture.width);
            TileEditor_SpriteCollection.AddSprite(newSprite);
            
        }

    }
}
