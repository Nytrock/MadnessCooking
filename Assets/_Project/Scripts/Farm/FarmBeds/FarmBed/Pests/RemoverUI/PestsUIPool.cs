using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class PestsUIPool : Pool<PestUI> {
        [SerializeField] private PestUI _pestPrefab;

        [Header("Borders")]
        [SerializeField] private RangeVector _position;
        private PestsRemoverUI _remover;

        public PestUI GetObject(Pest pest) {
            PestUI pestUI = GetObject();
            pestUI.ChangeState(true);
            pestUI.Setup(pest, _position, _remover);
            return pestUI;
        }

        protected override PestUI CreateObject() {
            return Instantiate(_pestPrefab, _container);
        }

        public override void PutObject(PestUI pest) {
            pest.ResetPest();
            base.PutObject(pest);
        }

        public void SetRemover(PestsRemoverUI pestsRemoverUI) {
            _remover = pestsRemoverUI;
        }
    }
}
