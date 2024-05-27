using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecisionInputController : MonoBehaviour
{
    public int max_count = 99;
    public TMP_InputField InputField;
    public void CheckNum()
    {
        int temp_value = int.Parse(InputField.text);
        if (temp_value < 1) InputField.text = "1";
        else if (temp_value > max_count) InputField.text = max_count.ToString();

        ContentsManager.instance.makingFoodsCount = int.Parse(InputField.text);
        ContentsManager.instance.ControlDecision();
    }
}
