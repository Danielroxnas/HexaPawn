namespace HexaPawnConsole
{
    public interface IPlayer
    {
        Color Color { get; set; }
        int TimesWon { get; set; }
        AvailableAction LastAvailableActions { get; set; }
        void LastState(BoardService board);
        IBoardState BoardState { get; }
        BoardService LastBord { get; set; }
    }
}
