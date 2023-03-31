using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WindmillEditor {
  public class LocalizationTablesUtility : EditorWindow {

    private Vector2 scrollPos = Vector2.zero;
    private int tableCollectionIndex = 0;
    private bool isSmart = false;
    private List<string> tableCollection = new List<string>();

    [MenuItem("Tools/Windmill/Localization Tables Utility")]
    static void Draw() {
      var window = (LocalizationTablesUtility)EditorWindow.GetWindow(typeof(LocalizationTablesUtility));
      window.minSize = new Vector2(500f, 150f);
      window.maxSize = window.minSize;
      window.Show();
    }

    void OnEnable() {
      titleContent = new GUIContent("Localization Tables Utility");
    }

    void OnGUI() {
      var style = EditorStyle.Get;
      var labelWidth = EditorGUIUtility.labelWidth;

      using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos, style.area)) {
        scrollPos = scrollView.scrollPosition;
        EditorGUIUtility.labelWidth = 160;
        GUILayout.Label("Smart Settings", style.heading);

        using (new EditorGUILayout.VerticalScope(style.area)) {

          foreach (var collection in UnityEditor.Localization.LocalizationEditorSettings.GetStringTableCollections()) {
            tableCollection.Add(collection.name);
          }

          tableCollectionIndex = EditorGUILayout.Popup("Table Collection", tableCollectionIndex, tableCollection.ToArray());
          isSmart = EditorGUILayout.Toggle("Smart", isSmart);

          if (GUI.Button(new Rect(Screen.width - 120f, 105f, 100.0f, 20.0f), "Setup")) {
            if (EditorUtility.DisplayDialog("Smart Settings", "Set all Smart settings for " + tableCollection[tableCollectionIndex] + " in the Localization Tables to " + (isSmart ? "Enable" : "Disable") + ".", "OK", "Cancel")) {
              foreach (var table in UnityEditor.Localization.LocalizationEditorSettings.GetStringTableCollection(tableCollection[tableCollectionIndex]).StringTables) {
                foreach (var element in table) {
                  element.Value.IsSmart = isSmart;
                }
                EditorUtility.SetDirty(table);
              }
            }
          }
        }
      }
    }
  }
}