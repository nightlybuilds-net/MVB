using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Mvb.Core.Abstract;
using RemIoc;

namespace Mvb.Test.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            RIoc.Register<IUiRunner, WpfRunner>();

        }
    }
}
