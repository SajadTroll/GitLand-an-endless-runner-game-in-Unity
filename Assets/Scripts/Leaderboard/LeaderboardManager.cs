using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    public GameObject cardPrefab;
    public Transform cardsParentTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        /*
        foreach (var item in cardsParentTransform.GetComponentsInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
        */
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate
                {
                    StatisticName = "CoinScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "CoinScore",
            StartPosition = 0,
            MaxResultsCount = 50
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    public void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            GameObject cardInstance = Instantiate(cardPrefab, cardsParentTransform);
            Card card = cardInstance.GetComponent<Card>();
            card.FillCard("#" + (item.Position + 1), item.DisplayName.ToString(), item.StatValue.ToString());
        }
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        print("successfully updated leaderboard!");
        this.Wait(1f,()=> {
            GetLeaderboard();
        });
    }

    void OnError(PlayFabError error)
    {
        print(error.GenerateErrorReport());
        SendLeaderboard(CoinManager.Instance.coin);
    }
}
