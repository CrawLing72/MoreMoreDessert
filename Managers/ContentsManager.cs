using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentsManager : MonoBehaviour
{
    public static ContentsManager instance;

    [Header("UI 요소")]
    public GameObject InterObject;
    public GameObject Buttons;
    public GameObject GoodsMakingScreen;
    public GameObject ItemSellingScreen;
    public GameObject ReceipeRNDScreen;
    public GameObject SaboSelectingScreen;

    [Header("GoodsMaking Related")]
    public GameObject MakeBoxPrefab;
    public GameObject Itemlist;
    public GameObject Itemcontent;
    public GameObject InitText;
    public GameObject MakeDecision;
    public GameObject DecisionButton;
    public GameObject MakeReal;

    [Header("GoodsSelling Related")]
    public GameObject SellingBoxPrefab;
    public GameObject GoodsList;
    public GameObject GoodsContent;
    public TMP_Text FoxText;

    [Header("Receipe Research Related")]
    public GameObject MagicCircle;
    public TMP_Text CurrentStar;
    public TMP_InputField MPDescent;
    public GameObject MagicStartButton;

    [Header("Sabotage Related")]
    public GameObject SelectionButtons;
    public GameObject CyberSABO;
    public GameObject SABOPrefab;
    public GameObject SABOList;
    public GameObject SABOgame;
    public GameObject PhysicsSABO;
    public GameObject StarTree;

    [Header("Sabotage Env Related")]
    public bool isSaarlandOn = false;

    [Header ("Recommended not to Modify")]
    public int GoodsCount = 0;
    public int MakingFoodsIndex;

    private bool wasIBOn = true;

    private bool wasInv1stOpened = true;
    private bool wasSell1stOpened = true;
    private bool wasRND1stOpened = true;
    private bool wasSABO1stOpended = true;

    public bool wasMakingFoods = false;
    public int makingFoodsCount = 0;
    public int sellingFoodsCount = 0;
    public int RNDValue = 0;
    public int RNDcount = 0;
    public int MagicColorIndex = 0;
    public int SABOIndex = 0;


    // UNDER : MAKING SCREEN RELATED
    void SettingMakingScreen()
    {
        if (wasInv1stOpened)
        {
            for (int i = 0; i < ItemManager.instance.FoodInfoData.Count; i++)
            {
                Instantiate(MakeBoxPrefab, new Vector2(0, 0), Quaternion.identity, Itemcontent.transform);
            }
        }

        for (int i = 0; i < ItemManager.instance.FoodInfos.Count; i++)
        {
            Transform MakeBoxTrans = Itemcontent.transform.GetChild(i);

            Transform imageTrans = MakeBoxTrans.transform.GetChild(0);
            Transform GDNameTrans = MakeBoxTrans.transform.GetChild(1);
            Transform GDContentTrans = MakeBoxTrans.transform.GetChild(2);
            Transform ButtonTrans = MakeBoxTrans.transform.GetChild(3);

            Image ItemImg = imageTrans.GetComponent<Image>();
            TMP_Text ItemName = GDNameTrans.GetComponent<TMP_Text>();
            TMP_Text ItemText = GDContentTrans.GetComponent<TMP_Text>();
            Button UseButton = ButtonTrans.GetComponent<Button>();
            TMP_Text ButtonText = ButtonTrans.transform.GetChild(0).GetComponent<TMP_Text>();
            MakingButtonController buttonController = ButtonTrans.GetComponent<MakingButtonController>();

            // 0. Get Item Object
            FoodItem TargetItem = ItemManager.instance.FoodInfos[i];

            // 1. Setting Unders
            ItemImg.sprite = ItemManager.instance.FoodSprites[TargetItem.TileID];
            ItemName.text = TargetItem.Name + $"({GameManager.currentGoods[i]}개)";
            ItemText.text = TargetItem.Description;
            buttonController.MakeButtonSettings(i);
        }
        }
    public void LoadMakingScreen()
    {
        if (InterObject.activeSelf == true) // 만약 Interactive Button이 on 상태였다면
        {
            wasIBOn = true;
            InterObject.SetActive(false);
        }
        else wasIBOn = false;

        Buttons.SetActive(false);
        GoodsMakingScreen.SetActive(true);
        GameManager.isFrozen = true;

        if (wasInv1stOpened) // 만약 처음 창을 연 상태라면
        {
            SettingMakingScreen();
            wasInv1stOpened = false;
        }
        else
        {
            SettingMakingScreen();
            ControlDecision();
        }

    }

    public void OutMakingScreen()
    {
        if (wasIBOn)
        {
            InterObject.SetActive(true);
            wasIBOn = false;
        }

        Buttons.SetActive(true);
        GoodsMakingScreen.SetActive(false);
        GameManager.isFrozen = false;
        GameSoundManager.instance.onClick();
    }

    public void SettingMakeDecision()
    {
        InitText.SetActive(false);
        MakeDecision.SetActive(true);

        Transform WindowTrans = MakeDecision.transform;
        FoodItem temp_Food = ItemManager.instance.FoodInfos[MakingFoodsIndex];

        TMP_Text SelectedFoodName = WindowTrans.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text FoodBills = WindowTrans.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text FoodCosts = WindowTrans.GetChild(2).GetComponent<TMP_Text>();
        TMP_Text FoodStars = WindowTrans.GetChild(3).GetComponent<TMP_Text>();

        SelectedFoodName.text = $"선택된 아이템 : {temp_Food.Name.ToString()}";
        FoodBills.text = $"개당 제작비용 : {temp_Food.Bills.ToString()}원";
        FoodCosts.text = $"개당 판매가격 : {temp_Food.Price.ToString()}원";
        FoodStars.text = $"판매 획득평점 : {temp_Food.StarPoints.ToString()}원";
    }

    public void ControlDecision()
    {
        FoodItem temp_Food = ItemManager.instance.FoodInfos[MakingFoodsIndex];
        int total_price = temp_Food.Bills * makingFoodsCount;

        Button _DecisionButton = DecisionButton.GetComponent<Button>();
        TMP_Text ButtonText = DecisionButton.transform.GetChild(0).transform.GetComponent<TMP_Text>();

        if ((GameManager.current_money) - total_price < 0)
        { 
            _DecisionButton.interactable = false;
            ButtonText.text = "생산불가\n(잔고부족)";
        }
        else
        {
            _DecisionButton.interactable = true;
            ButtonText.text = "생산하기";
        }
    }
    public void StartingBaking ()
    { 
        wasMakingFoods = true;

        Itemlist.SetActive(false);
        MakeDecision.SetActive(false);
        MakeReal.SetActive(true);
    }

    public void ResetBaking()
    {
        wasMakingFoods = false;

        Itemlist.SetActive(true);
        MakeDecision.SetActive(true);
        MakeReal.SetActive(false);
        SettingMakingScreen();

        OutMakingScreen();
    }


    // UNDER : SELLING SCREEN RELATED;
    public void SettingSellingScreen()
    {
        if (wasSell1stOpened)
        {
            for (int i = 0; i < ItemManager.instance.FoodInfoData.Count; i++)
            {
                Instantiate(SellingBoxPrefab, new Vector2(0, 0), Quaternion.identity, GoodsContent.transform);
            }
        }

        for (int i = 0; i < ItemManager.instance.FoodInfos.Count; i++)
        {
            Transform MakeBoxTrans = GoodsContent.transform.GetChild(i);

            Transform imageTrans = MakeBoxTrans.transform.GetChild(0);
            Transform GDNameTrans = MakeBoxTrans.transform.GetChild(1);
            Transform GDContentTrans = MakeBoxTrans.transform.GetChild(2);
            Transform ButtonTrans = MakeBoxTrans.transform.GetChild(3);

            Image FoodImg = imageTrans.GetComponent<Image>();
            TMP_Text FoodName = GDNameTrans.GetComponent<TMP_Text>();
            TMP_Text FoodText = GDContentTrans.GetComponent<TMP_Text>();
            Button UseButton = ButtonTrans.GetComponent<Button>();
            TMP_Text ButtonText = ButtonTrans.transform.GetChild(0).GetComponent<TMP_Text>();
            SellingButtonController buttonController = MakeBoxTrans.GetComponent<SellingButtonController>();

            // 0. Get Item Object
            FoodItem TargetItem = ItemManager.instance.FoodInfos[i];

            // 1. Setting Unders
            FoodImg.sprite = ItemManager.instance.FoodSprites[TargetItem.TileID];
            FoodName.text = TargetItem.Name + $"({TargetItem.Price}원)";
            FoodText.text = TargetItem.Description;
            buttonController.MakeButtonSettings(i);
        }
    }

    public void LoadSellingScreen()
    {
        if (InterObject.activeSelf == true) // 만약 Interactive Button이 on 상태였다면
        {
            wasIBOn = true;
            InterObject.SetActive(false);
        }
        else wasIBOn = false;

        Buttons.SetActive(false);
        ItemSellingScreen.SetActive(true);
        GameManager.isFrozen = true;

        SettingSellingScreen();

        if (wasSell1stOpened) // 만약 처음 창을 연 상태라면
        {
            wasSell1stOpened = false;
        }

        CurrentStar.text = $"Current Score :  {string.Format(".3f", GameManager.stars)} / 1000";
    }

    public void OutSellingScreen()
    {
        if (wasIBOn)
        {
            InterObject.SetActive(true);
            wasIBOn = false;
        }

        Buttons.SetActive(true);
        ItemSellingScreen.SetActive(false);
        GameManager.isFrozen = false;
        GameSoundManager.instance.onClick();
    }
   
    // UNDER : RECEIPE RND RELATED;
    public void LoadRNDScreen()
    {
        if (InterObject.activeSelf == true) // 만약 Interactive Button이 on 상태였다면
        {
            wasIBOn = true;
            InterObject.SetActive(false);
        }
        else wasIBOn = false;

        Buttons.SetActive(false);
        ReceipeRNDScreen.SetActive(true);
        GameManager.isFrozen = true;

        Image MCImage = MagicCircle.transform.GetComponent<Image>();
        MCImage.color = Color.white;

        if (wasRND1stOpened) // 만약 처음 창을 연 상태라면
        {
            wasRND1stOpened = false;

        }

        CurrentStar.text = $"Current Score :  {string.Format("{0 : 0.00}", GameManager.stars)} / 1000";
    }

    public void OutRNDScreen()
    {
        if (wasIBOn)
        {
            InterObject.SetActive(true);
            wasIBOn = false;
        }

        Buttons.SetActive(true);
        ReceipeRNDScreen.SetActive(false);
        GameManager.isFrozen = false;
        GameSoundManager.instance.onClick();
    }

    public void CheckRNDButoon()
    {
        int InputedMP = int.Parse(MPDescent.text);
        if (InputedMP <= 0)
        {
            MPDescent.text = "1";
        } else if (InputedMP > GameManager.mp)
        {
            MPDescent.text = GameManager.mp.ToString();
        }

        Button MagicButton = MagicStartButton.transform.GetComponent<Button>();
        Image MagicBTIMG = MagicStartButton.transform.GetComponent<Image>();
        TMP_Text buttonText = MagicStartButton.transform.GetChild(0).transform.GetComponent<TMP_Text>();

        if (GameManager.mp < 1)
        { 
            MagicButton.interactable = false;
            buttonText.text = "MP 부족";
            MagicBTIMG.color = Color.red;
        }
        else
        {
            MagicButton.interactable = true;
            buttonText.text = "평판 강화";
            MagicBTIMG.color = Color.green;

            RNDValue = int.Parse(MPDescent.text);
        }
    }

    public void LetRND()
    {
        Image MCImage = MagicCircle.transform.GetComponent<Image>();
        RNDcount = 0;

        for (int i = 0; i < RNDValue; i++)
        {
            int determine_integer = UnityEngine.Random.Range(0, 1000);
            Debug.Log(determine_integer);
            
            if (determine_integer < 25)
            {
                RNDcount += 1;
                MagicColorIndex = MagicColorIndex < 1 ? 1 : MagicColorIndex;
            } else if (determine_integer < 800 && determine_integer > 795)
            {
                RNDcount += 10;
                MagicColorIndex = MagicColorIndex < 2 ? 2 : MagicColorIndex;
            } else if (determine_integer == 777)
            {
                RNDcount += 100;
                MagicColorIndex = MagicColorIndex < 3 ? 3 : MagicColorIndex;
            }
        }

        switch (MagicColorIndex)
        {
            case 0:
                MCImage.color = Color.white;
                break;
            case 1:
                MCImage.color = Color.red;
                break;
            case 2:
                MCImage.color = Color.green;
                break;
            case 3:
                MCImage.color = Color.yellow;
                break;

        }

        GameSoundManager.instance.PlayEffects("MagicalGacha");

        GameManager.stars *= (1 + (RNDcount/10));
        GameManager.mp -= RNDValue;

        CheckRNDButoon();

        CurrentStar.text = $"Current Score : {string.Format("{0 : 0.00}", GameManager.stars)} / 1000, {string.Format("{0 : 0.00}", (1 + (RNDcount / 10)))}배 UP!";

    }

    // UNDER : SABOTAGE RELATED;
    public void LoadSABOSelecting()
    {
        if (InterObject.activeSelf == true) // 만약 Interactive Button이 on 상태였다면
        {
            wasIBOn = true;
            InterObject.SetActive(false);
        }
        else wasIBOn = false;

        Buttons.SetActive(false);
        SaboSelectingScreen.SetActive(true);
        SelectionButtons.SetActive(true);
        GameManager.isFrozen = true;
    }

    public void OutSABOSelecting()
    {
        if (wasIBOn)
        {
            InterObject.SetActive(true);
            wasIBOn = false;
        }

        Buttons.SetActive(true);
        SaboSelectingScreen.SetActive(false);
        GameManager.isFrozen = false;
        GameSoundManager.instance.onClick();
    }

    public void SettingCyberSABO()
    {
        if (wasSABO1stOpended)
        {
            for (int i = 0; i < ItemManager.instance.SABOData.Count; i++)
            {
                Instantiate(SABOPrefab, new Vector2(0, 0), Quaternion.identity, SABOList.transform);
            }
        }

        for (int i = 0; i < ItemManager.instance.SABOInfos.Count; i++)
        {
            Transform SABOBoxTrans = SABOList.transform.GetChild(i);

            Transform SABONameTrans = SABOBoxTrans.transform.GetChild(1);
            Transform SABOContentTrans = SABOBoxTrans.transform.GetChild(2);
            Transform ButtonTrans = SABOBoxTrans.transform.GetChild(3);

            TMP_Text ItemName = SABONameTrans.GetComponent<TMP_Text>();
            TMP_Text ItemText = SABOContentTrans.GetComponent<TMP_Text>();
            SABOButtonController buttonController = ButtonTrans.GetComponent<SABOButtonController>();

            // 0. Get Item Object
            CyberSABO TargetItem = ItemManager.instance.SABOInfos[i];

            // 1. Setting Unders
            ItemName.text = TargetItem.Name + $"({TargetItem.Price}원, 성공시 {TargetItem.StarPoints}배)";
            ItemText.text = TargetItem.Description;
            buttonController.MakeButtonSettings(i);
        }
    }
    public void LoadCyberSABO()
    {
        SelectionButtons.SetActive(false);
        CyberSABO.SetActive(true);
        
        if (wasSABO1stOpended)
        {
            SettingCyberSABO();
            wasSABO1stOpended = false;
        }
        else
        {
            SettingCyberSABO();
        }
        GameSoundManager.instance.onClick();
    }
    public void OutCyberSABO()
    {
        if (wasIBOn)
        {
            InterObject.SetActive(true);
            wasIBOn = false;
        }

        Buttons.SetActive(true);
        SaboSelectingScreen.SetActive(false);
        GameManager.isFrozen = false;

        CyberSABO.SetActive(false);
        GameSoundManager.instance.onClick();
    }
    public void StartSABO()
    {
        SABOList.SetActive(false);
        SABOgame.SetActive(true);

        SABOGameController SABOCNT = SABOgame.transform.GetComponent<SABOGameController>();
        SABOCNT.SettingGames();
    }
    public void EndSABO()
    {
        SABOgame.SetActive(false);
        SABOList.SetActive(true);
        CyberSABO.SetActive(false);
        SelectionButtons.SetActive(true);

        if (wasIBOn)
        {
            InterObject.SetActive(true);
            wasIBOn = false;
        }

        Buttons.SetActive(true);
        SaboSelectingScreen.SetActive(false);
        GameManager.isFrozen = false;

    }

    public void StartPhysicsSABO()
    {
        StarTree.SetActive(true);
        PhysicsSABO.SetActive(false);
        OutSABOSelecting();
    }



    /* Under : BroadCast by Unity */

    void Awake()
    {
        // SingleTone Pattern Implemention
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
