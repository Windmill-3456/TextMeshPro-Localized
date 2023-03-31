using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using Windmill;

namespace WindmillEditor {
  public class TextMeshProLocalization {

    [MenuItem("GameObject/UI/Text - TextMeshPro (Localized)", false, 2001)]
    private static void Create(MenuCommand command) {
      var gameObject = new GameObject("Text (Localized)");
      GameObjectUtility.SetParentAndAlign(gameObject, command.context as GameObject);
      Undo.RegisterCreatedObjectUndo(gameObject, "Text (Localized)");
      Selection.activeObject = gameObject;

      gameObject.AddComponent<TextMeshProUGUI>();
      var textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
      textMeshProUGUI.text = "New Text";

      gameObject.AddComponent<UnityEngine.Localization.Components.LocalizeStringEvent>();
      var localizeStringEvent = gameObject.GetComponent<UnityEngine.Localization.Components.LocalizeStringEvent>();

      var call = Delegate.CreateDelegate(
        type: typeof(UnityEngine.Events.UnityAction<string>),
        firstArgument: textMeshProUGUI,
        method: textMeshProUGUI.GetType().GetProperty("text").GetSetMethod()
      ) as UnityEngine.Events.UnityAction<string>;

      UnityEditor.Events.UnityEventTools.AddPersistentListener<string>(
        unityEvent: localizeStringEvent.OnUpdateString,
        call: call
      );

      localizeStringEvent.OnUpdateString.SetPersistentListenerState(
        index: 0,
        state: UnityEngine.Events.UnityEventCallState.RuntimeOnly
      );

      gameObject.AddComponent<LocalizeStringChanger>();
    }
  }
}