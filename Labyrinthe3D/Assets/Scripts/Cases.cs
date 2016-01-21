using UnityEngine;
using System.Collections.Generic;

public class Cases : MonoBehaviour {
    public enum Direction { Null, W, N, E, S };
    public enum Wall { Null, R, D, RD}//Adding/removing right is +1/-1, adding/removing down is +2/-2

    public GameObject[,] cases;//table of cells
    public GameObject cell;//cell object (is drawn)
    public GameObject rightWall;//reference to right wall
    public GameObject downWall;//reference to down wall

    public int rows = 10;
    public int columns = 10;
    //Data used as information on labyrinth generated
    public Vector2 entrance;//Position of entrance
    public Vector2 exit;//Position of exit
    public List<Vector2> deadends;//Position of dead-ends(pickups and entrance/exits)

    public Wall[,] walls;//walls state (0 means no walls)
    public bool[,] visited;//Visited cell

    public void Awake()//called as constructor
    {
        //rows = 10;
        //columns = 10;
        entrance = new Vector2(-1, -1);
        exit = new Vector2(-1, -1);
        cases = new GameObject[columns, rows];
        walls = new Wall[columns, rows];
        deadends = new List<Vector2>();
        visited = new bool[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                visited[i, j] = false;
                walls[i, j] = Wall.RD;
            }
        }
        //Instantiate cells
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                cases[i, j] = (GameObject) Instantiate(cell, new Vector3(i - 4.5f, 0f, j - 4.5f), Quaternion.identity);
            }
        }
    }
    public bool isDestroyable(Vector2 vect, Direction dir)
    {
        if (dir == Direction.Null)
        {
            return false;
        }
        //does not check if already visited
        /*//check if wall already destroyed //Never happens?
        int check = 0;
        if (dir == Direction.W || dir == Direction.E)
        {
            check = 1;
        }
        if (dir == Direction.N || dir == Direction.S)
        {
            check = 2;
        }
        if (0 >= (walls[(int) vect.x, (int) vect.y]-check))
        {   
            return false;
        }*/
        print("isDestroyable:not already destroyed :x"+vect.x+" y: "+ vect.y+" dir: "+dir);
        Vector2 v = vect;
        switch (dir)
        {
            case Direction.W:
                v.x += -1;
                break;
            case Direction.N:
                v.y += 1;
                break;
            case Direction.E:
                v.x += 1;
                break;
            case Direction.S:
                v.y += -1;
                break;
            default:
                break;
        }
        //Labyrinth limits
        if (v.x < 0 || v.x >= columns || v.y < 0 || v.y >= rows)
        {
            return false;
        }
        else
        {
            print("isDestroyable:not outside limits :x" + vect.x + " y: " + vect.y + " dir: " + dir);
            //Visited cell ahead
            if (visited[(int)v.x, (int)v.y] == true)
            {
                return false;
            }
            else
            {
                print("isDestroyable:not already visited = good :x" + vect.x + " y: " + vect.y + " dir: " + dir);
                return true;
            }
        }
    }

    public bool isDestroyable_visited(Vector2 vect, Direction dir)
    {
        if (dir == Direction.Null)
        {
            return false;
        }
        //does not check if already visited
        /*//check if wall already destroyed //Never happens?
        int check = 0;
        if (dir == Direction.W || dir == Direction.E)
        {
            check = 1;
        }
        if (dir == Direction.N || dir == Direction.S)
        {
            check = 2;
        }
        if (0 >= (walls[(int) vect.x, (int) vect.y]-check))
        {   
            return false;
        }*/
        print("isDestroyable:not already destroyed :x" + vect.x + " y: " + vect.y + " dir: " + dir);
        Vector2 v = vect;
        switch (dir)
        {
            case Direction.W:
                v.x += -1;
                break;
            case Direction.N:
                v.y += 1;
                break;
            case Direction.E:
                v.x += 1;
                break;
            case Direction.S:
                v.y += -1;
                break;
            default:
                break;
        }
        //Labyrinth limits
        if (v.x < 0 || v.x >= columns || v.y < 0 || v.y >= rows)
        {
            return false;
        }
        else
        {
            print("isDestroyable:not outside limits :x" + vect.x + " y: " + vect.y + " dir: " + dir);
            //Visited cell ahead
            if (visited[(int)v.x, (int)v.y] == true)
            {
                print("isDestroyable:already visited = good :x" + vect.x + " y: " + vect.y + " dir: " + dir);
                return true;
            }
            else
            {
                print("isDestroyable: not visited = not good :x" + vect.x + " y: " + vect.y + " dir: " + dir);
                return false;
            }
        }
    }

    public void Pick()//CHOICE
    {
        //Pick an entrance from the list of deadends
        foreach (Vector2 i in deadends)
        {
            if(i.x == 0 || i.x == rows-1)
            {
                if(i.y == 0 || i.y == columns-1)
                {
                    entrance = i;
                    deadends.Remove(i);
                }
            }
        }

        //Pick an exit from the list of deadends
        foreach (Vector2 i in deadends)
        {
            if (i.x == 0 || i.x == rows-1)
            {
                if (i.y == 0 || i.y == columns-1)
                {
                    entrance = i;
                    deadends.Remove(i);
                }
            }
        }
        if(entrance == new Vector2(-1,-1))
        {
            foreach(Vector2 i in deadends)
            {
                //Another choice
                print(i);
            }
        }
    }

    public void replaceWall(Vector2 v, Direction dir)
    {
        Destroy(cases[(int)v.x, (int)v.y]);
        print("  Destroyed cases[:" + (int)v.x + " , " + (int)v.y + " ]");
        if(dir == Direction.W || dir == Direction.E)
        {
            if (walls[(int)v.x, (int)v.y] == Wall.RD)
                walls[(int)v.x, (int)v.y] = Wall.D;
            else
            {
                if (walls[(int)v.x, (int)v.y] == Wall.R)
                    walls[(int)v.x, (int)v.y] = Wall.Null;
            }
        }
        if (dir == Direction.N || dir == Direction.S)
        {
            if (walls[(int)v.x, (int)v.y] == Wall.RD)
                walls[(int)v.x, (int)v.y] = Wall.R;
            else
            {
                if (walls[(int)v.x, (int)v.y] == Wall.D)
                    walls[(int)v.x, (int)v.y] = Wall.Null;
            }
        }
        print("   Case state = " + walls[(int)v.x, (int)v.y]);
        if (Wall.D == walls[(int)v.x, (int)v.y])
        {
            cases[(int)v.x, (int)v.y] = (GameObject)Instantiate(downWall, new Vector3(v.x - 4.5f, 0f, v.y - 5f), Quaternion.identity);
            print("    Rebuilt case: S Wall:" + walls[(int)v.x, (int)v.y]+"at x:"+ v.x+"-4.5"+"z:"+ v.y+"- 5");
        }
        else
        {
            if (Wall.R == walls[(int)v.x, (int)v.y])
            {
                cases[(int)v.x, (int)v.y] = (GameObject)Instantiate(rightWall, new Vector3(v.x - 4f, 0f, v.y - 4.5f), Quaternion.identity);
                print("    Rebuild case: E Wall:" + walls[(int)v.x, (int)v.y] + "at x:" + v.x+"-5" + "z:" + v.y + "- 4.5");
            }
            else
            {
                print("    No rebuilt: walls[" + v.x + " , " + v.y + " = " + walls[(int)v.x, (int)v.y]);
            }
        }
            
    }

    public Vector3 scan()
    {
        print("Beginning scan");
        Direction dir = Direction.Null;
        for (int i = 0; i < rows; i++)//Down-leftmost first CHOICE
        {
            for(int j = 0; j < columns; j++)
            {
                if (!visited[i, j])
                {
                    for (int k = 1; k < 5; k++)
                    {
                        switch ((Direction) k)//CHOICE of direction : W then N then E then S(see at the top, not order below
                        {
                            case Direction.W:
                                if (i - 1 >= 0 && visited[i - 1, j] == true )
                                {
                                    dir = Direction.W;
                                    return new Vector3(i, j, (float) dir);
                                }
                                break;
                            case Direction.N:
                                if (j + 1 < rows && visited[i, j + 1] == true )
                                {
                                    dir = Direction.N;
                                    return new Vector3(i, j, (float)dir);
                                }
                                break;
                            case Direction.E:
                                if (i + 1 < columns && visited[i + 1, j] == true )
                                {
                                    dir = Direction.E;
                                    return new Vector3(i, j, (float)dir);
                                }
                                break;
                            case Direction.S:
                                if (j - 1 >= 0 && visited[i, j - 1] == true )
                                {
                                    dir = Direction.S;
                                    return new Vector3(i, j, (float)dir);
                                }
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
        }
        return new Vector3(-1, -1, (float) dir);
    }

    public void generate()
    {  
        //Hunt and kill algorithm
        //Pick a random first cell
        Vector3 c = new Vector3(Random.Range(0, columns), Random.Range(0, rows), (float) Direction.Null);// CHOICE ; z is the direction when scanning
        deadends.Add(c);
        int count = 0;
        Direction dir = Direction.Null;
        List<Direction> list = new List<Direction>();
        list.Add(Direction.W);
        list.Add(Direction.N);
        list.Add(Direction.E);
        list.Add(Direction.S);
        bool noDirection = false;
        do
        {

        while (!noDirection)
            {
            //Set cell to visited
            visited[(int)c.x, (int)c.y] = true;
            //Pick a random direction
            dir = Direction.Null;
            list = new List<Direction>();
            list.Add(Direction.W);
            list.Add(Direction.N);
            list.Add(Direction.E);
            list.Add(Direction.S);
            print("List filled" + "x:" + c.x + "y:" + c.y);
            count = 0;
            print("count reinitialise" + "x:" + c.x + "y:" + c.y);
            print("count avant " + count + "x:" + c.x + "y:" + c.y);
            
            while (!isDestroyable(c, dir) && count <= 3)
                {
                    print("count " + count);
                    dir = list[Random.Range(0, 4-count)];
                    print("List: removing " + dir);
                    list.Remove(dir);  
                    count++;
                    print(" isDestroyable(x: " + c.x + " y: " + c.y + " z: " + c.z + " dir: " + dir + ")");
                }//Find a destroyable wall (cell ahead not visited and wall not already destroyed)

                //print("isDestroyable(x: " + c.x + " y: " + c.y + " z: " + c.z + " dir: " + dir+") = "+isDestroyable(c, dir));
                print("sortie count " + count);
                if (isDestroyable(c, dir))
                {

                    //Destroy the corresponding wall and move attention to cell onwards
                    switch (dir)
                    {
                        case Direction.W:
                            c.x += -1;
                            replaceWall(c, Direction.E);
                            break;
                        case Direction.N:
                            c.y += 1;
                            replaceWall(c, Direction.S);
                            break;
                        case Direction.E:
                            replaceWall(c, Direction.E);
                            c.x += 1;
                            break;
                        case Direction.S:
                            replaceWall(c, Direction.S);
                            c.y += -1;
                            break;
                        default:
                            break;
                    }
                
                }
                else
            {
                noDirection = true;
            }
            
        }//Repeat until no directions possible
            //Add dead end to table
            deadends.Add(new Vector2((int)c.x, (int)c.y));
            //Scan first row and choose a cell (never chosen) which is adjacent to a cell already visited (can determine labyrinth type)
            c = scan();
            print("scan gives c x:" + c.x + "y:" + c.y);
            noDirection = false;
            //Link current cell to present labyrinth RANDOMLY ; CHOICE
            if ((Vector2)c != new Vector2(-1, -1))
            {
                List<Direction> list2 = new List<Direction>();
                list2.Add(Direction.W);
                list2.Add(Direction.N);
                list2.Add(Direction.E);
                list2.Add(Direction.S);
                int count2 = 0;
                int a = 0;
                while (count2 != 4)
                {
                    if (!isDestroyable_visited(c, list2[a]))
                    {
                        list2.Remove(list2[a]);
                    }
                    else
                    {
                        a++;
                    }
                    count2++;
                }
                int rdir = Random.Range(0, list2.Count); // CHOICE
                switch (list2[rdir])//Remove targeted cell if was deadend
                {
                    case Direction.W:
                        c.x += -1;
                        replaceWall(c, Direction.E);
                        deadends.Remove(new Vector2((int)c.x, (int)c.y));
                        c.x += 1;
                        break;
                    case Direction.N:
                        c.y += 1;
                        replaceWall(c, Direction.S);
                        deadends.Remove(new Vector2((int)c.x, (int)c.y));
                        c.y += -1;
                        break;
                    case Direction.E:
                        replaceWall(c, Direction.E);
                        c.x += 1;
                        deadends.Remove(new Vector2((int)c.x, (int)c.y));
                        c.x += -1;
                        break;
                    case Direction.S:
                        replaceWall(c, Direction.S);
                        c.y += -1;
                        deadends.Remove(new Vector2((int)c.x, (int)c.y));
                        c.y += 1;
                        break;
                    default:
                        break;
                }
                
                

            }
            

        } while ((Vector2) c != new Vector2(-1, -1));//Restart the above process with the chosen cell until the scan gives no possible cell
    }
    
    public void Start()
    {
        generate();
    }
}

