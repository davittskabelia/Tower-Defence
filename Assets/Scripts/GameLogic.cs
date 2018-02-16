using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public static GameLogic _instance;

    private float maxHealth = 100;

    public int StartedSkeletonsCount;
    public float SpawnCountIncrease;

    public GameObject GamePanel;
    public GameObject GameOverPanel;
    public Text SkeletonsText;
    public Text WaveText;
    public Text ScoreText;
    public Text GameOverScoreText;
    public Text GameOverWaveText;

    public float MaxSpawnCooldown;
    public GameObject Skeleton;

    public List<GameObject> AttackLocations;

    private float WaveCooldown = 10;
    private int currentWave = 0;


    private int skeletonsLeft;

    public int SkeletonsLeft
    {
        get { return skeletonsLeft; }
        set
        {
            skeletonsLeft = value;
            SkeletonsText.text = value.ToString();
            if (value == 0)
            {
                StartNewWave(StartedSkeletonsCount = (int) (StartedSkeletonsCount * SpawnCountIncrease), WaveCooldown);
            }
        }
    }


    private int score;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            ScoreText.text = value.ToString();
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        StartNewWave(StartedSkeletonsCount, 0);
    }

    void StartNewWave(int count, float cooldown)
    {
        SpawnController._instance.Spawn(count, MaxSpawnCooldown, Skeleton, cooldown);
        //WallController.Instance.Heal(maxHealth);
        SkeletonsLeft = count;
        WaveText.text = (++currentWave).ToString();
    }


    public void OnGameOver()
    {
        GamePanel.SetActive(false);
        GameOverPanel.SetActive(true);
        GameOverScoreText.text = score.ToString();
        GameOverWaveText.text = (currentWave - 1).ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}