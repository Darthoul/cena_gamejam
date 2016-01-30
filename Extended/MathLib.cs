using UnityEngine;
using System.Collections;

public class MathLib {

	public static Vector3 ZeroZ (Vector3 vector) {
		return new Vector3 (vector.x, vector.y, 0);
	}

	public static Vector2 DismissZ (Vector3 vector) {
		return new Vector2 (vector.x, vector.y);
	}

	public static float SimplifiedAngleDelta(Vector3 vector, Vector3 target) {
		return Mathf.Atan2 (target.y - vector.y, target.x - vector.x) * Mathf.Rad2Deg;
	}

	public static float SimplifiedAngleDirection(Vector3 vector, Vector3 target) {
		Vector2 vector2D = DismissZ (vector);
		Vector2 target2D = DismissZ (target);
		float angleDirection = (vector2D.x * target2D.x) + (vector2D.y * target2D.y);
		Debug.Log (angleDirection);
		if (angleDirection > 0f) {
			return 1;
		} else if (angleDirection < 0f) {
			return -1;
		} else {
			return 0;
		}
	}

	public static float PreciseAngleDirection(Vector3 vector, Vector3 target) {
		Vector2 vector2D = DismissZ (vector);
		Vector2 target2D = DismissZ (target);
		float angleDirection = (vector2D.x * target2D.x) + (vector2D.y * target2D.y);
		return angleDirection * 10;
	}

	public static Vector3 Rotate(Vector3 vector, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
		
		float tx = vector.x;
		float ty = vector.y;
		vector.x = (cos * tx) - (sin * ty);
		vector.y = (sin * tx) + (cos * ty);
		return vector;
	}
}
