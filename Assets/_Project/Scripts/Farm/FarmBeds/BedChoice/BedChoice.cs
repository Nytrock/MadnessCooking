using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(FarmBed))]
    public class BedChoice : MonoBehaviour {
        [SerializeField] private GameObject _addButton;
        [SerializeField] private BedTypeHolder[] _beds;
        private FarmBedData _bedData;

        private BedChoiceUI _UI;
        private FarmBed _farmBed;

        public void MouseDown() {
            if (_bedData.IsActive)
                _farmBed.MouseDown();
            else
                _UI.ActivateBedChoice(this);
        }

        private void Awake() {
            _farmBed = GetComponent<FarmBed>();
            _farmBed.BedWasReset += delegate { _addButton.SetActive(true); };
        }

        private void LateStart() {
            if (!_bedData.IsActive)
                _farmBed.enabled = false;
            _addButton.SetActive(!_bedData.IsActive);
        }

        public void SetType(BedType bedType) {
            BedTypeHolder bed = FindBedTypeHolder(bedType);

            _farmBed.SetBedType(bed);
            _farmBed.enabled = true;
            _addButton.SetActive(false);
        }

        public void Bind(FarmData data, FarmBedData bedData) {
            _bedData = bedData;

            BedTypeHolder nowBedTypeHolder = null;
            foreach (var bedTypeHolder in _beds) {
                if (_bedData.BedType == bedTypeHolder.Type)
                    nowBedTypeHolder = bedTypeHolder;
                bedTypeHolder.Bind(bedData);
                bedTypeHolder.ChangeState(false);
            }
            _farmBed.Bind(data, _bedData, nowBedTypeHolder);

            LateStart();
        }

        public void Setup(FarmBedSettings settings) {
            _UI = settings.BedChoiceUI;
            _farmBed.Setup(settings);
        }

        public void UpdateUpgrades() {
            _farmBed.UpdateGlobalUpgrades();
        }

        private BedTypeHolder FindBedTypeHolder(BedType bedType) {
            if (bedType == null)
                return null;

            foreach (var bed in _beds)
                if (bedType == bed.Type)
                    return bed;

            throw new ArgumentNullException("There's no bed holder with such bed Type");
        }
    }
}
