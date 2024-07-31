using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PestUI : MonoBehaviour {
    private PestsRemoverUI _pestsRemover;
    private Image _image;
    private Button _buttonRemove;

    public Pest Pest { get; private set; }

    private void Awake() {
        _image = GetComponent<Image>();
        _buttonRemove = GetComponent<Button>();
    }

    public void Setup(Pest pest, RangeVector position, PestsRemoverUI remover) {
        _pestsRemover = remover;
        _image.sprite = pest.GetSprite();
        _image.SetNativeSize();

        Vector2 normalizedPosition = pest.Data.NormalizedPosition.GetVector();
        transform.SetPositionAndRotation(position.Lerp(normalizedPosition), pest.Data.RotationDegree.GetQuaternion());

        Pest = pest;
    }

    public void ResetPest() {
        ChangeState(false);
        _buttonRemove.onClick.RemoveListener(delegate { _pestsRemover.RemovePest(this); });
    }

    public void ChangeState(bool value) {
        gameObject.SetActive(value);
    }

    public void SetupRemoveButton() {
        _buttonRemove.onClick.AddListener(delegate { _pestsRemover.RemovePest(this); });
    }
}
