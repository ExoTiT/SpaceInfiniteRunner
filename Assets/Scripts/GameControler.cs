using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public static GameControler Instance;

    [SerializeField]
    private GameObject[] GroundsPrefabs;
    [SerializeField]
    private GameObject[] GroundsOnStage;

    [SerializeField]
    private Canvas canvasGame;

    [SerializeField]
    private Canvas canvasGameOver;

    private Text ScoreTxt;
    private Text ScoreGameOver;

    [SerializeField]
    private Image HealthBarRed;
    [SerializeField]
    private Image HealthBarGreen;

    [SerializeField]
    public int Health;

    public int Score;
    public int BestScore;

    private GameObject Ship;
    private float pos;
    private float GroundSize;

    private bool inGame;
    public bool InGame { get { return inGame; } private set { } }

    private int NumberOfGrounds = 4 ;

    private void Awake()
    {
        enabled = false;
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        
        Instance = this;
        Health = 100;
        Score = 0;
        
        Ship = GameObject.Find("Ship");

        GroundsOnStage = new GameObject[NumberOfGrounds];

        ScoreTxt = canvasGame.GetComponent<Text>();
        ScoreGameOver = canvasGameOver.GetComponent<Text>();

    }
    void Start()
    {     
        inGame = true;
    }

    private void GroundConstruction()
    {
        for (int i = 0; i < NumberOfGrounds; i++)
        {
            int n = Random.Range(0, GroundsPrefabs.Length);
            GroundsOnStage[i] = Instantiate(GroundsPrefabs[n]);
        }

        GroundSize = GroundsOnStage[0].GetComponentInChildren<Transform>().Find("Road").localScale.z;
        pos = 0 + GroundSize / 2;

        foreach (var ground in GroundsOnStage)
        {
            ground.transform.position = new Vector3(0, 0, pos);
            pos += GroundSize;
        }
    }

    
    void Update()
    {
        if (inGame)
        {

            for (int i = GroundsOnStage.Length - 1; i >= 0; i--)
            {
                GameObject ground = GroundsOnStage[i];

                if (ground.transform.position.z + GroundSize / 2 < Ship.transform.position.z - 6f)
                {
                    float z = ground.transform.position.z;
                    Destroy(ground);
                    int n = Random.Range(0, GroundsPrefabs.Length);
                    ground = Instantiate(GroundsPrefabs[n]);
                    ground.transform.position = new Vector3(0, 0, z + GroundSize * NumberOfGrounds);
                    GroundsOnStage[i] = ground;
                    Score += 5;
                }
            }

        }
        //ADAPT THE SIZE OF THE HEALTH BAR
        float coef = Health / 100.0f;
        HealthBarGreen.rectTransform.sizeDelta = new Vector2(HealthBarRed.rectTransform.sizeDelta.x * coef, HealthBarRed.rectTransform.sizeDelta.y);

        ScoreTxt.text = "Score : " + Score;
        ScoreGameOver.text = "SCORE : " + Score + "\n" + "BEST SCORE : " + BestScore + "\n" + "\n" + "RETRY PRESS SPACE";

    }

    public void OnStartGame()
    {
        if (Instance != null)
        {
            DestroyGround();
        }
        GroundConstruction();
        inGame = true;
        enabled = true;
        Health = 100;
        Score = 0;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void OnGameOver()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
        }
        inGame = false;
        //enabled = false;
    }

    private void DestroyGround()
    {
        if (Instance != null)
        {
            for (int i = GroundsOnStage.Length - 1; i >= 0; i--)
            {
                Destroy(GroundsOnStage[i]);
            }
        }
    }
}
