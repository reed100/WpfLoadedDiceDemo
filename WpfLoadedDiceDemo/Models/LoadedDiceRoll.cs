namespace WpfLoadedDiceDemo.Models
{
    /// <summary>
    /// Data Class to track each roll of the dice
    /// </summary>
    public class LoadedDiceRoll
    {
        #region Properties
        /// <summary>
        /// Roll sequence number
        /// </summary>
        public int RollNum { get; }

        /// <summary>
        /// The value for the die #1
        /// </summary>
        public int Die1Value { get; }

        /// <summary>
        /// The value for the die #2
        /// </summary>
        public int Die2Value { get; }

        #endregion Properties

        #region Constructor/Destructor
        /// <summary>
        /// Instantiate a new roll of the dice
        /// </summary>
        /// <param name="rollNum"></param>
        /// <param name="die1Value"></param>
        /// <param name="die2Value"></param>
        public LoadedDiceRoll(int rollNum, int die1Value, int die2Value) 
        {
            RollNum = rollNum;
            Die1Value = die1Value;
            Die2Value = die2Value;
        }

        #endregion Constructor/Destructor

    }
}
