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
            ResultText.text = $"분당 평균 점수 :  {inputedTypeSpeed}점";
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
                ResultText.text = "정답과 일치하지 않아요!";
                ResultText.color = Color.red;
            }
        }
        else
        {
            ResultText.text = $"{GoodsNORCase.AVGTyping}점을 넘지 못했어요.";
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
