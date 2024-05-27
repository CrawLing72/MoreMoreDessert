using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellingInputController : MonoBehaviour
{
    public SellingButtonController buttonController;
    public int max_count = 99;
    public TMP_InputField InputField;
    public void CheckNum()
    {
        FoodItem tempItem = buttonController.ItemNORCase;
        max_count = GameManager.currentGoods[tempItem.itemID];

        int temp_value = int.Parse(InputField.text);
        if (temp_value < 1) InputField.text = "1";
        else if (temp_value > max_count) InputField.text = max_count.ToString();

        ContentsManager.instance.sellingFoodsCount = int.Parse(InputField.text);
    }
}
