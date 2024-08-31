using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Interface
{
    public interface IAppVersionAndBuild
    {
        string GetVersionNumber();
        string GetBuildNumber();
    }
}
