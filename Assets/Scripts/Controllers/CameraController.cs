using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    Transform player;

    private void Start()
    {
        player = PlayerController.Instance.transform;
    }
    
    private void LateUpdate()
    {
        transform.DOMoveX(player.position.x, 0.1f);
    }

}
