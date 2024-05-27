using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class InvenButtonController : MonoBehaviour
{
    public NormalItem ItemNORCase;
    public TMP_Text AlertText;
    public void InvButtonSettings(int index)
    {
        ItemNORCase = ItemManager.instance.ItemInfos[index];
        Image ButtonImg = gameObject.transform.GetComponent<Image>();
        TMP_Text ButtonTXT = gameObject.transform.GetComponentInChildren<TMP_Text>();
        Button Button = gameObject.GetComponent<Button>();

        if (GameManager.stars < ItemNORCase.StarDistricted)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���Ұ�\n(����"+ItemNORCase.StarDistricted.ToString()+"�̸�)";
            Button.interactable = false;
        } else if (GameManager.currentItems[ItemNORCase.itemID] == 0)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���Ұ�\n(�̺���)";
            Button.interactable = false;
        }
        else
        {
            Button.interactable = true;
            ButtonImg.color = Color.green;
            ButtonTXT.text = "����ϱ�\n(" + GameManager.currentItems[index] + "�� ������)";
        }
    }

    public void ItemActivity()
    {
        if(GameManager.mp + ItemNORCase.MP < 0)
        {
            GameManager.mp = 0;
            AlertText.color = Color.red;
            AlertText.text = "��� : MP �������� " + ItemNORCase.Name + "��(��) ��� �Ұ��մϴ�.";
            return;
        }else if(GameManager.mp + ItemNORCase.MP > GameManager.instance.MP_MAX)
        {
            GameManager.mp = GameManager.instance.MP_MAX;
            AlertText.color = Color.yellow;
            AlertText.text = "���� : MP �ִ�ġ " + GameManager.instance.MP_MAX + "������ �����˴ϴ�.";
        }
        else
        {
            GameManager.mp += ItemNORCase.MP;
        }

        if (GameManager.hp + ItemNORCase.HP < 0)
        {
            GameManager.hp = 0;
            AlertText.color = Color.red;
            AlertText.text = "��� : HP �������� " + ItemNORCase.Name + "��(��) ��� �Ұ��մϴ�.";
            return;
        }
        else if (GameManager.hp + ItemNORCase.HP > GameManager.instance.HP_MAX)
        {
            GameManager.hp = GameManager.instance.HP_MAX;
            AlertText.color = Color.yellow;
            AlertText.text = "���� : HP �ִ�ġ " + GameManager.instance.HP_MAX + "������ �����˴ϴ�.";
        }
        else
        {
            GameManager.hp += ItemNORCase.HP;
        }

        GameManager.currentItems[ItemNORCase.itemID] -= 1;
        InvButtonSettings(ItemNORCase.itemID);
        IGMenuManager.instance.SettingInventoryScreen();
    }

    private void Start()
    {
        AlertText = IGMenuManager.instance.AlertText;
    }

}
