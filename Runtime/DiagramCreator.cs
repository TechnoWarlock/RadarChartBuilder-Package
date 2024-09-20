using UnityEditor;
using UnityEngine;

namespace TW.RadarChartBuilder
{
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer),typeof(MeshCreator))]
    [RequireComponent(typeof(AxisCreator),typeof(DiagramPNGSaver),typeof(DiagramDataCreator))]
    public class DiagramCreator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshCreator meshCreator;
        [SerializeField] private AxisCreator axisCreator;
        [SerializeField] private DiagramPNGSaver diagramPNGSaver;
        [SerializeField] private DiagramDataCreator diagramDataCreator;
        [SerializeField] private Transform startPoint;


        [SerializeField] [Range(3, 10)] private int statsCount;
        [SerializeField] [Range(2, 10)] private int ringsCount;
        [SerializeField] [Range(50, 1000)] private int radius;
        [SerializeField] [Range(0, 360)] private int angleOffset;
        [SerializeField] [Range(1, 100)] private float lineWidth;
        [SerializeField] private bool isRadialLinesActive;

        [SerializeField] private string diagramName = "RadarChart";
        [SerializeField] private string diagramFolder = "Sprites/RadarChart";
        [SerializeField] private string diagramStartPointName = "StartPoint";
        [SerializeField] private string diagramConfigurationName = "DiagramConfiguration";
        [SerializeField] private string diagramConfigurationFolder = "ScriptableObjects/RadarChart";

        private Vector3[] _verticesArray;
        private int[] _trianglesArray;
        private float _angle;

        private const int CameraSizeOffset = 200;
        
#if UNITY_EDITOR
        
        public void Create()
        {
            meshCreator.CreatePolygon(meshFilter, statsCount, angleOffset, new int[] { radius }, startPoint);
            meshCreator.ScaleStartPoint(startPoint, radius);
            axisCreator.CreateLines(radius, ringsCount, statsCount, angleOffset, isRadialLinesActive);
        }

        public void TriggerSaveDiagram()
        {
            startPoint.gameObject.SetActive(false);
            diagramPNGSaver.SaveDiagram(diagramFolder, diagramName);
            startPoint.gameObject.SetActive(true);
            diagramPNGSaver.SaveDiagram(diagramFolder, diagramStartPointName);
            diagramDataCreator.SaveConfiguration(diagramConfigurationFolder,diagramConfigurationName,radius, angleOffset, statsCount);
            AssetDatabase.Refresh();
        }

        public void DoCameraSize()
        {
            diagramPNGSaver.ChangeCameraSize(radius + CameraSizeOffset);
        }

        public void DoLineWidth()
        {
            axisCreator.ChangeLineWidth(lineWidth);
        }

#endif
    }
}