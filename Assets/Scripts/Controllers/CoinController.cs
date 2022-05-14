using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        CoinManager.Instance.CoinPlusOne();
        Destroy(gameObject);
    }
}
