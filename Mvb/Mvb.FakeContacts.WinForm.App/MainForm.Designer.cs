namespace Mvb.FakeContacts.WinForm.App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SummaryLbl = new System.Windows.Forms.Label();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.ContactsListView = new System.Windows.Forms.ListView();
            this.ShakeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SummaryLbl
            // 
            this.SummaryLbl.AutoSize = true;
            this.SummaryLbl.Location = new System.Drawing.Point(12, 9);
            this.SummaryLbl.Name = "SummaryLbl";
            this.SummaryLbl.Size = new System.Drawing.Size(50, 13);
            this.SummaryLbl.TabIndex = 0;
            this.SummaryLbl.Text = "Summary";
            // 
            // LoadBtn
            // 
            this.LoadBtn.Location = new System.Drawing.Point(6, 28);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(337, 29);
            this.LoadBtn.TabIndex = 1;
            this.LoadBtn.Text = "Load Contacts";
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // ContactsListView
            // 
            this.ContactsListView.Location = new System.Drawing.Point(6, 63);
            this.ContactsListView.Name = "ContactsListView";
            this.ContactsListView.Size = new System.Drawing.Size(337, 198);
            this.ContactsListView.TabIndex = 2;
            this.ContactsListView.UseCompatibleStateImageBehavior = false;
            // 
            // ShakeBtn
            // 
            this.ShakeBtn.Location = new System.Drawing.Point(6, 28);
            this.ShakeBtn.Name = "ShakeBtn";
            this.ShakeBtn.Size = new System.Drawing.Size(337, 29);
            this.ShakeBtn.TabIndex = 3;
            this.ShakeBtn.Text = "Shake Names";
            this.ShakeBtn.UseVisualStyleBackColor = true;
            this.ShakeBtn.Visible = false;
            this.ShakeBtn.Click += new System.EventHandler(this.ShakeBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 265);
            this.Controls.Add(this.ShakeBtn);
            this.Controls.Add(this.ContactsListView);
            this.Controls.Add(this.LoadBtn);
            this.Controls.Add(this.SummaryLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Mvb WinForms Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SummaryLbl;
        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.ListView ContactsListView;
        private System.Windows.Forms.Button ShakeBtn;
    }
}

