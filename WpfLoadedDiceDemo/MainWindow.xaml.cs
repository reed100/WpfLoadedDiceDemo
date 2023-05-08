using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using WpfLoadedDiceDemo.Models;

namespace WpfLoadedDiceDemo
{
    /// <summary>
    /// Main Window for Loaded Dice demo
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Properties
        /// <summary>
        /// The desired number of rolls
        /// </summary>
        private int NumRolls
        {
            get
            {
                if (int.TryParse(txtNumRolls.Text, out int retVal))
                {
                    return retVal;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Die 1 Favored Value
        /// </summary>
        private int Die1FavoredValue
        {
            get
            {
                int retVal = 1;

                if (rdoDice1Favored1.IsChecked == true)
                {
                    retVal = 1;
                }
                else if (rdoDice1Favored2.IsChecked == true)
                {
                    retVal = 2;
                }
                else if (rdoDice1Favored3.IsChecked == true)
                {
                    retVal = 3;
                }
                else if (rdoDice1Favored4.IsChecked == true)
                {
                    retVal = 4;
                }
                else if (rdoDice1Favored5.IsChecked == true)
                {
                    retVal = 5;
                }
                else if (rdoDice1Favored6.IsChecked == true)
                {
                    retVal = 6;
                }
                return retVal;
            }
        }

        /// <summary>
        /// Die 1 Favored Weight
        /// </summary>
        private int Die1FavoredWeight
        {
            get
            {
                return (int)sldDice1Weight.Value;
            }
        }

        /// <summary>
        /// Die 2 Favored Value
        /// </summary>
        private int Die2FavoredValue
        {
            get
            {
                int retVal = 1;

                if (rdoDice2Favored1.IsChecked == true)
                {
                    retVal = 1;
                }
                else if (rdoDice2Favored2.IsChecked == true)
                {
                    retVal = 2;
                }
                else if (rdoDice2Favored3.IsChecked == true)
                {
                    retVal = 3;
                }
                else if (rdoDice2Favored4.IsChecked == true)
                {
                    retVal = 4;
                }
                else if (rdoDice2Favored5.IsChecked == true)
                {
                    retVal = 5;
                }
                else if (rdoDice2Favored6.IsChecked == true)
                {
                    retVal = 6;
                }
                return retVal;
            }
        }

        /// <summary>
        /// Die 2 Favored Weight
        /// </summary>
        private int Die2FavoredWeight
        {
            get
            {
                return (int)sldDice2Weight.Value;
            }
        }

        /// <summary>
        /// The tally of the current set of rolls
        /// </summary>
        protected ObservableCollection<LoadedDiceRoll> LoadedDieRolls { get; } = new ObservableCollection<LoadedDiceRoll>();

        #endregion Properties

        #region Constructor / Destructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ResetUI();

            lvResults.ItemsSource = LoadedDieRolls;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Roll the dice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            LoadedDie? dice1 = null;
            LoadedDie? dice2 = null;
            (bool isValid, string msg) validationResponse;

            try
            {
                // disable this button
                btnSubmit.IsEnabled = false;

                // clear any error messages
                lblErrorMessage.Text = string.Empty;

                // clear the results list
                LoadedDieRolls.Clear();

                // call to validate the input
                validationResponse = ValidInput();

                // validate input
                if (validationResponse.isValid)
                {
                    // hide the instructions overlay - revealing the results
                    brdResultsOverlay.Visibility = Visibility.Collapsed;

                    dice1 = new LoadedDie(1, Die1FavoredValue, Die1FavoredWeight);
                    dice2 = new LoadedDie(2, Die2FavoredValue, Die2FavoredWeight);

                    for (int i = 0; i < NumRolls; i++)
                    {
                        RollDice(dice1, dice2);

                        LoadedDieRolls.Add(new LoadedDiceRoll(i + 1, dice1.CurrentValue, dice2.CurrentValue));

                        await Task.Delay(1000);
                    }
                }
                else
                {
                    lblErrorMessage.Text = validationResponse.msg;
                }

            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
            finally
            {                
                dice1 = null;
                dice2 = null;
                btnSubmit.IsEnabled = true;
            }
        }

        /// <summary>
        /// Close the window please
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Reset the UI to it's original state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetUI();
        }

        #endregion Event Handlers

        #region Methods

        /// <summary>
        /// Confirm that input is valid
        /// </summary>
        /// <returns>tuple </returns>
        private (bool isValid,string msg) ValidInput()
        {
            bool retVal = true;
            string msg = string.Empty;

            if (Die1FavoredValue < 1 || Die1FavoredValue > 6)
            {
                msg += "Please a valid favored value for Die #1." + Environment.NewLine;
                retVal = false;
            }

            if (sldDice1Weight.Value < 1 || sldDice1Weight.Value > 5)
            {
                msg += "Please select a valid weight for Die #1." + Environment.NewLine;
                retVal = false;
            }

            if (Die2FavoredValue < 1 || Die2FavoredValue > 6)
            {
                msg += "Please select the favored value for Die #2." + Environment.NewLine;
                retVal = false;
            }

            if (sldDice2Weight.Value < 1 || sldDice2Weight.Value > 5)
            {
                msg += "Please select a valid weight for Die #2." + Environment.NewLine;
                retVal = false;
            }

            if (string.IsNullOrWhiteSpace(txtNumRolls.Text) == true)
            {
                msg += "Please enter the number of rolls you would like to have." + Environment.NewLine;
                retVal = false;
            }
            else if (int.TryParse(txtNumRolls.Text, out int tempValue) == false)
            {
                msg += "Please enter a valid number of rolls." + Environment.NewLine;
                retVal = false;
            }

            return (retVal, msg);
        }

        /// <summary>
        /// Roll the 2 dice - record the current dice roll results
        /// </summary>
        /// <param name="dice1"></param>
        /// <param name="dice2"></param>
        private void RollDice(LoadedDie dice1, LoadedDie dice2)
        {

            dice1.RollDice();
            imgDice1Results.Source = dice1.CurrentImage;

            dice2.RollDice();
            imgDice2Results.Source = dice2.CurrentImage;

        }

        /// <summary>
        /// reset the UI to it's original state
        /// </summary>
        private void ResetUI()
        {
            rdoDice1Favored1.IsChecked = true;
            sldDice1Weight.Value = sldDice1Weight.Minimum;
            rdoDice2Favored1.IsChecked = true;
            sldDice2Weight.Value = sldDice2Weight.Minimum;
            txtNumRolls.Text = "5";
            LoadedDieRolls.Clear();
            imgDice1Results.Source = null;
            imgDice2Results.Source = null;
            brdResultsOverlay.Visibility = Visibility.Visible;
        }

        #endregion Methods

    }
}
