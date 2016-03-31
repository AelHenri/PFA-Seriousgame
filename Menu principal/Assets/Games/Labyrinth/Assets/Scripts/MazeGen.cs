using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*cells structure
if we can go : true
else : false
*/
struct Cell{
	public bool left;
	public bool right;
	public bool up;
	public bool down;
}

public struct Point{
	public int x;
	public int y;
	
	public Point(int x, int y){
		this.x = x;
		this.y = y;
	}
	
}

public class MazeGen : MonoBehaviour {
	public Transform[] wallPrefab;
	public GameObject[] keys;
	public GameObject bonus;
	public Transform exit;
	public Transform arrow;
	public Transform gradiant;
	public int width;
	public int height;
	public List<Point> deadEnd;
	public int level;
	
	Cell[,] mazeData;
	
	public void SetupScene (int level) {
		// Inititalisation of the labyrinth data
		this.level = level;
		width =(int) Mathf.Floor(level*2.7f) + 1;
		height = (int)Mathf.Floor (level * 1.5f) + 2;//2 + 1;
		if (level == 4)
			height = height - 1;
		mazeData = new Cell[width, height];
		deadEnd = new List<Point>();
		
		//  labyrinth generation
		generateMaze ();

		mazeData [width - 1, height / 2].right = true;
		
		// beginning and exit placement
		Cell tempCell = new Cell();
		tempCell.left = true;
		tempCell.right = true;
		
		// beginning placement
		mazeData [0, height / 2].left = true;
		printCell (tempCell, -1, height/2);
		Transform gradStart = Instantiate(gradiant, new Vector3(-1, height/2, -5), Quaternion.identity) as Transform;
		gradStart.parent = GameObject.Find("Maze").transform;
		Transform arrow1 = Instantiate(arrow, new Vector3(-2, height/2, -5), Quaternion.identity) as Transform;
		arrow1.parent = GameObject.Find("Maze").transform;
		
		// exit placement
		mazeData [0, height / 2].left = true;
		printCell (tempCell, width, height/2);
		Transform gradEnd = Instantiate(gradiant, new Vector3(width, height/2, -5), new Quaternion(0, 0, 90, 0)) as Transform;
		gradEnd.parent = GameObject.Find("Maze").transform;
		Transform arrow2 = Instantiate(arrow, new Vector3(width + 1, height/2, -5), Quaternion.identity) as Transform;
		arrow2.parent = GameObject.Find("Maze").transform;

		//  labyrinth display
		for (int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++){
				printCell(mazeData[i, j], i, j);
			}
		}
		Instantiate(exit, new Vector2(width, height/2), Quaternion.identity);
		LayoutKeys (keys, level);
		if (this.deadEnd.Count - GameManager.instance.level > 1) {
			GameManager.instance.bonusPresent = true;
			LayoutBonus (bonus);
		}
	}

	/*the patterns numbers respect a binary conversion
	the lefter bit corresponds to up, the second left then down and right
	Example : 1101 has just one wall at left*/
	public void LayoutKeys(GameObject[] keys,int nbkeys){
		for (int i = 0; i < nbkeys; i++) {
			Instantiate(keys[0], new Vector2(1,i), Quaternion.identity);
		}
	
	}

	public void LayoutBonus(GameObject bonus){
		 
			Instantiate(bonus, new Vector2(1,30), Quaternion.identity);

	}

	private int getPatternFromCell(Cell c){
		int r = 0;
		if (!c.right)
			r |= 1;
		r = r << 1;
		if (!c.down)
			r |= 1;
		r = r << 1;
		if (!c.left)
			r |= 1;
		r = r << 1;
		if (!c.up)
			r |= 1;
		
		return r;
	}

	// creates and displays a cell 
		private void printCell(Cell c, int x, int y){
		int folderNum = (level - 1) % 3;

		int pattern = getPatternFromCell(c);

		Transform newCell = Instantiate(wallPrefab[pattern], new Vector2(x, y), Quaternion.identity) as Transform;
		Sprite tempTexture = Resources.Load("Tileset" + folderNum + "/WallImage" + pattern, typeof(Sprite))as Sprite;
		newCell.GetComponent<SpriteRenderer>().sprite = tempTexture;
		newCell.parent = GameObject.Find("Maze").transform;
	}
	
	private bool[,] isVisited; 
	private void generateMaze(){
		// algorithm data initialisation
		isVisited = new bool[width, height];
		
		// on lance la generation
		recursiveGeneration (width - 1, height / 2);
	}
	
	private void recursiveGeneration(int x, int y){
		// current cell is visited
		isVisited [x, y] = true;

		/*genration of order of visit of the neighbour cell
		in a table of 4 raw and lines
		create a table and mix
		the directions are 0 up, 1 left, 2 bottom, 3 right */
		//table initialisation
		int[] order = new int[4];
		for (int i = 0; i < 4; i++)
			order [i] = i;

		// mix the order of visit 3 times

		for (int j = 0; j < 3; j++) {
			for (int i = 0; i < 4; i++) {
				int tmp, n = Random.Range (0, 4);
				tmp = order [n];
				order [n] = order [i];
				order [i] = tmp;
			}
		}
		
		bool isDeadEnd = true;
		// visit by order defined previously
		for (int i = 0; i < 4; i++) {
			// up
			if (order [i] == 0 && y < (height - 1) && (!isVisited [x, y + 1])) {
				mazeData [x, y].up = true;
				mazeData [x, y + 1].down = true;
				isDeadEnd = false;
				recursiveGeneration (x, y + 1);
			}
			// left
			else if (order [i] == 1 && x > 0 && (!isVisited [x - 1, y])) {
				mazeData [x, y].left = true;
				mazeData [x - 1, y].right = true;
				isDeadEnd = false;
				recursiveGeneration (x - 1, y);
			}
			// bottom
			else if (order [i] == 2 && y > 0 && (!isVisited [x, y - 1])) {
				mazeData [x, y].down = true;
				mazeData [x, y - 1].up = true;
				isDeadEnd = false;
				recursiveGeneration (x, y - 1);
			}
			// right
			else if (order [i] == 3 && x < (width - 1) && (!isVisited [x + 1, y])) {
				mazeData [x, y].right = true;
				mazeData [x + 1, y].left = true;
				isDeadEnd = false;
				recursiveGeneration (x + 1, y);
			}
		}
		
		if (isDeadEnd && (x!= 0 || y != GameManager.instance.maze.height / 2) ) {
			deadEnd.Add (new Point (x, y));
		}
	}
}