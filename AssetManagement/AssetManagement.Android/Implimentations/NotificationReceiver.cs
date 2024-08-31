using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssetManagement.Droid.Implimentations
{
    [BroadcastReceiver(Enabled = true, Exported = false, Label = "Local Notifications Broadcast Receiver")]
    public class NotificationReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
			string notificationTitle = "MyApp";
			string notificationText = "Test notification";

			// Attempt to extract the "message" property from the payload: {"message":"Hello World!"}
			if (intent.GetStringExtra("message") != null)
			{
				notificationText = intent.GetStringExtra("message");
			}

			// Prepare a notification with vibration, sound and lights
			var builder = new Notification.Builder(context)
				  .SetAutoCancel(true)
				  .SetSmallIcon(Android.Resource.Drawable.IcDialogInfo)
				  .SetContentTitle(notificationTitle)
				  .SetContentText(notificationText)
				  .SetLights(Color.Red, 1000, 1000)
				  .SetVibrate(new long[] { 0, 400, 250, 400 })
				  .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
				  .SetContentIntent(PendingIntent.GetActivity(context, 0, new Intent(context, typeof(MainActivity)), PendingIntentFlags.UpdateCurrent));

		

			// Get an instance of the NotificationManager service
			var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

			// Build the notification and display it
			notificationManager.Notify(1, builder.Build());
		}
    }
}