using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance;

    public TextMeshProUGUI popupText;
    public GameObject askNameWindow;
    public TMP_InputField nameInput;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null)
        {
            this.Wait(2f, () => {
                askNameWindow.SetActive(true);
            });
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        popupText.text = "Login Success!";
        this.Wait(2f, () => {
            SceneManager.LoadScene(1);
        });
    }

    public void SubmitButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        askNameWindow.SetActive(false);
        LoadMainMenu();
    }

    void OnError(PlayFabError error)
    {
        popupText.text = "Login Failed! Check your connection and retry";
        print(error.GenerateErrorReport());
    }
}
