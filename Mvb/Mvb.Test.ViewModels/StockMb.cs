using System.Threading.Tasks;
using Mvb.Cross.Base;
using Mvb.Cross.Components;

namespace Mvb.Test.ModelBinders
{
    public class StockMb : MvbBase
    {
        private string _test;

        private MvbCollection<Stock> _testCollection;

        public StockMb()
        {
            this.TestCollection = new MvbCollection<Stock>();
            this.InitBinder();
        }

        public string Test
        {
            get { return this._test; }
            set { this.SetProperty(ref this._test, value); }
        }

        public MvbCollection<Stock> TestCollection
        {
            get { return this._testCollection; }
            set { this.SetProperty(ref this._testCollection, value); }
        }


        private bool _wait;

        public bool Wait
        {
            get { return this._wait; }
            set { this.SetProperty(ref this._wait, value); }
        }

        public async Task LogTaskTest()
        {
            this.Wait = true;
            await Task.Delay(2000);
            this.Wait = false;
        } 
    }

    public class Stock : Bindable
    {
        private string _name;

        public string Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value); }
        }

        private float _value;

        public float Value
        {
            get { return this._value; }
            set { this.SetProperty(ref this._value, value); }
        }
    }
}