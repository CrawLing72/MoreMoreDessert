using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ItemSellingController : MonoBehaviour
{
    public NormalItem ItemNORCase;
    public TMP_Text AlertText;
    public TMP_Text CountText;

    private int buyCount = 0;
    public void InvButtonSettings(int index)
    {
        ItemNORCase = ItemManager.instance.ItemInfos[index];
        Image ButtonImg = gameObject.transform.GetComponent<Image>();
        TMP_Text ButtonTXT = gameObject.transform.GetComponentInChildren<TMP_Text>();
        Button Button = gameObject.GetComponent<Button>();

        if (GameManager.stars < ItemNORCase.StarDistricted)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���źҰ�\n(����" + ItemNORCase.StarDistricted.ToString() + "�̸�)";
            Button.interactable = false;
        }
        else if (GameManager.current_money < (ItemNORCase.Price * buyCount))
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���źҰ�\n(�ڱݺ���)";
            Button.interactable = false;
        }
        else if (GameManager.currentItems[ItemNORCase.itemID] > 100) {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���źҰ�\n(�ִ������)";
            Button.interactable = false;
        }else if (buyCount == 0)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���źҰ�\n(0�� ����)";
            Button.interactable = false;
        }
        else
        {
            Button.interactable = true;
            ButtonImg.color = Color.green;
            ButtonTXT.text = "�����ϱ�\n" + $"({GameManager.currentItems[ItemNORCase.itemID]}�� ������)";
        }

        CountText.text = $"[{buyCount}��]";
    }

    public void countPlusActivity()
    {
        buyCount++;
        if (buyCount > 10)
        {
            AlertText.text = "������ �ѹ��� �ִ� 10������ ���� �����ϴٳ�~";
            buyCount = 1;
        }
        InvButtonSettings(ItemNORCase.itemID);
    }

    public void countMinusActivity()
    {

        buyCount--;
        if (buyCount < 1)
        {
            AlertText.text = "������ 1������ ���� �����ϴٳ�~";
            buyCount = 10;
        }
        InvButtonSettings(ItemNORCase.itemID);
    }

    public void BuyingActivity()
    {
        Debug.Log("Implemented!");
        GameManager.current_money -= (ItemNORCase.Price * buyCount);
        GameManager.currentItems[ItemNORCase.itemID] += buyCount;
        buyCount = 0;
        IGMenuManager.instance.SetItemSellScreen();
        GameSoundManager.instance.PlayEffects("Money");
    }


    private void Awake()
    {
        AlertText = IGMenuManager.instance.GobblinText;
    }

}
