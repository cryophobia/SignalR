using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace SignalR.Client.MonoTouch.Sample
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			var message = new EntryElement("Message", "Type your message here", "");
			var sendMessage = new StyledStringElement("Send Message")
			{
				Alignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Green
			};
			var receivedMessages = new Section();
			var root = new RootElement("") 
			{
				new Section() { message, sendMessage },
				receivedMessages
			};

			var connection = new Connection("http://localhost:8081/echo");

			sendMessage.Tapped += delegate 
			{
				if (!string.IsNullOrWhiteSpace(message.Value) && connection.State == ConnectionState.Connected)
				{
					connection.Send("iOS: " + message.Value);

					message.Value = "";
					message.ResignFirstResponder(true);
				}
			};

			connection.Received += data =>
			{
				InvokeOnMainThread(() =>
					receivedMessages.Add(new StringElement(data)));
			};
				

			connection.Start().ContinueWith(task =>
                connection.Send("iOS: Connected"));

			var viewController = new DialogViewController(root);
			window.RootViewController = viewController;

			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}