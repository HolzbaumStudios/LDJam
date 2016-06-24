using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_Window : EditorWindow {

    TileEditor_Window window;
    Camera camera;
    RenderTexture renderTexture;
    private Rect sceneRect;



    public void Init()
    {
        EditorWindow editorWindow = GetWindow(typeof(TileEditor_Window));
        editorWindow.autoRepaintOnSceneChange = true;
        editorWindow.Show();
        if (camera == null) CreateCamera();

    }

    public void CreateCamera()
    {
        if(!GameObject.Find("EditorCamera"))
        {
            GameObject cameraObject = new GameObject("EditorCamera");
            cameraObject.transform.position = new Vector3(0, 0, -15);
            camera = cameraObject.AddComponent<Camera>();
            cameraObject.AddComponent<GUILayer>();
            camera.orthographic = true;
            camera.depth = -1;
            camera.orthographicSize = 15;

        }
        else
        {
            camera = GameObject.Find("EditorCamera").GetComponent<Camera>();
        }
    }

    public void Awake()
    {
        if(camera == null)CreateCamera();
        renderTexture = new RenderTexture((int)position.width,
                    (int)position.height,
                    (int)RenderTextureFormat.ARGB32);
                    
    }

    public void Update()
    {
        if(camera != null)
        {
            camera.targetTexture = renderTexture;
            camera.Render();
            camera.targetTexture = null;
        }
        if (renderTexture.width != position.width || renderTexture.height != position.height)
        {
            renderTexture = new RenderTexture((int)position.width,
                            (int)position.height,
                            (int)RenderTextureFormat.ARGB32);
        }

    }

    void OnGUI()
    {
        if (camera == null) CreateCamera();
        Rect cameraRect = new Rect(0, 0, 800, 600);

        GUI.DrawTexture(new Rect(0.0f, 0.0f, position.width, position.height), renderTexture);


        //Handles.DrawCamera(cameraRect, camera, DrawCameraMode.Textured);
    }



}
