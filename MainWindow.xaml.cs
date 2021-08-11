using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RockPaperScissors
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private static readonly Random random = new();

		public string human;
		public string computer;

		public int betAmount;

		// 14 Rows fit inside TextBox
		public string[] battleLog = new string[14];
		public string battleLogConcatenated;

		public string insufficientFunds = "Insufficient Funds\n";
		public string matchTie;
		public string matchWin;
		public string matchLose;

		public int bankBalance = 100;

		public MainWindow()
		{
			InitializeComponent();
			balanceBox.Text = $"{bankBalance}";

		}

		// Click handling for buttons
		private void Rock_OnClick(object sender, RoutedEventArgs e) => ClickButton(1);
		private void Paper_OnClick(object sender, RoutedEventArgs e) => ClickButton(2);
		private void Scissors_OnClick(object sender, RoutedEventArgs e) =>ClickButton(3);

		// Set bet, confirm there's a large enough balance available
		// Set the Roll for human based on button, based on random for computer
		// Battle, update balance, and output concatenated string to textbox
		private void ClickButton(int humanFighter)
		{
			betAmount = (int)wagerSlider.Value;
			if (ConfirmWager())
			{
				human = Roll(humanFighter);
				computer = Roll(random.Next(1, 4));
				Battle();
			}
			else
			{
				UpdateBattleLog(insufficientFunds);
			}

			balanceBox.Text = $"{bankBalance}";
			battleLogBox.Text = battleLogConcatenated;
		}

		// Assigns Rock, Paper, Scissors based on number input
		public static string Roll(int fighter)
		{
			return fighter switch
			{
				1 => "Rock",
				2 => "Paper",
				3 => "Scissors",
				_ => "wrong selection"
			};
		}

		// Verifies there's enough in the bank to handle wager.
		public bool ConfirmWager()
		{
			return betAmount <= bankBalance;
		}

		// Handles the winning/losing mechanic, changes visibility of win or lose images
		// Updates the battle log, as well as the bank balance.
		public void Battle()
		{
			// Handles all tie outcomes
			if (human == computer)
			{
				Tie();
			}
			// All wins
			else if ((human == "Rock" && computer == "Scissors") || (human == "Paper" && computer == "Rock") || (human == "Scissors" && computer == "Paper"))
			{
				Win();
			}
			// All losses
			else
			{
				Lose();
			}
		}

		private void Win()
		{
			matchWin = $"Win {betAmount / 2 + 1}! Congratulations!\n";
			UpdateBalance(betAmount);
			UpdateBattleLog(matchWin);
			imageLose.Visibility = Visibility.Hidden;
			imageWin.Visibility = Visibility.Visible;
		}

		private void Lose()
		{
			matchLose = $"Lose {betAmount}! Tough break!\n";
			UpdateBalance(-betAmount);
			UpdateBattleLog(matchLose);
			imageLose.Visibility = Visibility.Visible;
			imageWin.Visibility = Visibility.Hidden;
		}

		private void Tie()
		{
			matchTie = $"Tie! Returning {betAmount}!\n";
			UpdateBattleLog(matchTie);
			imageLose.Visibility = Visibility.Hidden;
			imageWin.Visibility = Visibility.Hidden;
		}

		// Just handles the updating of the bank balance.
		public void UpdateBalance(int bet)
		{
			if (bet < 0)
			{
				bankBalance += bet;
			}
			else
			{
				bankBalance += bet / 2 + 1;
			}
		}

		// This is for scrolling the battle log down so the newest entry is at the top
		// of the textbox, as well as concatenating it for display.
		public void UpdateBattleLog(string output)
		{
			for (int i = 13; i > 0; i--)
			{
				battleLog[i] = battleLog[i - 1];
			}
			battleLog[0] = output;
			battleLogConcatenated = "";
			for (int i = 0; i < 14; i++)
			{
				battleLogConcatenated += battleLog[i];
			}
		}
	}


}
