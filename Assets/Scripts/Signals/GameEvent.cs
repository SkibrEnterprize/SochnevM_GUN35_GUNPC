public struct PlayersMove { }
public struct PlayersMoveDone { }
public struct TransferOfTurn { }
public struct SelectInstall 
{
    public Cell Cell; 
    public SelectInstall(Cell cell) 
    { Cell = cell; }
}
public struct SelectCancel { }
public struct SelectConfirm { }
public struct ChangeSelectedUnit { }
public struct DebugSignal { }