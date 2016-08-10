using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_TextureTool : EditorWindow {

    private GameObject levelContainer;
    public bool textureToolEnabled { get; set; }

    private enum TextureToolMode { Raise_Lower, Paint}
    private TextureToolMode textureMode = TextureToolMode.Raise_Lower;

    public Color defaultButtonBackground = new Color32(255, 255, 255, 255);
    public Color selectedButtonBackground = new Color32(100, 200, 130, 255);

    private int sortingLayerIndex = 0;
    private int raiseLowerIndex = 0;
    bool raise = false;

    private Color paintColor = Color.black;



    [MenuItem("Tools/TileEditor/TextureTool")]
    private static void ShowWindow()
    {
        TileEditor_TextureTool window = (TileEditor_TextureTool)EditorWindow.GetWindow(typeof(TileEditor_TextureTool));
        window.Init();
    }

    void Init()
    {
        this.minSize = new Vector2(150, 150);
    }

    public void OnEnable()
    {
        SceneView.onSceneGUIDelegate += TextureUpdate;
    }

    void OnGUI()
    {
        if(!textureToolEnabled)
        {
            if (GUILayout.Button("Start TextureTool"))
                textureToolEnabled = true;

        }
        else
        {
            if (GUILayout.Button("Close TextureTool"))
                textureToolEnabled = false;

            EditorGUILayout.Space();

            levelContainer = EditorGUILayout.ObjectField(levelContainer, typeof(GameObject)) as GameObject;

            if (levelContainer != null)
            {

                //Choose the sorting layer
                GUILayout.Label("Layer");
                var sortingLayers = SortingLayer.layers;
                string[] sortingLayerNames = new string[sortingLayers.Length];
                for (int i = 0; i < sortingLayers.Length; i++)
                {
                    sortingLayerNames[i] = sortingLayers[i].name;
                }
                sortingLayerIndex = EditorGUILayout.Popup(sortingLayerIndex, sortingLayerNames);

                EditorGUILayout.BeginHorizontal();
                if (textureMode == TextureToolMode.Raise_Lower) { GUI.backgroundColor = selectedButtonBackground; } else { GUI.backgroundColor = defaultButtonBackground; }
                if (GUILayout.Button("Raise/Lower"))
                    textureMode = TextureToolMode.Raise_Lower;
                if (textureMode == TextureToolMode.Paint) { GUI.backgroundColor = selectedButtonBackground; } else { GUI.backgroundColor = defaultButtonBackground; }
                if (GUILayout.Button("Paint"))
                    textureMode = TextureToolMode.Paint;
                EditorGUILayout.EndHorizontal();
                GUI.backgroundColor = defaultButtonBackground;
                EditorGUILayout.Space();
                //Tool specific options
                if (textureMode == TextureToolMode.Raise_Lower)
                {
                    EditorGUILayout.LabelField("Raise field?");
                    raise = EditorGUILayout.Toggle(raise);

                    //number of affected fields
                    EditorGUILayout.LabelField("Affected pixels");
                    string[] affectedPixels = new string[] { "1", "3", "5", "7" };
                    raiseLowerIndex = EditorGUILayout.Popup(raiseLowerIndex, affectedPixels);
                }
                else if (textureMode == TextureToolMode.Paint)
                {
                    paintColor = EditorGUILayout.ColorField(paintColor);
                }
            }  
        }
    }

    //Check for mouse gestures
    public void TextureUpdate(SceneView sceneview)
    {
        if (textureToolEnabled)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            Event e = Event.current;

            Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight, 1000));
            Vector3 mousePos = r.origin;

            if(e.isMouse && e.button == 0 &&  (e.type == EventType.MouseDown || (e.type == EventType.MouseDrag && textureMode == TextureToolMode.Paint)) && levelContainer != null)
            {
                //Get Texture
                TileEditor_ObjectHandler objectHandler = levelContainer.GetComponent<TileEditor_ObjectHandler>();
                var spriteArray = objectHandler.GetSpriteArray();
                SpriteRenderer spriteRenderer = spriteArray[(int)mousePos.x, (int)mousePos.y, sortingLayerIndex].GetComponent<SpriteRenderer>();
                Texture2D texture = spriteRenderer.sprite.texture;
                //Get Coordinates
                Vector2 difference = new Vector2(mousePos.x - (int)mousePos.x, mousePos.y - (int)mousePos.y);
                Vector2 pixelCoordinate = new Vector2((difference.x * texture.width), difference.y * texture.height);
                //Set the texture
                Texture2D newTexture = new Texture2D(texture.width, texture.height);
                newTexture.wrapMode = TextureWrapMode.Clamp;
                newTexture.SetPixels32(texture.GetPixels32()) ;
                switch(textureMode)
                {
                    case TextureToolMode.Paint: Paint(ref newTexture, pixelCoordinate, paintColor);
                        break;
                    case TextureToolMode.Raise_Lower: LevelTile(ref newTexture, pixelCoordinate);
                        break;
                }
                
                newTexture.Apply();
                Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), newTexture.width);
                spriteRenderer.sprite = newSprite;
            }
            else if( levelContainer == null)
            {
                Debug.Log("You have to assign a level container first");
            }
        }
    }

    //Paint the pixel on the texture
    public void Paint(ref Texture2D texture, Vector2 coordinates, Color color)
    {
        texture.SetPixel((int)coordinates.x, (int)coordinates.y, color);
    }

    //Raise or lower pixels
    public void LevelTile(ref Texture2D texture, Vector2 coordinates)
    {
        int coordinateX = (int)coordinates.x;
        int coordinateY = (int)coordinates.y;
        Color currentColor = texture.GetPixel(coordinateX, coordinateY);

        int numberOfPixels = 0;
        switch(raiseLowerIndex)
        {
            case 0: numberOfPixels = 1;
                break;
            case 1: numberOfPixels = 3;
                break;
            case 2: numberOfPixels = 5;
                break;
            case 3: numberOfPixels = 7;
                break;
            default: numberOfPixels = 1;
                break;
        }

        int tempDifference = 0;//to know where the loop begins
        if (numberOfPixels > 1) tempDifference = numberOfPixels - 2;
        Color newColor;
        if(!raise)
            newColor = new Color(currentColor.r - 0.05f, currentColor.g - 0.05f, currentColor.b - 0.05f);
        else
            newColor = new Color(currentColor.r + 0.04f, currentColor.g + 0.04f, currentColor.b + 0.04f);

        //Change middle fields
        for (int y = coordinateY - tempDifference; y < coordinateY + tempDifference; y++)
        {
            for (int x = coordinateX - tempDifference; x < coordinateX + tempDifference; x++)
            {
                Paint(ref texture, new Vector2(x, y), newColor);
            }
        }

        //Change top
        for (int i=coordinateX-tempDifference; i<coordinateX+tempDifference; i++)
        {
            int y = coordinateY + tempDifference;
            currentColor = texture.GetPixel(i, y);
            if (!raise)
                newColor = new Color(currentColor.r - 0.17f, currentColor.g - 0.17f, currentColor.b - 0.17f);
            else
                newColor = newColor = new Color(currentColor.r + 0.17f, currentColor.g + 0.17f, currentColor.b + 0.17f);
            Paint(ref texture, new Vector2(i, y), newColor);
        }
        //Change left
        for (int i = coordinateY - tempDifference + 1; i < coordinateY + tempDifference; i++)
        {
            int x = coordinateX - tempDifference;
            currentColor = texture.GetPixel(x, i);
            if (!raise)
                newColor = new Color(currentColor.r - 0.13f, currentColor.g - 0.13f, currentColor.b - 0.13f);
            else
                newColor = new Color(currentColor.r + 0.13f, currentColor.g + 0.13f, currentColor.b + 0.13f);
            Paint(ref texture, new Vector2(coordinateX - tempDifference, i), newColor);
        }
        //Change right 
        for (int i = coordinateY - tempDifference; i < coordinateY + tempDifference; i++)
        {
            int x = coordinateX + tempDifference;
            currentColor = texture.GetPixel(x, i);
            if (!raise)
                newColor = new Color(currentColor.r + 0.17f, currentColor.g + 0.17f, currentColor.b + 0.17f);
            else
                newColor = new Color(currentColor.r - 0.17f, currentColor.g - 0.17f, currentColor.b - 0.17f);
            Paint(ref texture, new Vector2(coordinateX + tempDifference, i), newColor);
        }
        //Change bottom
        for (int i = coordinateX - tempDifference + 1; i < coordinateX + tempDifference; i++)
        {
            int y = coordinateY - tempDifference;
            currentColor = texture.GetPixel(i, y);
            if (!raise)
                newColor = new Color(currentColor.r + 0.13f, currentColor.g + 0.13f, currentColor.b + 0.13f);
            else
                newColor = new Color(currentColor.r - 0.13f, currentColor.g - 0.13f, currentColor.b - 0.13f);
            Paint(ref texture, new Vector2(i, coordinateY - tempDifference), newColor);
        }
    }
}
