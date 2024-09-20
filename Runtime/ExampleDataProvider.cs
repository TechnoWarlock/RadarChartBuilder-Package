using UnityEngine;
using UnityEngine.UI;


namespace TW.RadarChartBuilder
{
    public class ExampleDataProvider : MonoBehaviour
    {
        [SerializeField] private DiagramStatsPlotter _diagramStatsPlotter;
        [SerializeField] private Button randomButton;

        private void Awake()
        {
            randomButton.onClick.AddListener(OnRandom);
        }


        private void OnRandom()
        {
            int[] data = new int[3];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Random.Range(0, 300);
            }

            _diagramStatsPlotter.Create(data);
        }
    }
}