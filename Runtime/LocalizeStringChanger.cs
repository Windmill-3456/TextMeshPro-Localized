using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Windmill {

  [RequireComponent(typeof(TextMeshProUGUI))]
  [RequireComponent(typeof(LocalizeStringEvent))]

  public class LocalizeStringChanger : MonoBehaviour {

    [HideInInspector] LocalizeStringEvent localizeStringEvent;
    [HideInInspector] LocalizedString localizedString;

    void Awake() {
      localizeStringEvent = gameObject.GetComponent<LocalizeStringEvent>();
    }

    // Example
    // void Start() {
    //   SetLocalizeString(
    //     tableReference: "Cards",
    //     tableEntryReference: "Weapon01",
    //     arguments: new Dictionary<string, object> {
    //       ["value"] = 5,
    //       ["value2"] = 6,
    //       ["times"] = 7
    //     }
    //   );
    // }

    public void SetLocalizeString(string tableReference, string tableEntryReference, Dictionary<string, object> arguments) {
      localizedString = new LocalizedString { TableReference = tableReference, TableEntryReference = tableEntryReference };
      localizedString.Arguments = new object[] { arguments };
      localizeStringEvent.StringReference = localizedString;
    }

  }
}