using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PestUI : MonoBehaviour
{
    private PestsRemoverUI _pestsRemover;
    private Image _image;
    private Button _buttonRemove;

    public Pest Pest { get; private set; }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _buttonRemove = GetComponent<Button>();
    }

    public void Setup(Pest pest, Vector2 leftDown, Vector2 rightUp, PestsRemoverUI remover)
    {
        _pestsRemover = remover;
        _image.sprite = pest.GetSprite();
        _image.SetNativeSize();

        float x = Mathf.Lerp(leftDown.x, rightUp.x, pest.PestData.NormalizedPosition.x);
        float y = Mathf.Lerp(leftDown.y, rightUp.y, pest.PestData.NormalizedPosition.y);
        transform.SetPositionAndRotation(new Vector2(x, y), pest.PestData.RotationDegree.GetQuaternion());

        Pest = pest;
    }

    public void ResetPest()
    {
        ChangeState(false);
        _buttonRemove.onClick.RemoveListener(delegate { _pestsRemover.RemovePest(this); });
    }

    public void ChangeState(bool value)
    {
        gameObject.SetActive(value);
    }

    internal void SetupRemoveButton()
    {
        _buttonRemove.onClick.AddListener(delegate { _pestsRemover.RemovePest(this); });
    }
}
