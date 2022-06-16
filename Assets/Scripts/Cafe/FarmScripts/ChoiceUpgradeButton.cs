using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUpgradeButton : MonoBehaviour
{
    public Image ThisImage;
    public Image Images;
    public Text Name;
    public Upgrade HaveUpgrade;
    public UpgradeChoice choice;
    public Sprite ActiveSprite;
    public Sprite NoActiveSprite;
    public AudioSource Press;

    void Update()
    {
        if (choice.ChoisedUpgrade == this)
            ThisImage.sprite = ActiveSprite;
        else
            ThisImage.sprite = NoActiveSprite;
            
    }

    public void ChoiceHaveUpgrade()
    {
        Press.Play();
        if (choice.ChoisedUpgrade != this || choice.ChoisedUpgrade == null) {
            choice.choisedplant.Images.sprite = HaveUpgrade.SpriteUpgrade;
            choice.choisedplant.Name.text = HaveUpgrade.Name;
            choice.choisedplant.Description.text = HaveUpgrade.Description;
            choice.choisedplant.Cost = (int)(HaveUpgrade.Cost * 0.1f);
            choice.choisedplant.CostText.text = choice.choisedplant.Cost.ToString();
            choice.choisedplant.BuyingUpgrade = this;
            choice.ChoisedUpgrade = this;
        } else {
            choice.choisedplant.BuyingUpgrade = null;
            choice.ChoisedUpgrade = null;
        }
    }

    public void SetVizual()
    {
        Images.sprite = HaveUpgrade.SpriteUpgrade;
        Name.text = HaveUpgrade.Name;
    }

    public void BuyUpgrade()
    {
        Press.Play();
        choice.groundBed.ExistUpgrades.Add(HaveUpgrade);
        choice.groundBed.UpdateUpgrades();
        choice.SetUpgrades();
    }
}
