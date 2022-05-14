using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public TextMeshProUGUI coinText;

    int _coi;
    int coin
    {
        get { return _coi; }
        set
        {
            _coi = value;
            coinText.text = coin + "";
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void CoinPlusOne()
    {
        coin++;
    }
}
