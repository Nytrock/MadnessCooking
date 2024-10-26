using UnityEngine;

public class FarmBedAudio : MonoBehaviour {
    [SerializeField] private AudioSource _waterAudio;
    [SerializeField] private AudioSource _fertilizeAudio;
    [SerializeField] private FarmBed _farmBed;

    private void Awake() {
        _farmBed.BedWatered += _waterAudio.Play;
        _farmBed.BedFertilized += _fertilizeAudio.Play;
    }
}
