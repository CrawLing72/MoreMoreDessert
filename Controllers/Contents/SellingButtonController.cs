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
            ButtonTXT.text = "판매불가\n(재고없음)";
            Button.interactable = false;
        }
        else
        {
            ButtonImg.color = Color.green;
            ButtonTXT.text = $"판매가능\n({GameManager.currentGoods[index]}개 보유중)";
            Button.interactable = true;
        }
    }

    public void BuyingActivity()
    {
        SellingCount = ContentsManager.instance.sellingFoodsCount;
        int modifiedCount;
        int totalGetMoney;


        if (SellingCount > (int)(UnityEngine.Random.Range(6, 10))) modifiedCount = (int)Math.Floor(SellingCount * UnityEngine.Random.Range(0.75f, 0.98f)); // random하게 퍼센테이지 결정
        else modifiedCount = SellingCount;

        totalGetMoney = (int)((ItemNORCase.Price * modifiedCount) * (1 + (GameManager.stars / 1000)));
        GameManager.current_money += totalGetMoney; // 돈 추가
        GameManager.stars += (ItemNORCase.StarPoints * modifiedCount); // 평판 추가

        if (SellingCount != modifiedCount)
        ContentsManager.instance.FoxText.text = $"{ItemNORCase.Name} {SellingCount}개 중 {modifiedCount}개가 팔려서, 팁까지 총 {totalGetMoney}원을 벌었어요! 남은건 제가 다 먹었답니다 ㅎㅎ";
        else ContentsManager.instance.FoxText.text = $"{ItemNORCase.Name} {SellingCount}개가 팔려서, 총 {totalGetMoney}원을 벌었어요!";


        GameManager.currentGoods[ItemNORCase.itemID] -= SellingCount;

        SellingCount = 0;
        Debug.Log("Implemented!");

        ContentsManager.instance.SettingSellingScreen();
        GameSoundManager.instance.PlayEffects("Money");
    }
}
