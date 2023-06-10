
namespace PokerHandLibrary
{
    public interface IPokerPlayer
    {
        void ReceivePlayingCard(PlayingCard card);
        void Join(PlayPokerGame pokerGameHost);
        PokerHand GetPokerHand();
        void ClearCards();
    }

    public class PokerPlayer : IPokerPlayer
    {
        internal List<PlayingCard> CardsOnHand = new List<PlayingCard>();
        public string Name { get; set; }

        public PokerPlayer(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Receives poker card object from the Poker Game object
        /// </summary>
        /// <param name="card"></param>
        public void ReceivePlayingCard(PlayingCard card)
        {
            CardsOnHand.Add(card);
        }

        public void Join(PlayPokerGame pokerGame)
        {
            pokerGame.AddPlayer(this);
        }

        /// <summary>
        /// Return Arranged/Strategized card
        /// </summary>
        /// <returns></returns>
        public PokerHand GetPokerHand()
        {
            PokerHand _pokerHand = new PokerHand();

            _pokerHand.Arrange(this);

            return _pokerHand;
        }

        /// <summary>
        /// Clear cards for the next game round
        /// </summary>
        public void ClearCards()
        {
            CardsOnHand.Clear();
        }
    }
}
