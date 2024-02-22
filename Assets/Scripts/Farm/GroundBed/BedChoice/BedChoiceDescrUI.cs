using UnityEngine;

public class BedChoiceDescrUI : MonoBehaviour
{
    [SerializeField] private ItemInfoRenderer _shower;
    private bool _isActive;

    public void UpdateDescription(BedType bedType)
    {
        _shower.SetItemInfo(bedType);
        if (bedType.Cost > 0)
            _shower.SetCountText(bedType.Cost.ToString());
        else
            _shower.SetCountText("Free");
    }

    public void ChangeActive()
    {
        _isActive = !_isActive;
        gameObject.SetActive(_isActive);
    }

    public void Disable()
    {
        _isActive = false;
        gameObject.SetActive(false);
    }
}
