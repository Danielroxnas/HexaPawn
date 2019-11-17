using System.Linq;

namespace HexaPawnService
{
    public class Human 
    {
        public Human(Color piece, IBoardState boardState)
        {
            Color = piece;
            BoardState = boardState;
        }
        public AvailableAction LastAvailableActions { get; set; } = new AvailableAction();

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
