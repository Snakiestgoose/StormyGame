using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesPuzzle : MonoBehaviour
{
    [SerializeField] private TilesPuzzlePiece picePrefab;
    [SerializeField] private TilesPuzzlePiece[,] pieces = new TilesPuzzlePiece[4, 4];
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int counter =0;
    [SerializeField] GameObject WholeMap;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        int n = 0;
        for (int y = 3; y >= 0; y--)
        {
            for (int x = 0; x < 4; x++)
            {
                TilesPuzzlePiece piece = Instantiate(picePrefab, new Vector2(x, y), Quaternion.identity, this.gameObject.transform);
                piece.Init(x, y, n + 1, sprites[n], ClickToSwap, x, y);
                pieces[x, y] = piece;
                Debug.Log(x + "," + y);
                n++;
            }
        }
        SetCorrectPositions();
        //SetInitialCounter();

        
    }

    public void SetInitialCounter()
    {
        for(int j = 3; j >= 0; j--)
        {
            for(int i = 0; i < 4; i++)
            {
                if (pieces[i, j].IsInCorrectPosition())
                {
                    counter++;
                    pieces[i, j].ChnageState(true);
                }
            }
        }
    }
    public void SetCorrectPositions()
    {
        //first row
        pieces[0, 3].SetTargetPosition(1, 3); //1 ->02
        pieces[1, 3].SetTargetPosition(3, 1); //2 ->12
        pieces[2, 3].SetTargetPosition(0, 1); //3 ->09
        pieces[3, 3].SetTargetPosition(1, 1); //4 ->10
        //second row
        
        pieces[0, 2].SetTargetPosition(0, 0); //5 ->13
        pieces[1, 2].SetTargetPosition(2, 0); //6 ->15
        pieces[2, 2].SetTargetPosition(2, 3); //7 ->03
        pieces[3, 2].SetTargetPosition(3, 2); //8 ->08
        //third row
        pieces[0, 1].SetTargetPosition(2, 1); //9  ->11
        pieces[1, 1].SetTargetPosition(0, 2); //10 ->05
        pieces[2, 1].SetTargetPosition(1, 2); //11 ->06
        pieces[3, 1].SetTargetPosition(0, 3); //12 ->01
        //forth row
        pieces[0, 0].SetTargetPosition(2, 2); //13 ->07
        pieces[1, 0].SetTargetPosition(3, 3); //14 ->04
        pieces[2, 0].SetTargetPosition(1, 0); //15 ->14
        pieces[3, 0].SetTargetPosition(3, 0); //16 ->16
        
        
        
        

    }
    public void ClickToSwap(int x, int y)
    {
        int dx = GetDx(x, y);
        int dy = GetDy(x, y);

        var from = pieces[x, y];
        var target = pieces[x + dx, y + dy];


        //swap this 2 pieces
        pieces[x, y] = target;
        pieces[x + dx, y + dy] = from;

        
        //update pos 2 pieces
        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);

       /* if (from.IsInCorrectPosition())
        {
            counter++;
            from.ChnageState(true);
        }
        else
        {
            if (from.GetCorrectPosition())
            {
                counter--;
                from.ChnageState(false);
            }
        }
        if (counter == 15)
        {
            Debug.Log("Win");
        }*/
    }

    public int GetDx(int x, int y)
    {
        //if right is empty
        if(x<3 && pieces[x + 1, y].IsEmpty())
        {
            return 1;
        }

        if (x > 0 && pieces[x - 1, y].IsEmpty())
        {
            return -1;
        }
        return 0;
    }

    public int GetDy(int x, int y)
    {
        //if top is empty
        if (y < 3 && pieces[x, y+1].IsEmpty())
        {
            return 1;
        }

        if (y > 0 && pieces[x, y-1].IsEmpty())
        {
            return -1;
        }
        return 0;
    }
    public void CheckWinCondition()
    {
        counter = 0;
        for (int j = 3; j >= 0; j--)
        {
            for (int i = 0; i < 4; i++)
            {
                if (pieces[i, j].IsInCorrectPosition())
                {
                    counter++;
                }
            }
        }
        if (counter == 16)
        {
            Debug.Log("Win");
            WholeMap.SetActive(true);
            for (int a = 0; a < transform.childCount; a++)
            {
                transform.GetChild(a).gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("There are " + counter + "pieces well placed");
        }
        
    }
}
