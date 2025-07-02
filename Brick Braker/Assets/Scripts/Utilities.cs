using UnityEngine;

public class Utilities : MonoBehaviour
{
    public enum GameState
    {
        Play, Pause
    }

    public static float GetNonZeroRandomFloat(float min = -1.0f, float max = 1.0f) // A static class cannot have non static function
    {
        float num;

        do
        {
            num = Random.Range(min, max);
        } while (Mathf.Approximately(num, 0.0f));

        return num;
    }
    public static Color[] Colors =
    {
        Color.blue, Color.green, Color.red,
        Color.cyan, Color.yellow, Color.magenta
    };
    
    

}

