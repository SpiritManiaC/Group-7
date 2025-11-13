using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[InfoHeaderClass("Drag this object into the scene. Attach the score text object into the inspector below")]
public class ScoreManager : MonoBehaviour
{   
    public static ScoreManager Instance { get; private set; }

    public UnityEvent OnAddScore;
    
    public List<GameObject> foodLeft;
    public List<GameObject> enemiesLeft;

    [SerializeField, Header("Text UI for score + highscore display")]
    private TextMeshProUGUI playerScoreText;
    [SerializeField]private TextMeshProUGUI enemyScoreText;

    
    
    private int PlayerScore
    {
        get { return _playerScore;} set { _playerScore = value; }
    }
    private int EnemyScore    {
        get { return _enemyScore;} set { _enemyScore = value; }
    }

    public int _playerScore;
    public int _enemyScore;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateUI();
    }

    public void UpdateFoodAndEnemyList()
    {
        foodLeft = new List<GameObject>(GameObject.FindGameObjectsWithTag("Food"));
        enemiesLeft = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public void AddPlayerScore(int value)
    {
        OnAddScore?.Invoke();
        PlayerScore += value;
        UpdateUI();
        if (foodLeft.Count == 0)
        { LossOrWin(); }
    }
    public void AddEnemyScore(int value)
    {
        OnAddScore?.Invoke();
        EnemyScore += value;
        UpdateUI();
        if (foodLeft.Count == 0)
        { LossOrWin(); }
    }

    public void ResetScore()
    {
        PlayerScore = 0;
        UpdateUI();
    }   

    public void LossOrWin()
    {
        if (_playerScore < _enemyScore)
        { GameStateManager.Instance.LoseGame(); }
        else if (_playerScore > _enemyScore)
        { GameStateManager.Instance.FruitEaten(); }
        if (enemiesLeft.Count == 0)
        { GameStateManager.Instance.WinGame(); }
    }

    private void UpdateUI()
    {
        if (playerScoreText != null)
        { playerScoreText.text = $"Eaten: {PlayerScore}"; }
        if (enemyScoreText != null)
        { enemyScoreText.text = $"Lost: {EnemyScore}"; }
    }
}
