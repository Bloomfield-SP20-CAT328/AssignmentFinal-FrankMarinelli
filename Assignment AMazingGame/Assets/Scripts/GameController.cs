using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	static private GameController instance = null;
    static public GameController Instance
	{
		get
		{
            if (instance == null)
			{
				instance = FindObjectOfType<GameController>();
                if (instance == null)
				{
					GameObject go = new GameObject();
					go.name = "GameController";
					instance = go.AddComponent<GameController>();
				}
			}
			return instance;
		}
	}



	// Should be odd, but hey, our Maze class with fix that!
	public int mazeWidth = 50;
	public int mazeHeight = 50;
	// Since we should also control this, let's make these class variables	
	public float mazeComplexity = 0.75f;
	public float mazeDensity = 0.75f;

	public InputField widthText;
	public InputField heightText;
	public Slider complexitySlider;
	public Slider densitySlider;
	public Text complexityText;
	public Text densityText;
	public int enemyAICounter;
	private int enemyAIMax = 3;

	public Maze maze = new Maze();
	GameObject mazeBase;

    void Awake()
	{
        if (instance != null)
		{
			Destroy(this.gameObject);
		} else
		{
			instance = this;
		}
	}

	public void Start () {
		UpdateDensityText();
		UpdateComplexityText();
		mazeBase = new GameObject("Maze GameObject");
		GenerateMaze();
		DrawMaze();
	}

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			GenerateMaze();
			PlaceThePlayer();
		}

        if(enemyAICounter < enemyAIMax)
		{
			if (Input.GetKeyDown("1"))
			{
				enemyAICounter++;
				PlaceTheEnemy();
			}

			if (Input.GetKeyDown("2"))
			{
				enemyAICounter++;
				PlaceTheEnemyTwo();
			}

			if (Input.GetKeyDown("3"))
			{
				enemyAICounter++;
				PlaceTheEnemyThree();
			}
		}
	}

	#region Update UI Values
	public void UpdateDensityText() { densityText.text = "Density: " + densitySlider.value.ToString("00.0000"); }

	public void UpdateComplexityText() { complexityText.text = "Complexity: " + complexitySlider.value.ToString("00.0000"); }

	public void UpdateWidthValue() { 
		mazeWidth = maze.Width;
		widthText.text = mazeWidth.ToString(); 
	}

	public void UpdateHeightValue() { 
		mazeHeight = maze.Height;
		Point a = new Point();
		heightText.text = mazeHeight.ToString(); 
	}
	#endregion

	public void RegenerateMaze_ClickHandler() {
		if(widthText.text=="" || heightText.text=="") {
			// @TODO Should alert you, but who has time for that!
			return;
		}

		int width = int.Parse(widthText.text);
		int height = int.Parse(heightText.text);
		float complexity = complexitySlider.value;
		float density = densitySlider.value;

		// Check if valid?...
		mazeWidth = width;
		mazeHeight = height;
		mazeComplexity = complexity;
		mazeDensity = density;

		ClearMaze();
		GenerateMaze();
		DrawMaze();
		
		
	}

	protected void GenerateMaze() {
		maze.GenerateMaze(mazeWidth,mazeHeight,mazeComplexity,mazeDensity);
		UpdateWidthValue();
		UpdateHeightValue();
		PlaceThePlayer();
		PlaceTheEnemy();
		
	}

	protected void DrawMaze() {
		// Make floor (and make it bright green)
		GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
		floor.transform.position = new Vector3(maze.Width/2,0,maze.Height/-2);
		floor.transform.localScale = new Vector3(maze.Width/10.0f,1.0f,maze.Height/10.0f);
		// Add this to the mazeBase
		floor.transform.parent = mazeBase.transform;
		floor.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f);

		// Make walls (and make it a darker green)
		for(int y = 0; y<maze.Height; y++) {
			for(int x = 0; x<maze.Width; x++) {
				if(maze.IsWall(x,y)) {
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = new Vector3(x, 0.5f, y * -1);
					cube.transform.localScale = new Vector3 (1, 1, 1);
					// Add this to the mazeBase (so we don't have a bunch of Cubes in the Heirarchy)
					cube.transform.parent = mazeBase.transform;
					cube.GetComponent<Renderer>().material.color = new Color(0.3f, 0.5f, 0.1f);
				}
			}
		}

		// Move the camera to show the whole maze
		float cameraY = (maze.Width<maze.Height) ? maze.Height + 1 : maze.Width + 1;
		Camera.main.transform.position = new Vector3(maze.Width/2.0f,cameraY,-1*maze.Height/2.0f);		
	}

	protected void ClearMaze() {
		Destroy(mazeBase);
		mazeBase = new GameObject("Maze GameObject");
	}

    protected void PlaceThePlayer()
	{
		Point placePlayer = maze.RandomOpenPosition();
		GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
		player.transform.position = new Vector3(placePlayer.x, 0.5f, placePlayer.y * -1);
		player.AddComponent<PlayerController>();
	}


    protected void PlaceTheEnemy()
	{
		Point placeEnemy = maze.RandomOpenPosition();
		GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
		enemy.transform.position = new Vector3(placeEnemy.x, 0.5f, placeEnemy.y * -1);
		enemy.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
		enemy.AddComponent<Enemy>();


	}

    protected void PlaceTheEnemyTwo()
	{
		Point placeEnemy = maze.RandomOpenPosition();
		GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		enemy.transform.position = new Vector3(placeEnemy.x, 0.5f, placeEnemy.y * -1);
		enemy.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
		enemy.AddComponent<EnemyTwo>();
	}


	protected void PlaceTheEnemyThree()
	{
		Point placeEnemy = maze.RandomOpenPosition();
		GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		enemy.transform.position = new Vector3(placeEnemy.x, 0.5f, placeEnemy.y * -1);
		enemy.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
		enemy.AddComponent<EnemyThree>();
	}
}
