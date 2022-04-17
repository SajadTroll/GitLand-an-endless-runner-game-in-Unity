using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [SerializeField] TextMeshProUGUI currencyText;
    [SerializeField] AudioClip coinSound;

    //Currency Structure, also autofills ui
    int _cur;
    int currency
    {
        get { return _cur; }
        set
        {
            _cur = value;
            currencyText.text = currency.ToString();
            PlayerPrefs.SetInt("Currency", currency);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currency = PlayerPrefs.GetInt("currency", 0);
    }
}
