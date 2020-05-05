public enum Direction {  None, Up, Right, Down, Left};

public class Movement
{
	static public Point[] Offsets = new Point[]
	{
		new Point (0, 0),

		new Point (0, -1),

		new Point (1, 0),

		new Point (0, 1),

		new Point (-1, 0)
	};
}
