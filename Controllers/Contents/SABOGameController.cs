using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SABOGameController : MonoBehaviour
{
    public CyberSABO GoodsNORCase;

    public TMP_Text AnswerText;
    public TMP_InputField InputText;
    public TMP_Text ResultText;
    public Button ResetButton;

    private bool isChanging = false;
    private int requiredTypeSpeed = 0;
    private int inputedTypeSpeed = 0;

    private char[] answerArray;
    private char[] currentArray;

    float startTime;
    float currentTime;
    
    public void SettingGames()
    {
        GoodsNORCase = ItemManager.instance.SABOInfos[ContentsManager.instance.SABOIndex];

        AnswerText.text = GoodsNORCase.TypedWords;
        answerArray = AnswerText.text.ToCharArray();

        requiredTypeSpeed = GoodsNORCase.AVGTyping;
        InputText.text = "";

        isChanging = false;
        ResultText.color = Color.green;
    }
    public void WhileOnChanged()
    {
        if (!isChanging)
        {
            startTime = Time.time;
            isChanging = true;
        }
        else
        {
            currentTime = Time.time;
            currentArray = InputText.text.ToCharArray();

            float interval = currentTime - startTime;
            inputedTypeSpeed = (int)((currentArray.Length / interval *60));
            ResultText.text = $"�д� ��� ���� :  {inputedTypeSpeed}��";
            Debug.Log(interval);

        }
    }

    public void EndChange()
    {
        if (inputedTypeSpeed > GoodsNORCase.AVGTyping)
        {
            if (AnswerText.text == InputText.text)
            {
                GameManager.current_money -= GoodsNORCase.Price;
                GameManager.stars *= GoodsNORCase.StarPoints;
                ContentsManager.instance.EndSABO();
            }
            else
            {
                ResultText.text = "����� ��ġ���� �ʾƿ�!";
                ResultText.color = Color.red;
            }
        }
        else
        {
            ResultText.text = $"{GoodsNORCase.AVGTyping}���� ���� ���߾��.";
            ResultText.color = Color.red;
        }
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
