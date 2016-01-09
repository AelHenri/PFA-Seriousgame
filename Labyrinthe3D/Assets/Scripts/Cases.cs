using UnityEngine;
using System.Collections;

public class Cases : MonoBehaviour {
    public GameObject[,] cases;
    public GameObject cell;
    public int rows;
    public int columns;

    public void Awake ()
    {

        cases = new GameObject[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            print(i);    
            for(int j = 0; j < rows; j++)
            {
                cases[i,j] = (GameObject) Instantiate(cell, new Vector3(i-4.5f, 0f, j-4.5f), Quaternion.identity);
            }
        }
    }


}
