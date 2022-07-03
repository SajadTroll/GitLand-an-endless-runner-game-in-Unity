using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using DG.Tweening;
using UnityEngine.SceneManagement;
using PlayFab.ClientModels;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] float swipeDuration = 0.5f;

    bool isMoving;

    SwipeListener swipeListener;
    public GameObject losePanel;
    Animator animator;

    private void Awake()
    {
        Instance = this;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void onLeaderboardGet(GetLeaderboardResult obj)
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        swipeListener = GetComponent<SwipeListener>();
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        switch (swipe)
        {
            case "Left":
                if (transform.position.x > -3f && !isMoving)
                {
                    Moved();
                    transform.DOMove(transform.position - Vector3.right * 3, swipeDuration);
                }
                break;
            case "Right":
                if (transform.position.x < 3f && !isMoving)
                {
                    Moved();
                    transform.DOMove(transform.position + Vector3.right * 3, swipeDuration);
                }
                break;
        }
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
            LevelManager.Instance.speed = 0;
            LeaderboardManager.Instance.SendLeaderboard(CoinManager.Instance.coin);
            animator.SetBool("isRunning", false);
            losePanel.SetActive(true);
        }
    }

    void Moved()
    {
        isMoving = true;

        this.Wait(swipeDuration, () =>
        {
            isMoving = false;
        });
    }
}
