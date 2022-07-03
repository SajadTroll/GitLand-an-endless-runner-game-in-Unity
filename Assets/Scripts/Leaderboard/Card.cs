using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public TextMeshProUGUI textNumber, textName, textScore;

    public void FillCard(string number, string name, string score)
    {
        textNumber.text = number;
        textName.text = name;
        textScore.text = score;
    }
}
