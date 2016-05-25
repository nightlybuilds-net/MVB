using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Mvb.Cross;

namespace Mvb.Test.ViewModels
{
    public class TestVm : MvbBase
    {
        public TestVm()
        {
            this.TestCollection = new ObservableCollection<string>();
            base.InitBinder();
        }

        private string _test;

        public string Test
        {
            get { return this._test; }
            set { SetProperty(ref _test, value); }
        }


        private ObservableCollection<string> _testCollection;

        public ObservableCollection<string> TestCollection
        {
            get { return this._testCollection; }
            set { SetProperty(ref _testCollection, value); }
        }
    }
}
