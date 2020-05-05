using System;
/// <summary>
/// A struct for handling a 2D point
/// </summary>
/// 
/// <remarks>
/// More about documentation of C# code: https://docs.microsoft.com/en-us/dotnet/csharp/codedoc
/// VSCode C# documentation generator (With 3 slashed): https://marketplace.visualstudio.com/items?itemName=k--kato.docomment
/// </remarks>
public struct Point {
	/// <summary>
	/// The X axis of the point
	/// </summary>
	public int x,y;

	/// <summary>
	/// The Y axis of the point
	/// </summary>
	
	
	/// <summary>
	/// A simple struct for storing a point.
	/// </summary>
	/// <param name="ptX">X axis of point</param>
	/// <param name="ptY">Y axis of point</param>
	public Point(int ptX, int ptY) {
		x = ptX;
		y = ptY;
	}


    public Point(float ptX, float ptY)
	{
		x = (int)ptX;
		y = (int)ptY;
	}
	/// <summary>
	/// Adds two points
	/// </summary>
	/// <param name="p1">First point</param>
	/// <param name="p2">Second Point</param>
	/// <returns>The sum of the two point</returns>
	public static Point operator +(Point p1, Point p2) {
		return new Point( p1.x + p2.x, p1.y + p2.y);
	}

	/// <summary>
	/// Subtracts two points
	/// </summary>
	/// <param name="p1"></param>
	/// <param name="p2"></param>
	/// <returns>A <c>Point</c> with the difference of two points</returns>
	public static Point operator -(Point p1, Point p2) {
		return new Point( p1.x - p2.x, p1.y - p2.y);
	}

	/// <summary>
	/// Converts a <c>Point</c> to a string for writing
	/// </summary>
	/// <returns>A <c>string</c> formatted with a comma in parenthesis</returns>
	public override string ToString() {
		return "(" + x + "," + y + ") ";
	}

    public static int Distance(Point p1, Point p2)
	{
		//return (int)Math.Sqrt(Math.Pow( p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));

		Point diff = p1 - p2;
		return (int)Math.Sqrt(Math.Pow(diff.x, 2) + Math.Pow(diff.y, 2));
	}
}