using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public Text scoreYouText;
    public Text scoreAIText;

    public GameObject scorePuckPrefab;
    private GameObject scorePuck;

    int scoreYou = 0;
    int scoreAI = 0;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


    }

    void resetTable()
    {
        if (scorePuck != null)
        {
            Destroy(scorePuck);
        }
        scorePuck = Instantiate(scorePuckPrefab, new Vector3(0f,3f,0f), Quaternion.identity) as GameObject;
    }

    // Use this for initialization
    void Start () {
        UpdateScore();
        resetTable();
    }

    void UpdateScore()
    {
        scoreYouText.text = "You:" + scoreYou;
        scoreAIText.text = "AI:" + scoreAI;
    }

    public void ScoredYou()
    {
        scoreYou++;
        resetTable();
        UpdateScore();
    }

    public void ScoredAi()
    {
        scoreAI++;
        resetTable();
        UpdateScore();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
