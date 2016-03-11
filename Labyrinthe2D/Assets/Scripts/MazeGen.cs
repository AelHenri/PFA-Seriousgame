using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Structure des cellules du labyrinthe
 * true si on peut passer, false sinon
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
	public Transform exit;
	public Transform arrow;
	public Transform gradiant;
	//public Transform key;
	public int width;
	public int height;
	public List<Point> deadEnd;
	
	Cell[,] mazeData;
	
	public void SetupScene (int level) {
		// Inititalisation des données du labyrinthe
		width = level*2 + 1;
		height = level*2 + 1;
		mazeData = new Cell[width, height];
		deadEnd = new List<Point>();
		
		// Génération du labyrinthe
		generateMaze ();
		
		// Placement de la case de fin
		mazeData [width - 1, height / 2].right = true;
		
		// generation de la sortie et de l'entrée
		Cell tempCell = new Cell();
		tempCell.left = true;
		tempCell.right = true;
		
		// Placement de début
		mazeData [0, height / 2].left = true;
		printCell (tempCell, -1, height/2);
		Transform gradStart = Instantiate(gradiant, new Vector3(-1, height/2, -5), Quaternion.identity) as Transform;
		gradStart.parent = GameObject.Find("Maze").transform;
		Transform arrow1 = Instantiate(arrow, new Vector3(-2, height/2, -5), Quaternion.identity) as Transform;
		arrow1.parent = GameObject.Find("Maze").transform;
		
		// Placement de la fin
		mazeData [0, height / 2].left = true;
		printCell (tempCell, width, height/2);
		Transform gradEnd = Instantiate(gradiant, new Vector3(width, height/2, -5), new Quaternion(0, 0, 90, 0)) as Transform;
		gradEnd.parent = GameObject.Find("Maze").transform;
		/*Transform arrow2 = Instantiate(arrow, new Vector3(width + 1, height/2, -5), Quaternion.identity) as Transform;
		arrow2.parent = GameObject.Find("Maze").transform;*/

		// Affichage du labyrinthe
		for (int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++){
				printCell(mazeData[i, j], i, j);
			}
		}
		Instantiate(exit, new Vector2(width, height/2), Quaternion.identity);
		LayoutKeys (keys, level);
	}
	
	/* Les numeros des patterns respectent une conversion binaire
	 * Le  bit le plus faible correspont au booleen up, le second left, puis down, et enfin right.
	 * EX : 1101 correspond au pattern avec juste un mur a gauche
	 */
	public void LayoutKeys(GameObject[] keys,int nbkeys){
		for (int i = 0; i < nbkeys; i++) {
			Instantiate(keys[0], new Vector2(1,i), Quaternion.identity);
		}
	
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
	
	// créer et affiche une cellule du labyrinthe
	private void printCell(Cell c, int x, int y){
		int pattern = getPatternFromCell(c);
		Transform newCell = Instantiate(wallPrefab[pattern], new Vector2(x, y), Quaternion.identity) as Transform;
		newCell.parent = GameObject.Find("Maze").transform;
	}
	
	private bool[,] isVisited; // Les variables statiques n'existant pas en c# on utilise un attribut.
	private void generateMaze(){
		// on initialise les donnees de l'algorithme
		isVisited = new bool[width, height];
		
		// on lance la generation
		recursiveGeneration (width - 1, height / 2);
		//for (int i = 0; i < deadEnd.Count; i++)
		//	Debug.Log (deadEnd [i].x + " " + deadEnd [i].y + "\n");
	}
	
	private void recursiveGeneration(int x, int y){
		// on marque la case courante comme visitee
		isVisited [x, y] = true;
		
		/* Generation de l'ordre de visite des cases voisines dans 
		 * un tableau de taille 4 (nombre de direction possible
		 * Pour cela on creer le tableau et on le melange.
		 * Les directions sont 0 haut, 1 gauche, 2 bas, 3 droite
		 */
		// on initialise le tableau
		int [] order = new int[4];
		for(int i = 0; i < 4; i++)
			order[i] = i;
		
		// Puis on mélange l'ordre de visite 3 fois
		for(int j = 0; j < 3; j++){
			for(int i = 0; i < 4; i++){
				int tmp, n = Random.Range(0, 4);
				tmp = order[n];
				order[n] = order[i];
				order[i] = tmp;
			}
		}
		
		// On initialise un booleen qui permettra de savoir si on doit ajouter la case au cul de sac
		bool isDeadEnd = true;
		// Et enfin, on effectue les visites selon l'ordre defini precedemment
		for(int i = 0; i < 4; i++){
			// haut
			if(order[i] == 0 && y < (height - 1) && (!isVisited[x, y + 1])){
				mazeData[x, y].up = true;
				mazeData[x, y + 1].down = true;
				isDeadEnd = false;
				recursiveGeneration(x, y + 1);
			}
			// gauche
			else if(order[i] == 1 && x > 0 && (!isVisited[x - 1, y])){
				mazeData[x, y].left = true;
				mazeData[x - 1, y].right = true;
				isDeadEnd = false;
				recursiveGeneration(x - 1, y);
			}
			// bas
			else if(order[i] == 2 && y > 0 && (!isVisited[x, y - 1])){
				mazeData[x, y].down = true;
				mazeData[x, y - 1].up = true;
				isDeadEnd = false;
				recursiveGeneration(x, y - 1);
			}
			// droite
			else if(order[i] == 3 && x < (width - 1) && (!isVisited[x + 1, y])){
				mazeData[x, y].right = true;
				mazeData[x + 1, y].left = true;
				isDeadEnd = false;
				recursiveGeneration(x + 1, y);
			}
		}
		
		if(isDeadEnd)
			deadEnd.Add(new Point(x, y));
	}
}