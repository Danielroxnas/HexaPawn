using System.Linq;

namespace HexaPawnConsole
{
    public class Player : IPlayer
    {
        public Player(Color piece, IBoardState boardState, bool human)
        {
            Color = piece;
            BoardState = boardState;
            Human = human;
        }
        public AvailableAction LastAvailableActions { get; set; } = new AvailableAction();

        public Color Color { get; set; }
        public int TimesWon { get; set; } = 0;
        public IBoardState BoardState { get; }
        public bool Human { get; }
        public BoardService LastBord { get; set; }
        public void LastState(BoardService board)
        {
            var b = BoardState.Boards.Where(x => x.Pieces.OrderBy(kvp => kvp.Key).SequenceEqual(board.Pieces.OrderBy(kvp => kvp.Key))).FirstOrDefault();

            LastBord = b ?? board;
        }
    }
}
