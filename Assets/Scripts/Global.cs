public enum DIRECTION { NONE, LEFT, RIGHT, UP, DOWN };
public enum TOOL { NONE, BUCKET, SCYTHE, SEED1, SEED2, SEED3 }
public enum SEED_TYPE { NONE, PUMPKIN, CABBAGE, CARROT }
public delegate void OnDirectionChange (DIRECTION newDirection);
public delegate void OnGameWin ();

public class Global
{
    public const float WIN_TIME = 5f;
    public const float WIN_GOAL = 1f;
}
