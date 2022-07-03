using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public float speed = 4f;
    public GameObject[] tiles;

    float Index = 10;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Destroy(transform.GetChild(0).gameObject, 10f);
        Destroy(transform.GetChild(1).gameObject, 10f);

        for (int i = 2; i < 10; i++)
        {
            CreateRandomTile(Vector3.forward * 10 * i);
        }
    }

    private void Update()
    {
        if (transform.position.z <= Index)
        {
            CreateRandomTile(Vector3.forward * 100);

            Index -= 10f;

            speed += 0.1f;
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed);
    }

    private void CreateRandomTile(Vector3 position)
    {
        int randomNumber = Random.Range(0, tiles.Length);

        GameObject insTile = Instantiate(tiles[randomNumber], transform);
        insTile.transform.position = position;
        Destroy(insTile, 100f);
    }
}
