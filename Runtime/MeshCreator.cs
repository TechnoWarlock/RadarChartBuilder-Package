using UnityEngine;

namespace TW.RadarChartBuilder
{
    public class MeshCreator : MonoBehaviour
    {
        private Mesh _chartMesh;

        private float _angle;
        private Vector3[] _verticesArray;
        private int[] _trianglesArray;

        public void CreatePolygon(MeshFilter meshFilter, int statsCount, int angleOffset, int[] radius,
            Transform startPoint)
        {

            InitiateValues(statsCount);

            DoVertices(statsCount, angleOffset, radius, startPoint);

            DoTriangles(statsCount);

            DrawMesh(meshFilter);
        }

        public void CreatePolygon(CanvasRenderer canvasRenderer, Material material, int statsCount, int angleOffset,
            int[] radius)
        {
            InitiateValues(statsCount);

            DoVertices(statsCount, angleOffset, radius, null);

            DoTriangles(statsCount);

            DrawMesh(canvasRenderer, material);
        }

        private void DrawMesh(MeshFilter meshFilter)
        {
            if (meshFilter.sharedMesh)
            {
                meshFilter.sharedMesh.Clear();
                meshFilter.sharedMesh.vertices = _verticesArray;
                meshFilter.sharedMesh.triangles = _trianglesArray;
                meshFilter.sharedMesh.RecalculateBounds();
            }
            else
            {
                _chartMesh = new Mesh
                {
                    vertices = _verticesArray,
                    triangles = _trianglesArray
                };
                meshFilter.mesh = _chartMesh;
                meshFilter.sharedMesh.MarkDynamic();
            }


        }

        private void DrawMesh(CanvasRenderer canvasRenderer, Material material)
        {

            if (canvasRenderer.GetMesh())
            {
                _chartMesh.Clear();
                _chartMesh.vertices = _verticesArray;
                _chartMesh.triangles = _trianglesArray;
                _chartMesh.RecalculateBounds();
            }
            else
            {
                _chartMesh = new Mesh
                {
                    vertices = _verticesArray,
                    triangles = _trianglesArray
                };
                _chartMesh.MarkDynamic();
                canvasRenderer.SetMaterial(material, null);
            }

            canvasRenderer.SetMesh(_chartMesh);
        }

        private void DoTriangles(int statsName)
        {
            _trianglesArray[0] = 0;
            _trianglesArray[1] = 1;
            _trianglesArray[2] = 2;

            for (int i = 3; i < statsName * 3; i += 3)
            {
                _trianglesArray[i] = 0;
                _trianglesArray[i + 1] = _trianglesArray[i - 1];
                _trianglesArray[i + 2] = _trianglesArray[i + 1] % statsName + 1;
            }
        }

        private void DoVertices(int statsName, int angleOffset, int[] radius, Transform startPoint)
        {
            _verticesArray[0] = new Vector3(0, 0, 1);
            for (int i = 0; i < statsName; i++)
            {
                int angleIndex = statsName - i;
                float currentAngle = angleIndex * _angle + angleOffset * Mathf.Deg2Rad;

                _verticesArray[i + 1] = new Vector3(radius[i % radius.Length] * Mathf.Cos(currentAngle),
                    radius[i % radius.Length] * Mathf.Sin(currentAngle), 1);
            }

            if (startPoint)
            {
                startPoint.position = _verticesArray[1];
            }
        }

        private void InitiateValues(int statsName)
        {
            _angle = 2 * Mathf.PI / statsName;

            _verticesArray = new Vector3[statsName + 1];
            _trianglesArray = new int[statsName * 3];

        }

        public void ScaleStartPoint(Transform startPoint, int radius)
        {
            float scale = radius / 2f;
            startPoint.localScale = new Vector3(scale, scale, 1);
        }
    }
}