using System;
using System.Windows;

namespace DiscordRpcDemo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// https://github.com/discordapp/discord-rpc/blob/master/examples/button-clicker/Assets/DiscordController.cs
	/// </summary>
	public partial class MainWindow : Window
	{
		private DiscordRpc.RichPresence presence;
		private int callbackCalls;

		DiscordRpc.EventHandlers handlers;

		public MainWindow()
		{
			InitializeComponent();

			this.TextBox_startTimestamp.Text = this.DateTimeToTimestamp(DateTime.UtcNow).ToString();
			this.TextBox_endTimestamp.Text = this.DateTimeToTimestamp(DateTime.UtcNow.AddHours(1)).ToString();
		}

		/*
		=============================================
		Private
		=============================================
		*/

		private void Initialize(string clientId)
		{
			callbackCalls = 0;
			handlers = new DiscordRpc.EventHandlers();

			handlers.readyCallback = ReadyCallback;
			handlers.disconnectedCallback += DisconnectedCallback;
			handlers.errorCallback += ErrorCallback;

			DiscordRpc.Initialize(clientId, ref handlers, true, null);

			this.SetStatusBarMessage("Initialized.");
		}

		private void UpdatePresence()
		{
			presence.details = this.TextBox_details.Text;
			presence.state = this.TextBox_state.Text;

			if (long.TryParse(this.TextBox_startTimestamp.Text, out long startTimestamp)) {
				presence.startTimestamp = startTimestamp;
			}

			if (long.TryParse(this.TextBox_endTimestamp.Text, out long endTimestamp)) {
				presence.endTimestamp = endTimestamp;
			}

			presence.largeImageKey = this.TextBox_largeImageKey.Text;
			presence.largeImageText = this.TextBox_largeImageText.Text;
			presence.smallImageKey = this.TextBox_smallImageKey.Text;
			presence.smallImageText = this.TextBox_smallImageText.Text;

			DiscordRpc.UpdatePresence(ref presence);

			this.SetStatusBarMessage("Presence updated.");
		}

		private void RunCallbacks()
		{
			DiscordRpc.RunCallbacks();

			this.SetStatusBarMessage("Rallbacks run.");
		}

		private void Shutdown()
		{
			DiscordRpc.Shutdown();

			this.SetStatusBarMessage("Shuted down.");
		}

		private void ReadyCallback()
		{
			++callbackCalls;

			this.SetStatusBarMessage("Ready.");
		}

		private void DisconnectedCallback(int errorCode, string message)
		{
			++callbackCalls;

			this.SetStatusBarMessage(string.Format("Disconnect {0}: {1}", errorCode, message));
		}

		private void ErrorCallback(int errorCode, string message)
		{
			++callbackCalls;

			this.SetStatusBarMessage(string.Format("Error {0}: {1}", errorCode, message));
		}

		/// <summary>
		/// Just set a message to be displayed in the status bar at the window's bottom.
		/// </summary>
		/// <param name="message"></param>
		private void SetStatusBarMessage(string message)
		{
			this.Label_Status.Content = message;
		}

		/// <summary>
		/// Convert a DateTime object into a timestamp.
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		private long DateTimeToTimestamp(DateTime dt)
		{
			return (dt.Ticks - 621355968000000000) / 10000000;
		}

		/*
		=============================================
		Event
		=============================================
		*/

		/// <summary>
		/// Called by clicking on the "Initialize" button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Initialize_Click(object sender, RoutedEventArgs e)
		{
			string clientId = this.TextBox_clientId.Text;
			bool isNumeric = ulong.TryParse(clientId, out ulong n);

			if (!isNumeric) {
				MessageBox.Show("The client ID must be a numeric value.");

				return;
			}

			this.Initialize(clientId);
		}

		/// <summary>
		/// Called by clicking on the "Update" button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Update_Click(object sender, RoutedEventArgs e)
		{
			this.UpdatePresence();
		}

		/// <summary>
		/// Called by clicking on the "RunCallbacks" button. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_RunCallbacks_Click(object sender, RoutedEventArgs e)
		{
			this.RunCallbacks();
		}

		/// <summary>
		/// Called by clicking on the "Shutdown" button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Shutdown_Click(object sender, RoutedEventArgs e)
		{
			this.Shutdown();
		}
	}
}
