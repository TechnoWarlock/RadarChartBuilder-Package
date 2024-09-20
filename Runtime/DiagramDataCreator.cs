using System.IO;
using UnityEditor;
using UnityEngine;

namespace TW.RadarChartBuilder
{
    public class DiagramDataCreator : MonoBehaviour
    {
#if UNITY_EDITOR

        public void SaveConfiguration(string diagramConfigurationFolder, string diagramConfigurationName, int radius,
            int angleOffset, int statsCount)
        {

            string filePath = Path.Combine("Assets",diagramConfigurationFolder, $"{diagramConfigurationName}.asset");
            string folderPath = Path.Combine(Application.dataPath, diagramConfigurationFolder);
            
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            DiagramConfigurationSO newDiagramConfigurationSo = AssetDatabase.LoadAssetAtPath<DiagramConfigurationSO>(filePath);
            
            if (!newDiagramConfigurationSo)
            {
                newDiagramConfigurationSo =
                    ScriptableObject.CreateInstance<DiagramConfigurationSO>();
                AssetDatabase.CreateAsset(newDiagramConfigurationSo, filePath);
                
            }
            
            newDiagramConfigurationSo.Radius = radius;
            newDiagramConfigurationSo.AngleOffset = angleOffset;
            newDiagramConfigurationSo.StatsCount = statsCount;
            

            EditorUtility.SetDirty(newDiagramConfigurationSo);
        }

#endif
        
    }
}