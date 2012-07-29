using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

namespace SignalR.Client.MonoDroid.Sample
{
    [Activity(Label = "SignalR.Client.MonoDroid.Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class DemoActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var messageListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string>());
            var messageList = FindViewById<ListView>(Resource.Id.Messages);
            messageList.Adapter = messageListAdapter;

            var connection = new Connection("http://10.0.2.2:8081/echo");
            connection.Received += data => 
                RunOnUiThread(() => messageListAdapter.Add(data));

            var sendMessage = FindViewById<Button>(Resource.Id.SendMessage);
            var message = FindViewById<TextView>(Resource.Id.Message);

            sendMessage.Click += delegate
            {
                if (!string.IsNullOrWhiteSpace(message.Text) && connection.State == ConnectionState.Connected)
                {
                    connection.Send("Android: " + message.Text);

                    RunOnUiThread(() => message.Text = "");
                }
            };

            connection.Start().ContinueWith(task => connection.Send("Android: connected"));
        }
    }
}

