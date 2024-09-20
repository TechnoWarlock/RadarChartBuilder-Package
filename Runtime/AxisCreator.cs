using UnityEngine;

namespace TW.RadarChartBuilder
{
    public class AxisCreator : MonoBehaviour
    {
        [SerializeField] private LineRenderer radialAxis;
        [SerializeField] private LineRenderer[] rings;

        private Vector3[][] _ringVertices;
        private float _ringsHeight;
        private float _angle;
        private float _ringSeparation;
        private int _currentLineIndex;
        private int _radialSegmentsCount;
        private int _circularSegmentsCount;

#if UNITY_EDITOR

        public void ChangeLineWidth(float lineWidth)
        {
            foreach (LineRenderer ring in rings)
            {
                ring.startWidth = lineWidth;
                ring.endWidth = lineWidth;
            }

            radialAxis.startWidth = lineWidth;
            radialAxis.endWidth = lineWidth;
        }

        public void CreateLines(float radius, int ringsCount, int numberOfStats, int angleOffset,
            bool isRadialLinesActive)
        {
            InitiateValues(radius, ringsCount, numberOfStats);
            FillVerticesArray(ringsCount, numberOfStats, angleOffset);
            DrawCircularSegments(ringsCount, numberOfStats);
            DrawRadialLines(ringsCount, numberOfStats, isRadialLinesActive);
        }

        private void DrawCircularSegments(int ringsCount, int numberOfStats)
        {
            for (int i = 0; i < rings.Length; i++)
            {
                if (i > ringsCount - 1)
                {
                    rings[i].positionCount = 0;
                    continue;
                }

                rings[i].positionCount = numberOfStats + 2;
                for (int k = 0; k < numberOfStats; k++)
                {
                    rings[i].SetPosition(_currentLineIndex, new Vector2(_ringVertices[i][k].x, _ringVertices[i][k].y));
                    _currentLineIndex++;
                }

                rings[i].SetPosition(_currentLineIndex, new Vector2(_ringVertices[i][0].x, _ringVertices[i][0].y));
                _currentLineIndex++;
                rings[i].SetPosition(_currentLineIndex, new Vector2(_ringVertices[i][1].x, _ringVertices[i][1].y));
                _currentLineIndex = 0;

            }
        }


        private void DrawRadialLines(int ringsCount, int numberOfStats, bool isRadialLinesActive)
        {
            if (!isRadialLinesActive)
            {
                radialAxis.positionCount = 0;
                return;
            }

            _radialSegmentsCount = numberOfStats * 2;
            radialAxis.positionCount = _radialSegmentsCount;
            for (int i = 0; i < numberOfStats; i++)
            {

                radialAxis.SetPosition(_currentLineIndex,
                    new Vector2(_ringVertices[ringsCount - 1][i].x, _ringVertices[ringsCount - 1][i].y));
                _currentLineIndex++;
                radialAxis.SetPosition(_currentLineIndex, new Vector2(0, 0));
                _currentLineIndex++;
            }
        }

        private void FillVerticesArray(int ringsCount, int numberOfStats, int angleOffset)
        {
            for (int i = 0; i < ringsCount; i++)
            {
                _ringsHeight = _ringSeparation * (i + 1);
                for (int j = 0; j < numberOfStats; j++)
                {
                    float currentAngle = j * _angle + angleOffset * Mathf.Deg2Rad;
                    _ringVertices[i][j] = new Vector3(_ringsHeight * Mathf.Cos(currentAngle),
                        _ringsHeight * Mathf.Sin(currentAngle), 1);
                }
            }
        }

        private void InitiateValues(float radius, int ringsCount, int numberOfStats)
        {
            _ringSeparation = radius / ringsCount;
            _angle = 2 * Mathf.PI / numberOfStats;

            _currentLineIndex = 0;


            _ringVertices = new Vector3[ringsCount][];
            for (int i = 0; i < _ringVertices.Length; i++)
            {
                _ringVertices[i] = new Vector3[numberOfStats];
            }
        }

#endif

    }

}