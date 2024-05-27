using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FoodsMakingController : MonoBehaviour
{

    [Header ("Objects")]
    public Slider slider;
    public int startPoint;
    public int endPoint;
    public RectTransform RainykTrnas;
    public TMP_Text indicatingText;

    [Header("Envs")]
    public float Player_interval = 1f;
    public float Resistance_interval = 2f;
    public float minus_degree = 0.1f;
    public float plus_degree = 0.15f;

    private bool isMaking = false;

    private float value = 0f;
    private float accm_time = 0f;
    private float accm_time_d = 0f;
    private int RandomSellingWeight = 0;


    public void StartMaking()
    {
        value = 0f;

        indicatingText.text = "스페이스 키를 연타하세요!";

        isMaking = ContentsManager.instance.wasMakingFoods;

        FoodItem food = ItemManager.instance.FoodInfos[ContentsManager.instance.MakingFoodsIndex];
        float item_time = food.ConsumedTime;

        Resistance_interval /= item_time;
        Player_interval *= (1 + ContentsManager.instance.makingFoodsCount / 150);

        isMaking = true;
    }

    public void Resetting()
    {

        int makingFoodCount = ContentsManager.instance.makingFoodsCount;
        int makingFoodIndex = ContentsManager.instance.MakingFoodsIndex;

        FoodItem food = ItemManager.instance.FoodInfos[makingFoodIndex];

        int total_price = food.Bills * makingFoodCount;

        GameManager.current_money -= total_price;
        GameManager.currentGoods[makingFoodIndex] += makingFoodCount;
        GameManager.hp = GameManager.hp <= 0 ? 0 : GameManager.hp - (int)(UnityEngine.Random.Range(0.1f, 0.7f)*makingFoodCount);
        Debug.Log(GameManager.currentGoods[makingFoodIndex]);
        isMaking = false;
    }
    private void Update()
    {
        if (isMaking)
        {
            accm_time += Time.deltaTime;
            accm_time_d += Time.deltaTime;
  

            if (value >= 1.0f)
             {
                ContentsManager.instance.ResetBaking();
                indicatingText.text = "MAKING COMPLETED!";
                Resetting();
                return;
            }
            else
            {
                if (accm_time > Player_interval && Input.GetKeyDown(KeyCode.Space))
                {
                    accm_time = 0;
                    value += plus_degree;
                    GameSoundManager.instance.PlayEffects("Baking");
                }
                if (accm_time_d > Resistance_interval)
                {
                    accm_time_d = 0;
                    value -= minus_degree;
                }
                slider.value = value;

                float x_index = startPoint + ((endPoint - startPoint) * value) < startPoint ? startPoint : startPoint + ((endPoint - startPoint) * value);
                RainykTrnas.localPosition = new Vector3(x_index, RainykTrnas.localPosition.y, 0);
            }


        }
    }

    private void Awake()
    {
    }

}
