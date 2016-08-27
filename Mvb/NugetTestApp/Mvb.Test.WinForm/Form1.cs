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
using Mvb.Test.ViewModels;

namespace Mvb.Test.WinForm
{
    public partial class Form1 : Form
    {
        private TestVm _vm;

        public Form1()
        {
            InitializeComponent();

            this._vm = new TestVm();

            this._vm.Binder.AddAction<TestVm>(t=>t.Test, () =>
            {
                AppendTextBox("vai cazzo!");
            });

            this._vm.Binder.AddActionForCollection<TestVm>(t=>t.TestCollection, args =>
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
            this._vm.Test = "cambio!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this._vm.TestCollection.Add("1");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this._vm.TestCollection.RemoveAt(this._vm.TestCollection.Count-1);
        }
    }
}
