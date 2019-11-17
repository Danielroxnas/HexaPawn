using System.Linq;

namespace HexaPawnService
{
    public interface IPlayer
    {
        Color Color { get; set; }
        int TimesWon { get; set; }
        AvailableAction LastAvailableActions { get; set; }
        void LastState(BoardService board);
        IBoardState BoardState { get; }
        BoardService LastBord { get; set; }
        bool Human { get; set; }
    }
    public class Player : IPlayer
    {
        public Player(Color color, IBoardState boardState, bool human)
        {
            Color = color;
            BoardState = boardState;
            Human = human;
        }
        public Player()
        {

        }
        public AvailableAction LastAvailableActions { get; set; } = new AvailableAction();
        public bool Human { get; set; }

        public Color Color { get; set; }
        public int TimesWon { get; set; } = 0;
        public IBoardState BoardState { get; }
        public BoardService LastBord { get; set; }
        public void LastState(BoardService board)
        {
            var b = BoardState.Boards.Where(z => z.Pieces.Equals(board.Pieces)).FirstOrDefault();
            LastBord = b;
        }
    }
}
