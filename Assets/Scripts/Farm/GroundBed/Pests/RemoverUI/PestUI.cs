using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class PestUI : MonoBehaviour
{
    private Image _image;
    private Button _buttonRemove;
    private Pest _pest;

    public Button ButtonRemove => _buttonRemove;
    public Pest Pest => _pest;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _buttonRemove = GetComponent<Button>();
    }

    public void Setup(Pest pest, Transform leftDown, Transform rightUp)
    {
        _image.sprite = pest.GetSprite();
        _image.SetNativeSize();

        var xNormalized = Mathf.InverseLerp(pest.LeftDown.position.x, pest.RightUp.position.x, pest.transform.position.x);
        var yNormalized = Mathf.InverseLerp(pest.LeftDown.position.y, pest.RightUp.position.y, pest.transform.position.y);
        var x = Mathf.Lerp(leftDown.position.x, rightUp.position.x, xNormalized);
        var y = Mathf.Lerp(leftDown.position.y, rightUp.position.y, yNormalized);
        
        transform.SetPositionAndRotation(new Vector2(x, y), pest.transform.rotation);
        _pest = pest;
    }

    public void ResetPest()
    {
        ChangeState(false);
        _buttonRemove.onClick.RemoveAllListeners();
    }

    public void ChangeState(bool value)
    {
        gameObject.SetActive(value);
    }
}
