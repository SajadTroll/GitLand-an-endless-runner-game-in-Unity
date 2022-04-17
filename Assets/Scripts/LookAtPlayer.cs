using UnityEngine;
using DG.Tweening;

public class LookAtPlayer : MonoBehaviour
{
    Transform player;
    void Start()
    {
        this.Wait(0.3f, () =>
         {
             player = GameObject.FindGameObjectWithTag("Player").transform;
         });
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.DOLookAt(player.position + Vector3.up * 3, 0.5f);

            if (GameManager.Instance.State != GameState.Preparing)
                this.Wait(GameManager.Instance.cameraAnimationsDuration, () =>
                {
                    transform.DOMove(new Vector3(transform.position.x, transform.position.y, player.position.z), 1f);
                });
        }
    }
}
