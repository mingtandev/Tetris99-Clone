using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    public static int WidthLimit = 10;
    public static int HeighLimit = -20;

    public static string Score;
    public static bool isEndGame = false;

    public static Transform[,] grid = new Transform[10 + 1, 20 + 1];

    public static int Lines = 0;
    public static int Level = 1;

    GameObject g;
    GameObject newBlock;
    public GameObject UIEndGame;
    public ParticleSystem part;

    TextMeshProUGUI scoreUI;
    TextMeshProUGUI Text_Level;
    TextMeshProUGUI Text_Lines;

    public Canvas myCanvas;
    // Start is called before the first frame update
    void Start()
    {
        part = GameObject.FindGameObjectWithTag("PTCBreak").GetComponent<ParticleSystem>();
        part.Pause();

        Score = "00000000900";
        scoreUI = GameObject.FindGameObjectWithTag("TextScore").GetComponent<TextMeshProUGUI>();
        scoreUI.text = Score;

        Text_Level = GameObject.FindGameObjectWithTag("TextLevel").GetComponent<TextMeshProUGUI>();
        Text_Lines = GameObject.FindGameObjectWithTag("TextLines").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {


        UpdateInfoLines_Level();

        if (isEndGame == true)
        {
            myCanvas.sortingOrder = 100;
            UIEndGame.SetActive(true);
        }
    }


    public bool CheckInsideGrid(Vector2 Pos)
    {
        if (Pos.x >= 0 && Pos.x <  WidthLimit && Pos.y >= HeighLimit)
            return true;

        return false;
    }

    public void NextBlock(GameObject next)
    {
        //Xoa di newBlock Cu
        Destroy(newBlock);
        g = GameObject.FindGameObjectWithTag("NextBlock");
        newBlock = Instantiate(next, g.transform.position, Quaternion.identity);
        newBlock.transform.localScale = new Vector2(0.6f, 0.6f);
        newBlock.transform.position = new Vector3(newBlock.transform.position.x, newBlock.transform.position.y, newBlock.transform.position.z - 4);
        newBlock.GetComponent<Tetomino>().enabled = false;
        newBlock.transform.parent = g.transform;
    }

    public void ScorePlus(string num2)
    {
        StringBuilder result = new StringBuilder(Score);
        int nho = 0;
        int j = num2.Length-1;
        for(int i = result.Length-1; i >=0; i--)
        {
            int resCol;
            if (j >= 0) resCol = (result[i] - '0') + (num2[j] - '0') + nho;
            else resCol = (int)(result[i] - 48) + nho;
            nho = resCol / 10;
            resCol %= 10;
            j--;
            result[i] = (char)(resCol+48);
        }

        Score = result.ToString();

        scoreUI.text = Score;


    }

    void UpdateInfoLines_Level()
    {
        Text_Level.text = "Level : " +  Level.ToString();
        Text_Lines.text = "Lines : " + Lines.ToString();

    }


    public void PauseGame()
    {
        myCanvas.sortingOrder = 100;
        Time.timeScale = 0;
        FindObjectOfType<Spawner>().CurTetomino.GetComponent<Tetomino>().enabled = false;
    }

    public void Resume()
    {
        myCanvas.sortingOrder = 0;
        Time.timeScale = 1;
        FindObjectOfType<Spawner>().CurTetomino.GetComponent<Tetomino>().enabled = true;

    }

    public void BackToMenu()
    {
        myCanvas.sortingOrder = 0;
        Time.timeScale = 1;
        isEndGame = false;
        SceneManager.LoadScene(0);
    }




}
