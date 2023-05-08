using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfLoadedDiceDemo.Models
{
    /// <summary>
    /// Data Class to define a loaded die
    /// </summary>
    internal class LoadedDie
    {
        #region Variables

        // random number generator - keep around for random seeds
        Random _randomNumGenerator = new Random();

        #endregion Variables

        #region Properties

        /// <summary>
        /// An identifier for this die
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Selected Face to be favored in the weighting
        /// </summary>
        public int FavoredFaceValue { get; }

        /// <summary>
        /// Selected Weight to give the favored face
        /// </summary>
        public int FavoredFaceWeight { get; }

        /// <summary>
        /// The current value of this die
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// The current image of this die
        /// </summary>
        public ImageSource? CurrentImage 
        { 
            get
            {
                ImageSource? retVal = null;
                Uri packUri;

                switch (CurrentValue)
                {
                    case 1:
                        packUri = new Uri("pack://application:,,,/Images/Dice1.png");
                        retVal = new BitmapImage(packUri);
                        break;
                    case 2:
                        packUri = new Uri("pack://application:,,,/Images/Dice2.png");
                        retVal = new BitmapImage(packUri);
                        break;
                    case 3:
                        packUri = new Uri("pack://application:,,,/Images/Dice3.png");
                        retVal = new BitmapImage(packUri);
                        break;
                    case 4:
                        packUri = new Uri("pack://application:,,,/Images/Dice4.png");
                        retVal = new BitmapImage(packUri);
                        break;
                    case 5:
                        packUri = new Uri("pack://application:,,,/Images/Dice5.png");
                        retVal = new BitmapImage(packUri);
                        break;
                    case 6:
                        packUri = new Uri("pack://application:,,,/Images/Dice6.png");
                        retVal = new BitmapImage(packUri);
                        break;
                }

                return retVal;

            }
        }

        #endregion Properties

        #region Constructor/Destructor
        /// <summary>
        /// Instantiate a new loaded die
        /// </summary>
        /// <param name="id"></param>
        /// <param name="favoredFaceValue"></param>
        /// <param name="favoredFaceWeight"></param>
        public LoadedDie(int id, int favoredFaceValue, int favoredFaceWeight) 
        {
            Id = id;
            FavoredFaceValue = favoredFaceValue;
            FavoredFaceWeight = favoredFaceWeight;
        }

        #endregion Constructor/Destructor

        #region Methods
        /// <summary>
        /// Roll the dice - add more options to the weighted value
        /// </summary>
        public void RollDice()
        {
            List<int> weightedValues = new List<int>();
            int idx;

            // load the initial 6 values
            for (int i = 0; i < 6; i++)
            {
                weightedValues.Add(i + 1);
            }

            // add the extra weight
            for (int i = 0; i < FavoredFaceWeight - 1; i++)
            {
                weightedValues.Add(FavoredFaceValue);
            }

            // grab a random index
            idx = _randomNumGenerator.Next(1, weightedValues.Count);

            // return the indexed value
            CurrentValue = weightedValues[idx - 1];

        }

        #endregion Methods
    }
}
