using UnityEngine;
using System.Collections;

/* Structure des cellules du labyrinthe
 * true si on peut passer, false sinon
 */
struct Cell{
	public bool left;
	public bool right;
	public bool up;
	public bool down;
}

public class MazeGen : MonoBehaviour {
	public Transform[] wallPrefab;
	public int width;
	public int height;
	
	Cell[,] mazeData;

	void Start () {
		// Inititalisation des données du labyrinthe
		mazeData = new Cell[width, height];

		// Génération du labyrinthe
		generateMaze ();

		// Placement de la case de fin
		mazeData [width - 1, height / 2].right = true;

		// generation de la sortie et de l'entrée
		Cell tempCell = new Cell();
		tempCell.left = true;
		tempCell.right = true;
		int pattern = getPatternFromCell(tempCell);

		// Placement de début
		mazeData [0, height / 2].left = true;
		Instantiate(wallPrefab[pattern], new Vector2(-1, height/2), Quaternion.identity);

		// Placement de la fin
		mazeData [0, height / 2].left = true;
		Instantiate(wallPrefab[pattern], new Vector2(width, height/2), Quaternion.identity);


		// Affichage du labyrinthe
		for (int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++){
				pattern = getPatternFromCell(mazeData[i, j]);
				Instantiate(wallPrefab[pattern], new Vector2(i, j), Quaternion.identity);
			}
		}
	}

	/* Les numeros des patterns respectent une conversion binaire
	 * Le  bit le plus faible correspont au booleen up, le second left, puis down, et enfin right.
	 * EX : 1101 correspond au pattern avec juste un mur a gauche
	 */
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


	private bool[,] isVisited; // Les variables statiques n'existant pas en c# on utilise un attribut.
	private void generateMaze(){
		// on initialise les donnees de l'algorithme
		isVisited = new bool[width, height];

		// on lance la generation
		recursiveGeneration (width - 1, height / 2);
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

		// Et enfin, on effectue les visites selon l'ordre defini precedemment
		for(int i = 0; i < 4; i++){
			// haut
			if(order[i] == 0 && y < (height - 1) && (!isVisited[x, y + 1])){
				mazeData[x, y].up = true;
				mazeData[x, y + 1].down = true;
				recursiveGeneration(x, y + 1);
			}
			// gauche
			else if(order[i] == 1 && x > 0 && (!isVisited[x - 1, y])){
				mazeData[x, y].left = true;
				mazeData[x - 1, y].right = true;
				recursiveGeneration(x - 1, y);
			}
			// bas
			else if(order[i] == 2 && y > 0 && (!isVisited[x, y - 1])){
				mazeData[x, y].down = true;
				mazeData[x, y - 1].up = true;
				recursiveGeneration(x, y - 1);
			}
			// droite
			else if(order[i] == 3 && x < (width - 1) && (!isVisited[x + 1, y])){
				mazeData[x, y].right = true;
				mazeData[x + 1, y].left = true;
				recursiveGeneration(x + 1, y);
			}
		}
		//Debug.Log("qsd" + order[0] + order[1] + order[2] + order[3]);
	}
}
