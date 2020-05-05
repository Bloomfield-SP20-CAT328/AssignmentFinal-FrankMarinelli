using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze {
	private byte[,] maze;

	private int width = 0;

	/// <value>The width of the maze</value>
	public int Width { get { return width; } }

	private int height = 0;
	/// <value>The height of the maze</value>
	public int Height { get { return height; } }

	private float complexity = 0.75f;
	/// <value>The complexity of the maze</value>
	public float Complexity { get { return complexity; } }

	private float density = 0.75f;
	/// <value>How dense the maze should be. 0 - 1, where one means little to no open space</value>
	public float Density { get { return density; } }

	public Maze() {}

	public void GenerateMaze(int mazeWidth, int mazeHeight) {
		GenerateMaze(mazeWidth, mazeHeight, complexity, density);
	}

	/// <summary>
	/// Generates a maze based off of Prim's algorithm
	/// </summary>
	///
	/// <see cref="http://en.wikipedia.org/wiki/Talk%3AMaze_generation_algorithm"/>
	///
	/// <param name="width"></param>
	/// <param name="height"></param>
	public void GenerateMaze(int mazeWidth, int mazeHeight, float mazeComplexity, float mazeDensity) {
		// Store new complexity and density values;
		complexity = mazeComplexity;
		density = mazeDensity;
		
		// Ensures that we have odd sizes
		width = (int)(mazeWidth / 2) * 2 + 1;
		height = (int)(mazeHeight / 2) * 2 + 1;

		complexity = (int)(complexity * 5 * (width + height));
		density = (float)Mathf.Floor(density * (int)(height / 2) * (int)(width / 2));

		// Create our array to hold the values
		maze = new byte[width, height];

		// Do borders
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				// If the X is the left or right most, or Y is the top or bottom value, make it a wall
				if ((y == 0) || (y == (height - 1)) || (x == 0) || (x == (width - 1))) {
					maze[x,y] = 1;
				} else {
					maze[x,y] = 0;
				}
			}
		}

		// Make isles
		for (int i = 0; i < density; i++) {
			// Pick a random point
			var x = (int)(Random.Range(0, (int)width / 2) * 2);
			var y = (int)(Random.Range(0, (int)height / 2) * 2);

			// Make this cell a wall
			maze[x,y] = 1;
			for(var j = 0; j<complexity; j++) {
				// Store which neighboring cells it can move to
				List<Point> neighbors = new List<Point>();
				if (x > 1) {
					neighbors.Add(new Point(x - 2, y));
				}
				if (x < width-2) {
					neighbors.Add(new Point(x + 2, y));
				}
				if (y > 1) {
					neighbors.Add(new Point(x, y - 2));
				}
				if (y < height-2) {
					neighbors.Add(new Point(x, y + 2));
				}

				if (neighbors.Count > 0) {
					// Pick a neighbor
					Point newLocation = neighbors[Random.Range(0,neighbors.Count)];
					int xx = (int)newLocation.x;
					int yy = (int)newLocation.y;

					if (maze[xx,yy] == 0) {
						// The neighbor becomes an wall
						maze[xx,yy] = 1;
						// Make the cell in-between the new and old location a wall
						maze[(int)(xx + (x - xx) / 2), (int)(yy + (y - yy) / 2)] = 1;
						// The new location becomes the next check, unless the complexity is reached
						x = xx;
						y = yy;
					}
				} else {
					break;
				}
			}
		}
		Debug.Log("DONE!\n" + this.ToString());
	}

	public override string ToString()  {
		string result = "";

		result = "Size: " + width + "," + height + " Complexity: " + complexity + "  Density: " + density + "\n";
		if(width>0 && height>0) {
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					// Write an X for where there is a wall, otherwise a space
					result += maze[x, y];
				}
				result += "\n";
			}
		} else {
			result = "No maze made";
		}

		return result;
	}

	public bool IsWall(int x, int y) {
		if (x < 0 || x > width) { Debug.LogError("The X value is invalid, only 0 to width (" + width + ") is acceptable!"); }
		if (y < 0 || y > height) { Debug.LogError("The Y value is invalid, only 0 to height (" + height + ") is acceptable!"); }

		return (maze[x, y]==0) ? false : true;
	}



	public bool IsWall(Point point)
	{
		return IsWall(point.x, point.y);
	}



	public bool IsOpen(int x, int y)
	{
		return !IsWall(x, y);
	}




	public bool IsOpen(Point point)
	{
		return !IsWall(point.x, point.y);
	}



	public Point RandomOpenPosition()
	{
		bool found = false;
		int x = 0;
		int y = 0;
		while (!found)
		{
			x = Random.Range(1, width - 1);
			y = Random.Range(0, height - 1);
			found = IsOpen(x, y);
		}

		return new Point(x, y);
	}
}
