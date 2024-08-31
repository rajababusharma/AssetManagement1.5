using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Interface
{
    public interface IShareReports
    {
        string GetCSVPath(string filename);
        string GetImagePath(string filename);
    }
}
