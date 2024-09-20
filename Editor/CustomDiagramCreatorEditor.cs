using UnityEngine;
using UnityEditor;

namespace TW.RadarChartBuilder
{
    [CustomEditor(typeof(DiagramCreator))]
    public class CustomDiagramCreatorEditor : Editor
    {
        private SerializedProperty _meshFilterProp;
        private SerializedProperty _meshCreatorProp;
        private SerializedProperty _axisCreatorProp;
        private SerializedProperty _diagramPNGSaverProp;
        private SerializedProperty _diagramDataCreatorProp;
        private SerializedProperty _startPointProp;

        private SerializedProperty _statsCountProp;
        private SerializedProperty _ringsCountProp;
        private SerializedProperty _radiusProp;
        private SerializedProperty _angleOffsetProp;
        private SerializedProperty _lineWidthProp;
        private SerializedProperty _isRadialLinesActiveProp;

        private SerializedProperty _diagramNameProp;
        private SerializedProperty _diagramFolderProp;
        private SerializedProperty _diagramStartPointNameProp;
        private SerializedProperty _diagramConfigurationNameProp;
        private SerializedProperty _diagramConfigurationFolderProp;

        private DiagramCreator _diagramCreator;

        private float _lastLineWidth;
        private int _lastRingsCount;
        private int _lastAngleOffset;
        private int _lastRadius;
        private int _lastStatsCount;
        private bool _lastRadialLines;
        private int _newAnglesOffset;

        void OnEnable()
        {
            _diagramCreator = (DiagramCreator)target;

            _meshFilterProp = serializedObject.FindProperty("meshFilter");
            _meshCreatorProp = serializedObject.FindProperty("meshCreator");
            _axisCreatorProp = serializedObject.FindProperty("axisCreator");
            _diagramPNGSaverProp = serializedObject.FindProperty("diagramPNGSaver");
            _diagramDataCreatorProp = serializedObject.FindProperty("diagramDataCreator");
            _startPointProp = serializedObject.FindProperty("startPoint");

            _statsCountProp = serializedObject.FindProperty("statsCount");
            _ringsCountProp = serializedObject.FindProperty("ringsCount");
            _radiusProp = serializedObject.FindProperty("radius");
            _angleOffsetProp = serializedObject.FindProperty("angleOffset");
            _lineWidthProp = serializedObject.FindProperty("lineWidth");
            _isRadialLinesActiveProp = serializedObject.FindProperty("isRadialLinesActive");

            _diagramNameProp = serializedObject.FindProperty("diagramName");
            _diagramFolderProp = serializedObject.FindProperty("diagramFolder");
            _diagramStartPointNameProp = serializedObject.FindProperty("diagramStartPointName");
            _diagramConfigurationNameProp = serializedObject.FindProperty("diagramConfigurationName");
            _diagramConfigurationFolderProp = serializedObject.FindProperty("diagramConfigurationFolder");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            LastValues();

            DrawElements();

            HandleInspectorUpdates();
        }

        private void HandleInspectorUpdates()
        {
            serializedObject.ApplyModifiedProperties();

            if (!Mathf.Approximately(_lineWidthProp.floatValue, _lastLineWidth))
            {
                _diagramCreator.DoLineWidth();
            }

            if (_ringsCountProp.intValue != _lastRingsCount || _angleOffsetProp.intValue != _lastAngleOffset ||
                _lastRadialLines != _isRadialLinesActiveProp.boolValue || _statsCountProp.intValue != _lastStatsCount)
            {
                _diagramCreator.Create();
            }

            if (_radiusProp.intValue != _lastRadius)
            {
                _diagramCreator.Create();
                _diagramCreator.DoCameraSize();
            }

            if (GUILayout.Button("Save Diagram"))
            {
                _diagramCreator.TriggerSaveDiagram();
            }
        }

        private void LastValues()
        {
            _lastLineWidth = _lineWidthProp.floatValue;
            _lastRingsCount = _ringsCountProp.intValue;
            _lastAngleOffset = _angleOffsetProp.intValue;
            _lastRadius = _radiusProp.intValue;
            _lastStatsCount = _statsCountProp.intValue;
            _lastRadialLines = _isRadialLinesActiveProp.boolValue;
        }

        private void DrawElements()
        {
            EditorGUILayout.PropertyField(_meshFilterProp);
            EditorGUILayout.PropertyField(_meshCreatorProp);
            EditorGUILayout.PropertyField(_axisCreatorProp);
            EditorGUILayout.PropertyField(_diagramPNGSaverProp);
            EditorGUILayout.PropertyField(_diagramDataCreatorProp);
            EditorGUILayout.PropertyField(_startPointProp);

            GUILayout.Label("Diagram Configuration", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20 });

            EditorGUILayout.IntSlider(_statsCountProp, 3, 10, new GUIContent("Stats"));
            EditorGUILayout.IntSlider(_ringsCountProp, 2, 10, new GUIContent("Rings"));
            EditorGUILayout.IntSlider(_radiusProp, 50, 2000, new GUIContent("Radius"));

            _newAnglesOffset = _angleOffsetProp.intValue;

            GUILayout.BeginHorizontal();

            GUILayout.Label(new GUIContent("Angle offset"), EditorStyles.boldLabel);

            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                _newAnglesOffset--;
                GUI.FocusControl(null);
            }

            if (GUILayout.Button("+", GUILayout.Width(30)))
            {
                _newAnglesOffset++;
                GUI.FocusControl(null);
            }

            _newAnglesOffset = EditorGUILayout.IntField(_newAnglesOffset, GUILayout.Width(50));

            GUILayout.EndHorizontal();

            _angleOffsetProp.intValue = Mathf.Clamp(_newAnglesOffset, 0, 360);


            EditorGUILayout.Slider(_lineWidthProp, 1, 100, new GUIContent("Line Width"));
            EditorGUILayout.PropertyField(_isRadialLinesActiveProp, new GUIContent("Radial Lines"));

            EditorGUILayout.PropertyField(_diagramNameProp);
            EditorGUILayout.PropertyField(_diagramFolderProp);
            EditorGUILayout.PropertyField(_diagramStartPointNameProp);
            EditorGUILayout.PropertyField(_diagramConfigurationNameProp);
            EditorGUILayout.PropertyField(_diagramConfigurationFolderProp);
        }
    }
}