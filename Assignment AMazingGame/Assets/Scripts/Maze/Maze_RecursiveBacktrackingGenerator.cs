using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze_RecursiveBacktrackingGenerator {
	private byte[,] maze;

	private int width = 0;
	public int Width { get { return width; } }

	private int height = 0;
	public int Height { get { return height; } }

	public Maze_RecursiveBacktrackingGenerator() {}

	/// <summary>
	/// Generates a maze based off the Backtracking Generator algorithm from The Jolly Sin's Maze Lib
	/// </summary>
	///
	/// <see cref="https://github.com/theJollySin/mazelib/blob/master/docs/MAZE_GEN_ALGOS.md#backtracking-generator"/>
	///
	/// <param name="mazeWidth"></param>
	/// <param name="mazeHeight"></param>
	public void GenerateMaze(int mazeWidth, int mazeHeight) {
		// Ensures that we have odd sizes
		width = (int)(mazeWidth / 2) * 2 + 1;
		height = (int)(mazeHeight / 2) * 2 + 1;

		// Create our array to hold the values
		maze = new byte[width, height];

		// Make all cells a wall
		for(int j=0; j<height; j++) {
			for(int i=0; i<width; i++) {
				maze[i,j] = 1;
			}
		}

		// 1. Randomly choose a starting cell.
		int x = (int)(Random.Range(2,width/2) *2);
		int y = (int)(Random.Range(2,height/2) *2);

		// Offset the start by one so that we are in an odd cell and we will have a border to our maze
		maze[x,y] = 0;
		
		ExamineCells(x+1,y+1);

		Debug.Log("DONE!\n" + this.ToString());
	}

	protected void ExamineCells(int x, int y) {
		List<Point> neighbors;
		do {
			// IMPORTANT: Start with a clear list, or it will cause an infinite loop!
			neighbors = new List<Point>();

			// Finding neighbors that are walls
			if(x>2 && maze[x-2,y]==1) { // To the left
				neighbors.Add(new Point(x-2,y));
			}
			if(x<width-2 && maze[x+2,y]==1) { // To the right
				neighbors.Add(new Point(x+2,y));
			}
			if(y>2 && maze[x,y-2]==1) { // Above
				neighbors.Add(new Point(x,y-2));
			}
			if(y<height-2 && maze[x,y+2]==1) { // Below
				neighbors.Add(new Point(x,y+2));
			}

			// 3. If all adjacent cells have been visited, back up to the
			// previous and repeat step 2.
			if(neighbors.Count>0) {
				Point newCheck = neighbors[Random.Range(0,neighbors.Count)];
				int xx = newCheck.x;
				int yy = newCheck.y;
				// Make Neighbor cell empty
				maze[xx,yy] = 0;
				// Make the cell between the "check" and "newCheck" a path
				maze[(int)(xx + (x-xx)/2),(int)(yy + (y-yy)/2)] = 0;
				
				Debug.Log("Going through..!\n" + this.ToString());
				ExamineCells(xx,yy);
			}
		} while(neighbors.Count>0);
	}

	public override string ToString() {
		string result = "";

		result = "Size: " + width + "," + height + "\n";
		result += "\n\n";
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
