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
            ButtonTXT.text = "구매불가\n(평판" + ItemNORCase.StarDistricted.ToString() + "미만)";
            Button.interactable = false;
        }
        else if (GameManager.current_money < (ItemNORCase.Price * buyCount))
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "구매불가\n(자금부족)";
            Button.interactable = false;
        }
        else if (GameManager.currentItems[ItemNORCase.itemID] > 100) {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "구매불가\n(최대소지량)";
            Button.interactable = false;
        }else if (buyCount == 0)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "구매불가\n(0개 선택)";
            Button.interactable = false;
        }
        else
        {
            Button.interactable = true;
            ButtonImg.color = Color.green;
            ButtonTXT.text = "구매하기\n" + $"({GameManager.currentItems[ItemNORCase.itemID]}개 보유중)";
        }

        CountText.text = $"[{buyCount}개]";
    }

    public void countPlusActivity()
    {
        buyCount++;
        if (buyCount > 10)
        {
            AlertText.text = "물건은 한번에 최대 10개까지 구매 가능하다네~";
            buyCount = 1;
        }
        InvButtonSettings(ItemNORCase.itemID);
    }

    public void countMinusActivity()
    {

        buyCount--;
        if (buyCount < 1)
        {
            AlertText.text = "물건은 1개부터 구매 가능하다네~";
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
