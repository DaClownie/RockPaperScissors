﻿using System;
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
		Random random = new Random();

		public string human;
		public string computer;

		public int betAmount;

		public string[] battleLog = new string[14];
		public string battleLogConcatenated = "";
		public string insufficientFunds = "Insufficient Funds\n";
		public string matchTie = $"Tie! Returning wager!\n";
		public string matchWin = $"Win! Congratulations!\n";
		public string matchLose = $"Lose! Tough break!\n";

		public int bankBalance = 100;

		public MainWindow()
		{
			InitializeComponent();
			balanceBox.Text = $"{bankBalance}";

		}

		// Click handling for buttons
		private void rock_OnClick(object sender, RoutedEventArgs e)
		{
			// Set bet, confirm there's a large enough balance available
			// Set the Roll for human based on button, based on random for computer
			// Battle, update balance, and output concatenated string to textbox
			betAmount = (int)wagerSlider.Value;
			if (ConfirmWager())
			{
				human = Roll(1);
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
		private void paper_OnClick(object sender, RoutedEventArgs e)
		{
			betAmount = (int)wagerSlider.Value;
			if (ConfirmWager())
			{
				human = Roll(2);
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
		private void scissors_OnClick(object sender, RoutedEventArgs e)
		{
			betAmount = (int)wagerSlider.Value;
			if (ConfirmWager())
			{
				human = Roll(3);
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

			int choice = fighter;
			switch (choice)
			{
				case 1:
					return "Rock";
				case 2:
					return "Paper";
				case 3:
					return "Scissors";
				default:
					return "wrong selection";
			}
		}

		// Verifies there's enough in the bank to handle wager.
		public bool ConfirmWager()
		{
			if (betAmount <= bankBalance)
				return true;
			return false;
		}

		// Handles the winning/losing mechanic, changes visibility of win or lose images
		// Updates the battle log, as well as the bank balance.
		public void Battle()
		{
			if (human == "Rock")
			{
				switch (computer)
				{
					case "Rock":
						UpdateBattleLog(matchTie);
						imageLose.Visibility = Visibility.Hidden;
						imageWin.Visibility = Visibility.Hidden;
						break;
					case "Paper":
						UpdateBalance(-betAmount);
						UpdateBattleLog(matchLose);
						imageLose.Visibility = Visibility.Visible;
						imageWin.Visibility = Visibility.Hidden;
						break;
					case "Scissors":
						UpdateBalance(betAmount);
						UpdateBattleLog(matchWin);
						imageLose.Visibility = Visibility.Hidden;
						imageWin.Visibility = Visibility.Visible;
						break;
				}
			}
			else if (human == "Paper")
			{
				switch (computer)
				{
					case "Rock":
						UpdateBalance(betAmount);
						UpdateBattleLog(matchWin);
						imageLose.Visibility = Visibility.Hidden;
						imageWin.Visibility = Visibility.Visible;
						break;
					case "Paper":
						UpdateBattleLog(matchTie);
						imageLose.Visibility = Visibility.Hidden;
						imageWin.Visibility = Visibility.Hidden;
						break;
					case "Scissors":
						UpdateBalance(-betAmount);
						UpdateBattleLog(matchLose);
						imageLose.Visibility = Visibility.Visible;
						imageWin.Visibility = Visibility.Hidden;
						break;
				}
			}
			else if (human == "Scissors")
			{
				switch (computer)
				{
					case "Rock":
						UpdateBalance(-betAmount);
						UpdateBattleLog(matchLose);
						imageLose.Visibility = Visibility.Visible;
						imageWin.Visibility = Visibility.Hidden;
						break;
					case "Paper":
						UpdateBalance(betAmount);
						UpdateBattleLog(matchWin);
						imageLose.Visibility = Visibility.Hidden;
						imageWin.Visibility = Visibility.Visible;
						break;
					case "Scissors":
						UpdateBattleLog(matchTie);
						imageLose.Visibility = Visibility.Hidden;
						imageWin.Visibility = Visibility.Hidden;
						break;
				}
			}
		}

		// Just handles the updating of the bank balance.
		public void UpdateBalance(int bet)
		{
			if (bet < 0)
				bankBalance += bet;
			else
				bankBalance += bet / 2;
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
