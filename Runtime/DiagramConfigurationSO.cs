using UnityEngine;

namespace TW.RadarChartBuilder
{
    public class DiagramConfigurationSO : ScriptableObject
    {
        [SerializeField] private int radius;
        [SerializeField] private int angleOffset;
        [SerializeField] private int statsCount;

        public int Radius
        {
            get => radius;
            set => radius = value;
        }

        public int AngleOffset
        {
            get => angleOffset;
            set => angleOffset = value;
        }

        public int StatsCount
        {
            get => statsCount;
            set => statsCount = value;
        }
    }
}