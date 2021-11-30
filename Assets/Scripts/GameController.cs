using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text _taskText;

    [SerializeField] private GameObject _particlesPrefab;

    public static GameController Instance { get; private set; }

    private static int _currentLevel;


    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        StartGame();
    }


    public void StartGame()
    {
        CellGenerator.Instance.RestartGame();
        CellGenerator.Instance.GenerateLevel(0);
        _taskText.gameObject.SetActive(true);
        _taskText.DOFade(0, 0);
        _taskText.text = $"Find {CellGenerator.Instance.CorrectAnswer}";

        _taskText.DOFade(1, 0.65f).SetDelay(0.2f);

    }


    public void SpawnWinParticles(Vector2 position)
    {
        GameObject particlesObject = Instantiate(_particlesPrefab, position, Quaternion.identity);

        Destroy(particlesObject, 2f);
    }


    public void GenerateNewLevel()
    {
        _currentLevel++;

        if (_currentLevel < 3)
        {
            ClearGameField();

            CellGenerator.Instance.GenerateLevel(_currentLevel);
            _taskText.text = $"Find {CellGenerator.Instance.CorrectAnswer}";
        }
        else
        {
            RestartPanelController.Instance.GameOver();
            _taskText.gameObject.SetActive(false);
        }
    }


    public void ClearGameField()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");

        for (int i = 0; i < cells.Length; ++i)
        {
            cells[i].SetActive(false);
            Destroy(cells[i], 0.8f);
        }
    }


    public static int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
}
