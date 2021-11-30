using UnityEngine;
using System.Collections.Generic;

public class CellGenerator : MonoBehaviour
{
    [SerializeField] private QuestionData[] _questionData; // НЕ МЕНЯТЬ НАЗВАНИЕ

    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _gameCanvas;

    private string _correctAnswer;

    private List<string> _usedWords;

    private Vector2[] _cellPosition;


    public static CellGenerator Instance { get; private set; }


    void Awake()
    {
        PositionAssignment();
        Instance = this;
    }


    void Start()
    {
        RestartGame();
    }


    public void RestartGame()
    {
        _usedWords = new List<string>();
    }


    private void PositionAssignment()
    {
        _cellPosition = new Vector2[9];

        _cellPosition[0] = new Vector2(225, 0);
        _cellPosition[1] = new Vector2(0, 0);
        _cellPosition[2] = new Vector2(-225, 0);

        _cellPosition[3] = new Vector2(225, 225);
        _cellPosition[4] = new Vector2(0, 225);
        _cellPosition[5] = new Vector2(-225, 225);

        _cellPosition[6] = new Vector2(225, -225);
        _cellPosition[7] = new Vector2(0, -225);
        _cellPosition[8] = new Vector2(-225, -225);
    }


    public void GenerateLevel(int currentLevel)
    {
        _questionData.Shuffle();

        if (currentLevel == 0)
        {
            CreateCellsFirstLvl();
        }
        else if (currentLevel == 1)
        {
            CreateCellsSecondLvl();
        }
        else if (currentLevel == 2)
        {
            CreateCellsThirdLvl();
        }
    }


    private void CreateCellsFirstLvl()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject cell = Instantiate(_cellPrefab);
            cell.transform.SetParent(_gameCanvas.transform);

            cell.transform.localPosition = _cellPosition[i];

            var cellScript = cell.GetComponent<Cell>();
            cellScript.SetCell(_questionData[i]);
            cellScript.FirstLevelBounce();
        }

        _correctAnswer = _questionData[Random.Range(0, 3)].Answer;
        _usedWords.Add(CorrectAnswer);
    }


    private void CreateCellsSecondLvl()
    {
        for (int i = 0; i < 6; ++i)
        {
            GameObject cell = Instantiate(_cellPrefab);
            cell.transform.SetParent(_gameCanvas.transform);
            cell.transform.localScale = Vector2.one;

            cell.transform.localPosition = _cellPosition[i];

            cell.GetComponent<Cell>().SetCell(_questionData[i]);
        }

        _correctAnswer = CheckIsWordUnique(6);
        _usedWords.Add(CorrectAnswer);
    }


    private void CreateCellsThirdLvl()
    {
        for (int i = 0; i < 9; ++i)
        {
            GameObject cell = Instantiate(_cellPrefab);
            cell.transform.SetParent(_gameCanvas.transform);
            cell.transform.localScale = Vector2.one;

            cell.transform.localPosition = _cellPosition[i];

            cell.GetComponent<Cell>().SetCell(_questionData[i]);
        }

        _correctAnswer = CheckIsWordUnique(9);
    }


    private string CheckIsWordUnique(int cellCount)
    {
        while (true)
        {
            _correctAnswer = _questionData[Random.Range(0, cellCount)].Answer;

            if (!_usedWords.Contains(_correctAnswer))
            {
                return _correctAnswer;
            }
        }
    }


    public QuestionData[] QuestionData { get => _questionData; }

    public string CorrectAnswer { get => _correctAnswer; set => _correctAnswer = value; }
}


[System.Serializable]
public struct QuestionData
{
    public Sprite CellImage;
    public string Answer;
}
