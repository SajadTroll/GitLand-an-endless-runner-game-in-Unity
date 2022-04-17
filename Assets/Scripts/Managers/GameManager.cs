using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("GameManager Base")]
    public static GameManager Instance;
    public GameState State;

    [Header("Prefabs")]
    [SerializeField] Material[] skyboxes;
    [SerializeField] AudioClip[] musicClips;
    [SerializeField] AudioClip[] audioClips;


    [Header("Gameobjects")]
    [SerializeField] Transform[] cameraPoints;

    [Header("Variables")]
    [SerializeField] public float cameraAnimationsDuration = 2f;

    [Header("AutoFill Things")]
    GameObject player;
    bool isCameraMoving;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeGameState(GameState.Preparing);
    }

    //start the game when player touches the screen.
    public void StartTheGame()
    {
        ChangeGameState(GameState.Started);
    }

    public void ChangeGameState(GameState newState)
    {
        State = newState;

        //apply the state
        switch (newState)
        {
            case GameState.Preparing:
                HandlePreparing();
                break;

            case GameState.Started:
                HandleStarted();
                break;

            case GameState.Finished:
                HandleFinished();
                break;
        }
    }

    private void HandlePreparing()
    {
        MusicManager.Instance.PlayClip(musicClips[UnityEngine.Random.Range(0, musicClips.Length)]);

        RenderSettings.skybox = skyboxes[UnityEngine.Random.Range(0, skyboxes.Length)];

        CameraGoToPoint(0);
    }
    private void HandleStarted()
    {

    }

    private void HandleFinished()
    {

    }

    //camera animator
    public void CameraGoToPoint(int point)
    {
        Camera.main.transform.DOMove(cameraPoints[point].position, cameraAnimationsDuration);
    }
}

//the states of the game
public enum GameState
{
    Preparing,
    Started,
    Finished
}
