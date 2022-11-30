using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Helpers
{
	static float cellWidth = 1;
	static float cellHeight = 0.5f;
	public static async Task<bool> Rotate(this GameObject gameObject)
	{
		int rotateCount = 0;
		int times = 7;
		for (int i = 0; i < times; i++)
		{
			/*if (rotateCount++ % 2 == 0)
				return;*/
			Quaternion rotation = Quaternion.Euler(0, 0, -90) * gameObject.transform.rotation;
			gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, 50 * Time.deltaTime);
			await Task.Delay(100);
		}
		return true;
	}

	public static bool TileType(this Tilemap tilemap, string type, Vector2 pos)
	{
		if (type == "") return true;
		Vector3Int convertedPos = new((int)pos.x, (int)pos.y, 0);
		var tpos = tilemap.WorldToCell(pos);
		TileBase tile = tilemap.GetTile(tpos);
		return tile?.name == type;
	}

	public static bool TileType(this Tilemap tilemap, string type, Vector3Int pos)
	{
		if (type == "") return true;
		var tpos = tilemap.WorldToCell(pos);
		TileBase tile = tilemap.GetTile(tpos);
		return tile?.name == type;
	}

	public static bool TileTypeInRange(this Tilemap tilemap, string type, Vector2 pos, int range)
	{
		if (type == "") return true;
		for (float i = -range; i <= range; i += cellWidth)
		{
			for (float j = -range; j <= range; j += cellHeight)
			{
				Vector2 tpos = new(pos.x + i, pos.y + j);
				//Vector3Int convertedPos = new((int)pos.x + i, (int)pos.y + j, 0);
				if (TileType(tilemap, type, tpos))
					return true;
			}
		}
		return false;
	}

	public static string ToSnakeCase(this string text)
	{
		if (text == null)
		{
			throw new ArgumentNullException(nameof(text));
		}
		if (text.Length < 2)
		{
			return text;
		}
		var sb = new StringBuilder();
		sb.Append(char.ToLowerInvariant(text[0]));
		for (int i = 1; i < text.Length; ++i)
		{
			char c = text[i];
			if (char.IsUpper(c))
			{
				sb.Append('_');
				sb.Append(char.ToLowerInvariant(c));
			}
			else
			{
				sb.Append(c);
			}
		}
		return sb.ToString();
	}
}
