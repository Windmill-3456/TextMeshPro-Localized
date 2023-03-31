using System;
using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
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

      gameObject.AddComponent<LocalizeStringEvent>();
      var localizeStringEvent = gameObject.GetComponent<LocalizeStringEvent>();

      var call = Delegate.CreateDelegate(
        type: typeof(UnityEngine.Events.UnityAction<string>),
        firstArgument: textMeshProUGUI,
        method: textMeshProUGUI.GetType().GetProperty("text").GetSetMethod()
      ) as UnityAction<string>;

      UnityEventTools.AddPersistentListener<string>(
        unityEvent: localizeStringEvent.OnUpdateString,
        call: call
      );

      localizeStringEvent.OnUpdateString.SetPersistentListenerState(
        index: 0,
        state: UnityEventCallState.RuntimeOnly
      );

      gameObject.AddComponent<LocalizeStringChanger>();
    }
  }
}