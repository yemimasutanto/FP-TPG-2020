using UnityEngine;

public static class RoundVector
{
    public static Vector2 RoundedVector(Vector2 input)
    {
        return new Vector2(Mathf.Round(input.x), Mathf.Round(input.y));
    }
}
