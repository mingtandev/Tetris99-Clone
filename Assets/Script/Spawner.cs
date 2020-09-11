using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Temominoes;
    public GameObject CurTetomino;

    int Index_NextBlock;
    void Start()
    {
        Index_NextBlock = Random.Range(0, Temominoes.Length);
        NewTemino();

    }

    void CalcNextBlock()
    {
        Index_NextBlock = Random.Range(0, Temominoes.Length);
    }
    // Update is called once per frame
    public void NewTemino()
    {
        CurTetomino = Temominoes[Index_NextBlock];
        CalcNextBlock();
        GameObject NextTetomino = Temominoes[Index_NextBlock];
        CurTetomino = Instantiate(CurTetomino, transform.position,Quaternion.identity);
        FindObjectOfType<Game>().NextBlock(NextTetomino);

    }


}
