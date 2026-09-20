using UnityEngine;

namespace MadnessCooking.General {
    public class FlyingItemPool : Pool<FlyingItem> {
        [SerializeField] private FlyingItem _prefab;
        [SerializeField] private float _bottomPosition;

        public FlyingItem GetItem(BuyableItem item) {
            FlyingItem flyingItem = base.GetObject();
            flyingItem.SetSprite(item);
            flyingItem.ChangeState(true);
            return flyingItem;
        }

        public override void PutObject(FlyingItem flyingItem) {
            base.PutObject(flyingItem);
            flyingItem.ChangeState(false);
        }

        protected override FlyingItem CreateObject() {
            FlyingItem flyingItem = Instantiate(_prefab, _container);
            flyingItem.SetBottomPosition(_bottomPosition);
            return flyingItem;
        }
    }
}
