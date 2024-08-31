using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Interface
{
    public interface INotification
    {
        void CreateNotification(string title, string message);
    }
}
