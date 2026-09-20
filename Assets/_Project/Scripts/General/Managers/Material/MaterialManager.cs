using UnityEngine;

namespace MadnessCooking.General {
    public class MaterialManager : Singleton<MaterialManager> {
        [SerializeField] private Material _grayscaleMaterial;
        [SerializeField] private Material _blackMaterial;

        public Material GrayscaleMaterial => _grayscaleMaterial;
        public Material BlackMaterial => _blackMaterial;
    }
}
