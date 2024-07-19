using UnityEngine;

public class MaterialManager : Singleton<MaterialManager> {
    [SerializeField] private Material _grayscaleMaterial;

    public Material GrayscaleMaterial => _grayscaleMaterial;
}
