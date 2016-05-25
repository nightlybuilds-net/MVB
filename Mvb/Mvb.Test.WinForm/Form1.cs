using System;
using System.Collections.Generic;
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
        private MvBinder<TestVm> _vm;

        public Form1()
        {
            InitializeComponent();

            this._vm = new MvBinder<TestVm>(new TestVm());

            this._vm.AddAction<TestVm>(t=>t.Test, () =>
            {
                AppendTextBox("vai cazzo!");
            });

            this._vm.AddAction("Pippo", () =>
            {
                AppendTextBox("add!");
            });
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
            this._vm.VmInstance.Test = "cambio!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this._vm.VmInstance.TestCollection.Add("1");
        }
    }
}
