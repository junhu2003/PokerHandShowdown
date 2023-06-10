using System.Collections;

namespace PokerHandLibrary
{
    public interface IPlayPokerGame
    {
        void GivePlayingCards(PokerPlayer player, PlayingCard card);
        void AddPlayer(PokerPlayer player);
        void Evaluate();
        PokerHand? GetWinner();
    }
    public class PlayPokerGame : IPlayPokerGame
    {
        private Dictionary<string, PokerPlayer> PokerPlayers = new Dictionary<string, PokerPlayer>();
        private PokerHand? Winner { get; set; }

        public void GivePlayingCards(PokerPlayer player, PlayingCard card)
        {
            player.ReceivePlayingCard(card);
        }

        /// <summary>
        /// Add Player
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(PokerPlayer player)
        {
            if (PokerPlayers.ContainsKey(player.Name)) { throw new Exception("Player name already exists"); }

            PokerPlayers.Add(player.Name, player);
        }

        /// <summary>
        /// Store the winner in the PokerHand object and use as result
        /// </summary>
        /// <returns></returns>
        public PokerHand? GetWinner()
        {
            this.CheckHasDuplicates();
            this.Evaluate();

            PokerHand? _winner = Winner;

            return _winner;
        }

        /// <summary>
        /// Evaluate who the winnner is
        /// </summary>
        public void Evaluate()
        {
            List<PokerHand> _pokerHands = new List<PokerHand>();

            foreach (KeyValuePair<string, PokerPlayer> pair in PokerPlayers)
            {
                PokerHand _pokerHand = new PokerHand();
                _pokerHand = PokerPlayers[pair.Key].GetPokerHand();

                _pokerHands.Add(_pokerHand);
            }

            var sortPokerHandScore = from pokerHand in _pokerHands
                                     orderby pokerHand.IsFlush descending, 
                                        pokerHand.IsThreeOfAKind descending, 
                                        pokerHand.IsPair descending, 
                                        pokerHand.Score descending, 
                                        Convert.ChangeType(pokerHand.DominantSuit, pokerHand.DominantSuit.GetType()) ascending
                                     select pokerHand;

            this.Winner = sortPokerHandScore.DefaultIfEmpty().First();

        }

        /// <summary>
        /// Check if has duplicates, throw error if there's any
        /// </summary>
        private void CheckHasDuplicates()
        {
            ArrayList _playingCards = new ArrayList();

            foreach (KeyValuePair<string, PokerPlayer> pair in PokerPlayers)
            {
                foreach (PlayingCard card in PokerPlayers[pair.Key].CardsOnHand)
                {
                    string _card = string.Concat(card.Rank, card.Suit);

                    if (_playingCards.Contains(_card))
                    {
                        throw new Exception("Duplicate card(s) found");
                    }
                    else { _playingCards.Add(_card); }
                }
            }

        }
    }
}
