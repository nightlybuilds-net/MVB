using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mvb.Cross;
using Mvb.Test.ModelBinders;

namespace Mvb.Test.WinForm
{
    public partial class Form1 : Form
    {
        private StockMb _mb;

        public Form1()
        {
            InitializeComponent();

            this._mb = new StockMb();

            this._mb.Binder.AddAction<StockMb>(t=>t.Test, () =>
            {
                AppendTextBox("vai cazzo!");
            });

            this._mb.Binder.AddActionForCollection<StockMb>(t=>t.TestCollection, args =>
            { MessageBox.Show(args.Action == NotifyCollectionChangedAction.Add ? "Add!" : "NOT Add!"); });
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            label1.Text += value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._mb.Test = "cambio!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this._mb.TestCollection.Add("1");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this._mb.TestCollection.RemoveAt(this._mb.TestCollection.Count-1);
        }
    }
}
