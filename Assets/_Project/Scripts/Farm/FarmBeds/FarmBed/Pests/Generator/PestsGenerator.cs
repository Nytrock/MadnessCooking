using System;
using System.Collections.Generic;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class PestsGenerator : MonoBehaviour {
        [SerializeField] private PestsPool _pool;
        [SerializeField] private VisualChanger _pestRemover;
        [SerializeField, Min(1)] private int _maxPests;
        [SerializeField, Min(0)] private float _onePestSlowdown;
        [SerializeField] private RangeFloat _spawnTime;
        private PestsGeneratorData _data;

        private readonly List<Pest> _pests = new();
        private bool _isPause;

        public event Action PestsChanged;
        public event Action PestsCleaned;

        private void Update() {
            if (!_data.IsActive || _isPause || _data.IsPestsRemoved)
                return;

            _data.UpdateTime();
            if (_data.NowTime > _data.NeedTime) {
                SpawnPest();
                _data.SetNewTime(_spawnTime.RandomValue);
            }
        }

        public void ChangePause(bool newState) {
            _isPause = newState;
        }

        private void SpawnPest() {
            Pest pest = _pool.GetObject();
            _pests.Add(pest);
            _data.AddPest(pest.Data);
            UpdateSlowdown();
        }

        public void Activate() {
            _data.SetMaxPestsCount(_maxPests);
            _pestRemover.ChangeState(_data.IsPestsRemoved);

            if (_pool as StaticPestsPool)
                (_pool as StaticPestsPool).SetupFreePestsList();

            foreach (var pestData in _data.Pests) {
                Pest pest = _pool.GetObject(pestData.PrefabIndex);
                _pests.Add(pest);
                pest.Bind(pestData);
            }
        }

        public void ChangeState(bool newState) {
            _data.ChangeMode(newState);

            if (newState) {
                _data.SetNewTime(_spawnTime.RandomValue);
                return;
            }

            _pestRemover.ChangeState(false);
        }

        public void RemovePests() {
            CleanPests();
            _pestRemover.ChangeState(true);
        }

        public void CleanPests() {
            foreach (var pest in _pests)
                _pool.PutObject(pest);
            _pests.Clear();
            _data.CleanPests();

            PestsChanged?.Invoke();
            PestsCleaned?.Invoke();
        }

        public IEnumerable<Pest> Pests() {
            foreach (var pest in _pests) {
                yield return pest;
            }
        }

        public void RemovePest(Pest pest) {
            _pool.RemovePest(pest);
            _data.RemovePest(pest.Data);
            _pests.Remove(pest);
            UpdateSlowdown();
        }

        private void UpdateSlowdown() {
            if (_pests.Count == _maxPests)
                _data.SetMaxSlowdown();
            else
                _data.SetRegularSlowdown(_onePestSlowdown);
            PestsChanged?.Invoke();
        }

        public void Bind(PestsGeneratorData data) {
            _data = data;
        }
    }
}
