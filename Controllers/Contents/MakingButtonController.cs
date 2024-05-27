using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MakingButtonController : MonoBehaviour
{
    public FoodItem GoodsNORCase;
    public void MakeButtonSettings(int i)
    {
        GoodsNORCase = ItemManager.instance.FoodInfos[i];
        Image ButtonImg = gameObject.transform.GetComponent<Image>();
        TMP_Text ButtonTXT = gameObject.transform.GetComponentInChildren<TMP_Text>();
        Button Button = gameObject.GetComponent<Button>();

        if (GameManager.stars < GoodsNORCase.StarDistricted)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���úҰ�\n(����" + GoodsNORCase.StarDistricted.ToString() + "�̸�)";
            Button.interactable = false;
        }
        else if (GameManager.hp <= 0) {
            Button.interactable = true;
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���úҰ�\n(HP����)";
        }
        else
        {
            Button.interactable = true;
            ButtonImg.color = Color.blue;
            ButtonTXT.text = "����";
        }

        GameSoundManager.instance.onClick();
    }

    public void SettingIndex()
    {
        ContentsManager.instance.MakingFoodsIndex = GoodsNORCase.itemID;
        ContentsManager.instance.SettingMakeDecision();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
