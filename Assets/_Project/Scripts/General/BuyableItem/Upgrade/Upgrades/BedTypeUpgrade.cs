using UnityEngine;

namespace MadnessCooking.General {
    [CreateAssetMenu(menuName = AssetMenuName + nameof(BedTypeUpgrade))]
    public class BedTypeUpgrade : BaseUpgrade {
        [SerializeField] private BedType _bedType;

        public BedType BedType => _bedType;
        public override UpgradeType Type => UpgradeType.BedType;
    }
}
