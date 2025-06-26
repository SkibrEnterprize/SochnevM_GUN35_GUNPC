public enum Team
{
    Black,
    White,
}

public enum UnitType
{
    Check,
    Qween,
}

public enum CellState
{
    Empty,
    Selected,
    Occupied,
    Attack
}

public enum GameEvent
{
    TurnTransfer,
    UnitIsChoiced,
    TargetIsChoiced,
    MoveDone,
}

public enum GameStatus
{
    Lock,
    Unlock,
    Select,
    Move,
    Attack,
    Confirm
}