using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;


public class Tetomino : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 rotationPoint;
    private float Timefall = 1;  //after 1 second block auto fall
    private float tempFall = 0;
    private bool isFall = true;

    private bool isDown = false;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Falling();
        StopFallingAndSpaw();
        if (CheckEndGame() == true)
        {
            Game.isEndGame = CheckEndGame();
            FindObjectOfType<Spawner>().CurTetomino.GetComponent<Tetomino>().enabled = false;
        }
       
    }
   
    private void Movement()
    {
        if (isFall == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.transform.position += new Vector3(1, 0, 0);
                if (!IsValidPosition(transform))
                {
                    gameObject.transform.position += new Vector3(-1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.transform.position += new Vector3(-1, 0, 0);
                if (!IsValidPosition(transform))
                {
                    gameObject.transform.position += new Vector3(1, 0, 0);
                }

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //RotateAround ho trợ xoay object xung quanh 1 Point
                // TransformPoint = Transform hiện tại + point đó(di chuyển transform đến rotateion Point) (sau đó quay tại đó)
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!IsValidPosition(transform))
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                    return;

                }
                FindObjectOfType<AudioMaster>().PlaySound("Rotate");


            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //int YRun = Mathf.RoundToInt(transform.position.x);
                for(int i = 0; i <20; i++)
                {
                    gameObject.transform.position += new Vector3(0, -1, 0);
                    if (!IsValidPosition(transform))
                    {
                        break;
                    }
                }
                FindObjectOfType<AudioMaster>().PlaySound("Down");

            }
        }
    }

    private void Falling()
    {
        if (isFall)
        {
            if (IsValidPosition(transform))
            {
                if (Time.time - tempFall >= (float)1f/Game.Level)
                {
                    tempFall = Time.time;
                    transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    private void StopFallingAndSpaw()
    {
        //Handle 
        if (!IsValidPosition(transform))
        {
            this.enabled = false;
            isFall = false;
            transform.position += new Vector3(0, 1, 0);
            AddToGrid(transform);
            CheckLines();
                Spawn();


        }
    }
    void Spawn()
    {
        FindObjectOfType<Spawner>().NewTemino();
        Destroy(FindObjectOfType<GhostTetroMino>().GhostTetomino);
        FindObjectOfType<GhostTetroMino>().CreateGhostBlock();
        FindObjectOfType<GhostTetroMino>().GhostBlockDown();
    }
    bool CheckLines()
    {
        bool isMusicPlay = false;
        for(int i = 20; i>=0; i--)
        {
            if (HasLine(i))
            {
                Game.Lines++;
                if (Game.Lines % 20 == 0) Game.Level++;
                DeleteLine(i);
                RowDown(i);
                i++;
                isMusicPlay = true;
                
            }
        }


        if (isMusicPlay) FindObjectOfType<AudioMaster>().PlaySound("FullLine");
        return isMusicPlay;


    }
    bool HasLine(int i)
    {
        for(int j = 0; j < 10; j++)
        {
            if (Game.grid[j, i] == null)
            {
                return false;
            }
        }
        

        return true;
    }

    void DeleteLine(int i)
    {
        for(int j = 0; j<10; j++)
        {
            FindObjectOfType<Game>().part.transform.position = new Vector2(FindObjectOfType<Game>().part.transform.position.x, Game.grid[j, i].transform.position.y);
            FindObjectOfType<Game>().part.Play();
             
            Destroy(Game.grid[j, i].gameObject);
            Game.grid[j, i] = null;
        }
        FindObjectOfType<Game>().ScorePlus("100");
    }


    void RowDown(int row)
    {
        

        for (int i = row; i > 0; i--)
        {
            for (int j = 0; j < 10; j++)
            {
                if (Game.grid[j, i - 1] != null)
                {
                    Game.grid[j, i] = Game.grid[j, i - 1];
                    Game.grid[j, i - 1].transform.position += new Vector3(0, -1, 0);
                    Game.grid[j, i - 1] = null;
                }
            }

        }

    }


    bool IsValidPosition(Transform trans)
    {
        foreach(Transform mino in trans)
        {
            //Lay Round  do position co the tuy y chuyen toa do ve float , kho quan ly
            int RoudX = Mathf.RoundToInt(mino.position.x);
            int RoudY = Mathf.RoundToInt(mino.position.y);
            if (FindObjectOfType<Game>().CheckInsideGrid(new Vector2(RoudX,RoudY)) ==false)
            {
                return false;
            }
            
            if (Game.grid[RoudX, Mathf.Abs(RoudY)] != null)
            {
                return false;
            }
        }
        return true;
    }

 
    void AddToGrid(Transform trans)
    {
        foreach (Transform mino in trans)
        {
            //Lay Round  do position co the tuy y chuyen toa do ve float , kho quan ly
            int RoudX = Mathf.RoundToInt(mino.position.x);
            int RoudY = Mathf.RoundToInt(mino.position.y);

            Game.grid[RoudX, Mathf.Abs(RoudY)] = mino;
        }
    }


    bool CheckEndGame()
    {
        foreach (Transform mino in transform)
        {
            //Lay Round  do position co the tuy y chuyen toa do ve float , kho quan ly
           
            int RoudY = Mathf.RoundToInt(mino.position.y);
            if (RoudY >= 0)
                return true;

        }
        return false;
    }

   

}
