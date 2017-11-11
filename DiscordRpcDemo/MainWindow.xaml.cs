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

		/// <summary>
		/// Initialize the RPC.
		/// </summary>
		/// <param name="clientId"></param>
		private void Initialize(string clientId)
		{
			handlers = new DiscordRpc.EventHandlers();

			handlers.readyCallback = ReadyCallback;
			handlers.disconnectedCallback += DisconnectedCallback;
			handlers.errorCallback += ErrorCallback;

			DiscordRpc.Initialize(clientId, ref handlers, true, null);

			this.SetStatusBarMessage("Initialized.");
		}

		/// <summary>
		/// Update the presence status from what's in the UI fields.
		/// </summary>
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

		/// <summary>
		/// Calls ReadyCallback(), DisconnectedCallback(), ErrorCallback().
		/// </summary>
		private void RunCallbacks()
		{
			DiscordRpc.RunCallbacks();

			this.SetStatusBarMessage("Rallbacks run.");
		}

		/// <summary>
		/// Stop RPC.
		/// </summary>
		private void Shutdown()
		{
			DiscordRpc.Shutdown();

			this.SetStatusBarMessage("Shuted down.");
		}

		/// <summary>
		/// Called after RunCallbacks() when ready.
		/// </summary>
		private void ReadyCallback()
		{
			this.SetStatusBarMessage("Ready.");
		}

		/// <summary>
		/// Called after RunCallbacks() in cause of disconnection.
		/// </summary>
		/// <param name="errorCode"></param>
		/// <param name="message"></param>
		private void DisconnectedCallback(int errorCode, string message)
		{
			this.SetStatusBarMessage(string.Format("Disconnect {0}: {1}", errorCode, message));
		}

		/// <summary>
		/// Called after RunCallbacks() in cause of error.
		/// </summary>
		/// <param name="errorCode"></param>
		/// <param name="message"></param>
		private void ErrorCallback(int errorCode, string message)
		{
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

			this.Button_RunCallbacks.IsEnabled = true;
			this.Button_Update.IsEnabled = true;
			this.Button_Shutdown.IsEnabled = true;
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

			this.Button_RunCallbacks.IsEnabled = false;
			this.Button_Update.IsEnabled = false;
			this.Button_Shutdown.IsEnabled = false;
		}
	}
}
