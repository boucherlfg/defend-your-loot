namespace Utility
{
    public enum Allegiance {
        Ally = 0,
        Enemy = 1
    }
    public enum IdleType {
        StartPos = 0,
        Roam = 1,
    }
    public enum LevelDoneType {
        Win,
        Lose
    }

    public enum GameStateEnum
    {
        Intro,
        Shop,
        Placement,
        Level,
        GameOver,
        End,
    }
}