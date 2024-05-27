using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SellingButtonController : MonoBehaviour
{
    public FoodItem ItemNORCase;
    public TMP_Text CountText;
    int SellingCount = 0;
    public void MakeButtonSettings(int index)
    {
        ItemNORCase = ItemManager.instance.FoodInfos[index];

        Transform ButtonTrans = gameObject.transform.GetChild(3).transform;
        Image ButtonImg = ButtonTrans.GetComponent<Image>();
        TMP_Text ButtonTXT = ButtonTrans.GetComponentInChildren<TMP_Text>();
        Button Button = ButtonTrans.GetComponent<Button>();

        if(GameManager.currentGoods[index] <= 0)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "�ǸźҰ�\n(������)";
            Button.interactable = false;
        }
        else
        {
            ButtonImg.color = Color.green;
            ButtonTXT.text = $"�ǸŰ���\n({GameManager.currentGoods[index]}�� ������)";
            Button.interactable = true;
        }
    }

    public void BuyingActivity()
    {
        SellingCount = ContentsManager.instance.sellingFoodsCount;
        int modifiedCount;
        int totalGetMoney;


        if (SellingCount > (int)(UnityEngine.Random.Range(6, 10))) modifiedCount = (int)Math.Floor(SellingCount * UnityEngine.Random.Range(0.75f, 0.98f)); // random�ϰ� �ۼ������� ����
        else modifiedCount = SellingCount;

        totalGetMoney = (int)((ItemNORCase.Price * modifiedCount) * (1 + (GameManager.stars / 1000)));
        GameManager.current_money += totalGetMoney; // �� �߰�
        GameManager.stars += (ItemNORCase.StarPoints * modifiedCount); // ���� �߰�

        if (SellingCount != modifiedCount)
        ContentsManager.instance.FoxText.text = $"{ItemNORCase.Name} {SellingCount}�� �� {modifiedCount}���� �ȷ���, ������ �� {totalGetMoney}���� �������! ������ ���� �� �Ծ���ϴ� ����";
        else ContentsManager.instance.FoxText.text = $"{ItemNORCase.Name} {SellingCount}���� �ȷ���, �� {totalGetMoney}���� �������!";


        GameManager.currentGoods[ItemNORCase.itemID] -= SellingCount;

        SellingCount = 0;
        Debug.Log("Implemented!");

        ContentsManager.instance.SettingSellingScreen();
        GameSoundManager.instance.PlayEffects("Money");
    }
}
