using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SABOButtonController : MonoBehaviour
{
    public CyberSABO GoodsNORCase;
    public void MakeButtonSettings(int i)
    {
        GoodsNORCase = ItemManager.instance.SABOInfos[i];
        Image ButtonImg = gameObject.transform.GetComponent<Image>();
        TMP_Text ButtonTXT = gameObject.transform.GetComponentInChildren<TMP_Text>();
        Button Button = gameObject.GetComponent<Button>();

        if (GameManager.stars < GoodsNORCase.StarDistricted)
        {
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���� �Ұ�\n(����" + GoodsNORCase.StarDistricted.ToString() + "�̸�)";
            Button.interactable = false;
        }
        else if (GameManager.current_money < GoodsNORCase.Price) {
            Button.interactable = false;
            ButtonImg.color = Color.red;
            ButtonTXT.text = "���úҰ�\n(�ڱ� ����)";
        }
        else
        {
            Button.interactable = true;
            ButtonImg.color = Color.blue;
            ButtonTXT.text = "�����ϱ�";
        }
    }

    public void SettingIndex()
    {
        ContentsManager.instance.SABOIndex = GoodsNORCase.SABOID;
        ContentsManager.instance.StartSABO();
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
