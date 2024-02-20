using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetCatalog : MonoBehaviour
{
    [SerializeField] private Transform _pagesContainer;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    private readonly List<InternetCatalogPage> _pages = new();
    private int _nowPage = 0;

    public Transform PagesContainer => _pagesContainer;

    public void AddPage(InternetCatalogPage page)
    {
        _pages.Add(page);
        page.ChangeState(false);
    }

    public void ActivateFirstPage()
    {
        _pages[0].ChangeState(true);
        UpdateButtons();
    }

    public void NextPage()
    {
        _pages[_nowPage].ChangeState(false);
        _nowPage++;
        _pages[_nowPage].ChangeState(true);
        UpdateButtons();
    }
    
    public void PreviousPage()
    {
        _pages[_nowPage].ChangeState(false);
        _nowPage--;
        _pages[_nowPage].ChangeState(true);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        _nextButton.interactable = _nowPage < _pages.Count - 1;
        _previousButton.interactable = _nowPage > 0;
    }
}
