using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public TextMeshProUGUI coinText;

    public AudioClip coinSound;

    int _coi;
    public int coin
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
        AudioManager.Instance.PlayClip(coinSound);
    }
}
