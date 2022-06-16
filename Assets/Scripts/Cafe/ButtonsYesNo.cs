using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsYesNo : MonoBehaviour
{
    public Client client;
    public string type;
    public GameObject Buttons;

    void Update() {
        if (client.NeedFood != null && type == "Yes" && !client.Eating) {
            if (client.cafe.orders.MadeOreders[client.cafe.orders.OrdersClients.IndexOf(client.NeedFood)])
                this.GetComponent<Button>().interactable = true;
            else
                this.GetComponent<Button>().interactable = false;
        }
    }

    public void Click() {
        if (NewOrContinue.Education)
            client.cafe.education.AddIndex(false);
        if (!client.Eating) {
            if (client.cafe.Camera.position.x == 0) {
                if (type == "No")
                    client.ClickNo.Play();
                else
                    client.ClickYes.Play();
            }
            if (client.NeedFood != null) {
                int i = client.cafe.orders.OrdersClients.IndexOf(client.NeedFood);
                int cost = client.cafe.orders.OrdersClients[i].Cost;
                Destroy(client.cafe.orders.Orders[i].gameObject);
                client.cafe.orders.OrdersClients.RemoveAt(i);
                client.cafe.orders.Orders.RemoveAt(i);
                client.cafe.orders.MadeOreders.RemoveAt(i);
                if (type == "Yes")  {
                    client.cafe.money.AddCoins(cost);
                    client.WaitClient.maxValue = client.NeedFood.TimeToCooking * 0.5f;
                    client.Eat.sprite = client.NeedFood.ImageFood;
                    client.Eat.gameObject.SetActive(true);
                    client.ImageFood.gameObject.SetActive(false);
                    client.Eating = true;
                    client.WaitClient.value = 0;
                    Buttons.SetActive(false);
                } else {
                    client.WaitClient.value = 0;
                    Buttons.SetActive(false);
                    Destroy();
                }
            } else {
                client.WaitClient.value = 0;
                Buttons.SetActive(false);
                Destroy();
            }
        } else {
            client.EatingVolume.Stop();
            Destroy();
        }
    }

    public void Destroy() {
        foreach (SpriteRenderer rend in client.AllChapterClient)
            rend.sortingLayerName = "Hand";
        if (client.transform.localScale.x < 0)
            client.transform.localScale *= new Vector2(-1, 1);
        client.Table.SetActive(false);
        client.cafe.AvailableClients[client.NumberIn] = null;
        client.WaitClient.gameObject.SetActive(false);
        client.Eat.gameObject.SetActive(false);
        client.GoToDestroy();
    }
}
