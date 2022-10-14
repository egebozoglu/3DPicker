using UnityEngine;
using UnityEditor;

namespace Picker3D.EditorScene { 

    [CustomEditor(typeof(LevelEditorManager))]
    public class LevelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelEditorManager manager = (LevelEditorManager)target;

            GUILine();
            #region Creating Object
            EditorGUILayout.LabelField("Create Object");
            if (GUILayout.Button("Platform"))
            {

            }
            if (GUILayout.Button("Picker"))
            {

            }
            if (GUILayout.Button("Sphere"))
            {

            }
            if (GUILayout.Button("Cube"))
            {

            }
            if (GUILayout.Button("Cylinder"))
            {

            }
            #endregion

            GUILine();
            #region Set Complete Counts
            EditorGUILayout.LabelField("Set Complete Counts");
            #endregion

        }

        void GUILine(int i_height = 1)

        {
            GUILayout.Space(20);

            Rect rect = EditorGUILayout.GetControlRect(false, i_height);
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

            GUILayout.Space(20);
        }

    }
}