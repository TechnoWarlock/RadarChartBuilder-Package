using UnityEngine;


namespace TW.RadarChartBuilder
{
    public class DiagramStatsPlotter : MonoBehaviour
    {
        [SerializeField] private CanvasRenderer canvasRenderer;
        [SerializeField] private MeshCreator meshCreator;
        [SerializeField] private DiagramConfigurationSO diagramConfigurationSo;
        [SerializeField] private Material material;

        public void Create(int[] stats)
        {
            VerifyStats(stats);
            meshCreator.CreatePolygon(canvasRenderer, material, diagramConfigurationSo.StatsCount,
                diagramConfigurationSo.AngleOffset, stats);
        }

        private void VerifyStats(int[] stats)
        {
            if (stats.Length != diagramConfigurationSo.StatsCount)
            {
                Debug.Log("The length of the stats array is different from PNG diagram");
            }

            for (int i = 0; i < stats.Length; i++)
            {
                stats[i] = Mathf.Clamp(stats[i], 0, diagramConfigurationSo.Radius);
            }
        }
    }
}