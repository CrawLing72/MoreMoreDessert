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
            ButtonTXT.text = "사용불가\n(평판"+ItemNORCase.StarDistricted.ToString()+"미만)";
            Button.interactable = false;
        } else if (GameManager.currentItems[ItemNORCase.itemID] == 0)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "사용불가\n(미보유)";
            Button.interactable = false;
        }
        else
        {
            Button.interactable = true;
            ButtonImg.color = Color.green;
            ButtonTXT.text = "사용하기\n(" + GameManager.currentItems[index] + "개 보유중)";
        }
    }

    public void ItemActivity()
    {
        if(GameManager.mp + ItemNORCase.MP < 0)
        {
            GameManager.mp = 0;
            AlertText.color = Color.red;
            AlertText.text = "경고 : MP 부족으로 " + ItemNORCase.Name + "은(는) 사용 불가합니다.";
            return;
        }else if(GameManager.mp + ItemNORCase.MP > GameManager.instance.MP_MAX)
        {
            GameManager.mp = GameManager.instance.MP_MAX;
            AlertText.color = Color.yellow;
            AlertText.text = "주의 : MP 최대치 " + GameManager.instance.MP_MAX + "까지만 충전됩니다.";
        }
        else
        {
            GameManager.mp += ItemNORCase.MP;
        }

        if (GameManager.hp + ItemNORCase.HP < 0)
        {
            GameManager.hp = 0;
            AlertText.color = Color.red;
            AlertText.text = "경고 : HP 부족으로 " + ItemNORCase.Name + "은(는) 사용 불가합니다.";
            return;
        }
        else if (GameManager.hp + ItemNORCase.HP > GameManager.instance.HP_MAX)
        {
            GameManager.hp = GameManager.instance.HP_MAX;
            AlertText.color = Color.yellow;
            AlertText.text = "주의 : HP 최대치 " + GameManager.instance.HP_MAX + "까지만 충전됩니다.";
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
