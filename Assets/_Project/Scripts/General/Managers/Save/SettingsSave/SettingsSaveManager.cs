using System.Linq;
using UnityEngine;

namespace MadnessCooking.General {
    public class SettingsSaveManager : SaveManager<SettingsData> {
        [SerializeField, Interface(typeof(ISettingable))] private MonoBehaviour[] _settingables;

        protected override string FileName => "settings";

        protected override void UpdateData() {
            foreach (ISettingable settingable in _settingables.Cast<ISettingable>())
                settingable.SetSettings(_data);
        }
    }
}
