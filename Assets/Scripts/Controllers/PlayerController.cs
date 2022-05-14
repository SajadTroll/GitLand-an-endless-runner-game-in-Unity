using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float swipeDuration = 0.5f;

    bool isMoving;

    SwipeListener swipeListener;

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
            print("Lose!");
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
