﻿using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AssetManagement.Droid.Implimentations;
using AssetManagement.Interface;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationImplementation))]
namespace AssetManagement.Droid.Implimentations
{
    public class NotificationImplementation : INotification
    {
        private Context mContext;
        private NotificationManager mNotificationManager;
        private NotificationCompat.Builder mBuilder;
        public static string NOTIFICATION_CHANNEL_ID = "10023";
        public NotificationImplementation()
        {
            mContext = global::Android.App.Application.Context;
        }
        public void CreateNotification(string title, string message)
        {
            try
            {
                

                var intent = new Intent(mContext, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.PreviousIsTop);

                // useful to open message reader page on notification click
                intent.PutExtra("title", title);
                intent.PutExtra("message", message);

                // var pendingIntent = PendingIntent.GetActivity(mContext, 0, intent, PendingIntentFlags.OneShot);
                var pendingIntent = PendingIntent.GetActivity(mContext, 0, intent, PendingIntentFlags.UpdateCurrent);

                var sound = global::Android.Net.Uri.Parse(ContentResolver.SchemeAndroidResource + "://" + mContext.PackageName + "/" + Resource.Raw.notification);
                // Creating an Audio Attribute
                var alarmAttributes = new AudioAttributes.Builder()
                    .SetContentType(AudioContentType.Sonification)
                    .SetUsage(AudioUsageKind.Notification).Build();

                mBuilder = new NotificationCompat.Builder(mContext);
                mBuilder.SetSmallIcon(Resource.Drawable.logo);
                mBuilder.SetContentTitle(title)
                        .SetSound(sound)
                        .SetAutoCancel(true)
                        .SetContentTitle(title)
                        .SetContentText(message)
                        .SetChannelId(NOTIFICATION_CHANNEL_ID)
                        .SetPriority((int)NotificationPriority.High)
                        .SetVibrate(new long[0])
                        .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                        .SetVisibility((int)NotificationVisibility.Public)
                        .SetSmallIcon(Resource.Drawable.icon)
                     
                        .SetContentIntent(pendingIntent);



                NotificationManager notificationManager = mContext.GetSystemService(Context.NotificationService) as NotificationManager;

                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    //NotificationImportance importance = global::Android.App.NotificationImportance.High;
                    NotificationImportance importance = global::Android.App.NotificationImportance.High;

                    NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, title, importance);
                    notificationChannel.EnableLights(true);
                    notificationChannel.EnableVibration(true);
                    notificationChannel.SetSound(sound, alarmAttributes);
                    notificationChannel.SetShowBadge(true);
                    notificationChannel.Importance = NotificationImportance.High;
                    notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                    if (notificationManager != null)
                    {
                        mBuilder.SetChannelId(NOTIFICATION_CHANNEL_ID);
                        notificationManager.CreateNotificationChannel(notificationChannel);
                    }
                }

                notificationManager.Notify(0, mBuilder.Build());
            }
            catch (Exception excp)
            {
                //
                //notificationManager.Notify(0, mBuilder.Build());
                Crashes.TrackError(excp);
            }
        }
    }
}