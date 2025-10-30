using UnityEngine;

public static class ColorExtensions
{
	public static Color GetColor(this int index)
	{
		return index switch
		{
			1 => Color.red,
			2 => Color.yellow,
			3 => Color.blue,            
			_ => Color.white
		};
	}
	
	public static Color GetColor(this ColorType type)
	{
		return type switch
		{
			ColorType.Red => Color.red,
			ColorType.Yellow => Color.yellow,
			ColorType.Blue => Color.blue,            
			_ => Color.white
		};
	}
}