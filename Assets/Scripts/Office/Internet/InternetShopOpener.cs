using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InternetShopOpener : MonoBehaviour
{
    [SerializeField] private InternetDownload _download;
    [SerializeField] private BaseShop _internetShop;

    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(delegate { _download.StartDownload(_internetShop); });
    }
}
