using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TilesPuzzlePiece : MonoBehaviour
{

    [SerializeField] private int index;
    [SerializeField]private int x = 0;
    [SerializeField]private int y = 0;
    [SerializeField] private int xTarget = 0;
    [SerializeField] private int yTarget = 0;
    private Action<int, int> swapFunc = null;
    [SerializeField] private bool correctPosition = false;

    public void Init(int i,int j, int index, Sprite sprite, Action<int, int> swapFunc, int iTarget, int jTarget)
    {
        this.index = index;
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        UpdatePos(i, j);
        this.swapFunc = swapFunc;
        xTarget = iTarget;
        yTarget = jTarget;
    }

    public void ChnageState(bool change)
    {
        correctPosition = change;
    }

    public bool GetCorrectPosition()
    {
        return correctPosition;
    }
    public void UpdatePos(int i, int j)
    {
        x = i;
        y = j;
        StartCoroutine(Move());
        //this.gameObject.transform.localPosition = new Vector2(i, j);
    }

    public bool IsInCorrectPosition()
    {
        if(x==xTarget && y== yTarget)
        {
            return true;
        }
        return false;
    }

    public void SetTargetPosition(int x, int y)
    {
        xTarget = x;
        yTarget = y;
    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        float duration = 0.2f;
        Vector2 start = this.gameObject.transform.localPosition;
        Vector2 end = new Vector2(x*4, y*3);

        while(elapsedTime< duration)
        {
            this.gameObject.transform.localPosition = Vector2.Lerp(start, end, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        this.transform.localPosition = end;
    }
    public bool IsEmpty()
    {
        return index == 16;
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) && swapFunc!= null)
        {
            swapFunc(x, y);
        }
    }

}
