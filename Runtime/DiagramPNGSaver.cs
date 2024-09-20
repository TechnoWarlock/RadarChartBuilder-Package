using System.IO;
using UnityEditor;
using UnityEngine;

namespace TW.RadarChartBuilder
{
    public class DiagramPNGSaver : MonoBehaviour
    {
        [SerializeField] private Camera diagramCamera;

        private int _size;

#if UNITY_EDITOR

        public void ChangeCameraSize(int size)
        {
            diagramCamera.orthographicSize = size;
        }

        public void SaveDiagram(string folder, string fileName)
        {
            _size = (int)diagramCamera.orthographicSize * 2;
            diagramCamera.clearFlags = CameraClearFlags.SolidColor;
            diagramCamera.backgroundColor = new Color(0, 0, 0, 0);


            RenderTexture renderTexture = new RenderTexture(_size, _size, 24, RenderTextureFormat.ARGB32);
            renderTexture.useMipMap = false;
            renderTexture.filterMode = FilterMode.Bilinear;

            diagramCamera.targetTexture = renderTexture;


            Texture2D texture = new Texture2D(_size, _size, TextureFormat.RGBA32, false);
            diagramCamera.Render();

            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, _size, _size), 0, 0);
            texture.Apply();


            byte[] bytes = texture.EncodeToPNG();
            string filePath = Path.Combine(Application.dataPath, folder, $"{fileName}.png");
            string folderPath = Path.Combine(Application.dataPath, folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            File.WriteAllBytes(filePath, bytes);


            RenderTexture.active = null;
            diagramCamera.targetTexture = null;
            DestroyImmediate(renderTexture);
        }
        
#endif

    }

}