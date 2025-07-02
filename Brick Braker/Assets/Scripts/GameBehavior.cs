using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    public static int BrickLevel;

    public int _playerScore = 0;
    public Utilities.GameState CurrentState;
    
    int _scoreMultiplier;
    public BallBehavior ball;
    public BrickBehavior brick;


    int numberOfBricks = 7;

    public float PaddleSpeed = 5.0f;
    public float InitBallForce = 5.0f;
    public float _ballSpeedIncrement = 1.1f;
    public float _paddleInfluence = 0.4f;

    public bool Win;

    public bool levelCleared = false;

    [SerializeField] TextMeshProUGUI _scoreGUI;
    [SerializeField] TextMeshProUGUI _youLoseTitle;

    [SerializeField] GameObject Brick;
    [SerializeField] TMP_Text _messagesGUI;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _youLoseTitle.gameObject.SetActive(false);
        BrickLevel = 2;
    }

    void Start()
    {
        _scoreMultiplier = 1;
        CurrentState = Utilities.GameState.Play;
        _messagesGUI.enabled = false;
    }

    public void ScorePoint()
    {
        if (ball.DidHitPaddleLast)
            _scoreMultiplier = 1;

        else
            _scoreMultiplier++;

        Score += 1 * _scoreMultiplier;
    }

    public int Score
    {
        get => _playerScore;

        set
        {
            _playerScore = value;
            _scoreGUI.text = Score.ToString();
        }
    }

    void Update()
    {
        if (!levelCleared && GameObject.FindGameObjectsWithTag("Brick").Length == 0)
        {
            levelCleared = true;
            NextLevel();
        }
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (CurrentState)
            {
                case Utilities.GameState.Play:
                    CurrentState = Utilities.GameState.Pause;
                    _messagesGUI.enabled = true;
                    break;

                case Utilities.GameState.Pause:
                    CurrentState = Utilities.GameState.Play;
                    _messagesGUI.enabled = false;
                    break;

                default:
                    break;

            }
        }
    }
    void NextLevel()
    {

        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)

        {
            Debug.Log("Generating more bricks!");
            BrickLevel++;
            numberOfBricks++;
            BallBehavior._ballSpeedIncrement += 0.1f;
            PaddleMovement.Speed += 0.5f;
            for (int i = 0; i < numberOfBricks; i++)
            {
                Vector3 randomPos = new Vector3(
                Random.Range(-6.57f, 0.215f),
                Random.Range(-2.3f, 4.26f),
                0f
                );

                Instantiate(Brick, randomPos, Quaternion.identity);
            }
            levelCleared = false;
        }
    }
    
    public void YouLose()
    {
        Debug.Log("You Lose");
        _youLoseTitle.gameObject.SetActive(true);
    }
}

