using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ForIngridients : MonoBehaviour
{
    public List<Ingridient> HaveIngridients;
    public List<int> NumberIngridients;
    public Transform ContainerImages;
    public Animator Panel;
    public IngridientCell ingridientCell;
    public TextIngridient ingridientText;
    public Transform CellContainer;
    public Transform CellTextContainer;
    public AudioSource Open;
    public AudioSource Close;

    void Start()
    {
        RenderIngridients();
    }

    public void ActivePanel()
    {
        Panel.SetBool("PanelActive", !Panel.GetComponent<Animator>().GetBool("PanelActive"));
        if (Panel.GetComponent<Animator>().GetBool("PanelActive"))
            Open.Play();
        else
            Close.Play();
    }

    public void RenderIngridients() 
    {
        foreach (Transform child in CellContainer)
            Destroy(child.gameObject);
        foreach (Transform child in ContainerImages)
            child.gameObject.SetActive(false);
        for (int i = 0; i < NumberIngridients.Count; i++) {
            var cell = Instantiate(ingridientCell, CellContainer);
            cell.IngridientImage.sprite = HaveIngridients[i].ImageIngridient;
            cell.Count.text = NumberIngridients[i].ToString();
            var descr = Instantiate(ingridientText, CellTextContainer);
            descr.MainText.text = HaveIngridients[i].Name;
            descr.DescriptionText.text = HaveIngridients[i].Description;
            cell.Description = descr;
        }
        int sum = NumberIngridients.Sum(x => x);
        for (int i = 0; i < sum / 8; i++) {
            if (i < ContainerImages.childCount) { 
                ContainerImages.GetChild(i).GetComponent<Image>().sprite = HaveIngridients[Random.Range(0, HaveIngridients.Count)].ImageIngridient;
                ContainerImages.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
