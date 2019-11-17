using System.Linq;

namespace HexaPawnServices
{
    public interface IPlayer
    {
        IBoardState BoardState { get; }
        Color Color { get; set; }
        AvailableAction LastAvailableActions { get; set; }
        BoardService LastBord { get; set; }
        int TimesWon { get; set; }

        void LastState(BoardService board);
    }

}
