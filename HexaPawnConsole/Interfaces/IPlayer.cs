using System.Linq;

namespace HexaPawnConsole
{
    public interface IPlayer
    {
        Color Color { get; set; }
        int TimesWon { get; set; }
        AvailableAction1 LastAvailableActions { get; set; }
        void LastState(BoardService board);
        IBoardState BoardState { get; }
        BoardService LastBord { get; set; }
        bool Human { get; }
    }
    public class Player : IPlayer
    {
        public Player(Color piece, IBoardState boardState, bool human)
        {
            Color = piece;
            BoardState = boardState;
            Human = human;
        }
        public AvailableAction1 LastAvailableActions { get; set; } = new AvailableAction1();

        public Color Color { get; set; }
        public int TimesWon { get; set; } = 0;
        public IBoardState BoardState { get; }
        public bool Human { get; }
        public BoardService LastBord { get; set; }
        public void LastState(BoardService board)
        {
            var b = BoardState.Boards.Where(x => x.Pieces1.OrderBy(kvp => kvp.Key).SequenceEqual(board.Pieces1.OrderBy(kvp => kvp.Key))).FirstOrDefault();

            LastBord = b ?? board;
        }
    }
}
