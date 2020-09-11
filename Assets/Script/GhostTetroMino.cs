using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTetroMino : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GhostTetomino;
    //private Sprite GhostBlock;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    public void Movement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Destroy(GhostTetomino);
            CreateGhostBlock();
            GhostTetomino.transform.position += new Vector3(1, 0, 0);
            if (!IsValidPosition(GhostTetomino.transform))
            {
                GhostTetomino.transform.position += new Vector3(-1, 0, 0);
            }
            GhostBlockDown();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Destroy(GhostTetomino);
            CreateGhostBlock();
            GhostTetomino.transform.position += new Vector3(-1, 0, 0);
            if (!IsValidPosition(GhostTetomino.transform))
            {
                GhostTetomino.transform.position += new Vector3(1, 0, 0);
            }
            GhostBlockDown();

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            //RotateAround ho trợ xoay object xung quanh 1 Point
            // TransformPoint = Transform hiện tại + point đó(di chuyển transform đến rotateion Point) (sau đó quay tại đó)
            Destroy(GhostTetomino);
            CreateGhostBlock();
            Vector3 rotationPoint = FindObjectOfType<Spawner>().CurTetomino.GetComponent<Tetomino>().rotationPoint;

            GhostTetomino.transform.RotateAround(GhostTetomino.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!IsValidPosition(GhostTetomino.transform))
            {
                GhostTetomino.transform.RotateAround(GhostTetomino.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
            GhostBlockDown();

        }
    }
    public void CreateGhostBlock()
    {

        GhostTetomino = Instantiate(FindObjectOfType<Spawner>().CurTetomino, FindObjectOfType<Spawner>().CurTetomino.transform.position, FindObjectOfType<Spawner>().CurTetomino.transform.rotation);
        GhostTetomino.GetComponent<Tetomino>().enabled = false;
        SpriteRenderer[] sprites = GhostTetomino.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {

            sprites[i].sprite = Resources.Load<Sprite>("Ghost");
        }
    }
    public void GhostBlockDown()
    {

        for (int i = 0; i < 20; i++)
        {
            GhostTetomino.transform.position += new Vector3(0, -1, 0);
            if (!IsValidPosition(GhostTetomino.transform))
            {
                GhostTetomino.transform.position += new Vector3(0, 1, 0);
                break;
            }
        }
    }
    bool IsValidPosition(Transform trans)
    {
        foreach (Transform mino in trans)
        {
            //Lay Round  do position co the tuy y chuyen toa do ve float , kho quan ly
            int RoudX = Mathf.RoundToInt(mino.position.x);
            int RoudY = Mathf.RoundToInt(mino.position.y);
            if (FindObjectOfType<Game>().CheckInsideGrid(new Vector2(RoudX, RoudY)) == false)
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

}
