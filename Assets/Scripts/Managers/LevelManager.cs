using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float speed = 4f;
    public GameObject[] tiles;

    float Index = 10;

    private void Start()
    {
        Destroy(transform.GetChild(0).gameObject, 5f);
        Destroy(transform.GetChild(1).gameObject, 5f);
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (transform.position.z <= Index)
        {
            CreateRandomTile(Vector3.forward * 20);

            Index -= 10f;
        }
    }

    private void CreateRandomTile(Vector3 position)
    {
        int randomNumber = Random.Range(0, tiles.Length);

        GameObject insTile = Instantiate(tiles[randomNumber], transform);
        insTile.transform.position = position;
        Destroy(insTile, 10f);
    }
}
