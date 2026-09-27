using System.Windows.Forms;

namespace парсер
{
    partial class FormРедакторПравила
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblIf;
        private TextBox txtIf;
        private Label lblDo;
        private ComboBox cmbDo;
        private Label lblParam;
        private TextBox txtParam;
        private Button btnOk;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblIf = new System.Windows.Forms.Label();
            this.txtIf = new System.Windows.Forms.TextBox();
            this.lblDo = new System.Windows.Forms.Label();
            this.cmbDo = new System.Windows.Forms.ComboBox();
            this.lblParam = new System.Windows.Forms.Label();
            this.txtParam = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIf
            // 
            this.lblIf.ForeColor = System.Drawing.Color.White;
            this.lblIf.Location = new System.Drawing.Point(10, 20);
            this.lblIf.Name = "lblIf";
            this.lblIf.Size = new System.Drawing.Size(120, 23);
            this.lblIf.TabIndex = 0;
            this.lblIf.Text = "Если содержит:";
            // 
            // txtIf
            // 
            this.txtIf.Location = new System.Drawing.Point(130, 20);
            this.txtIf.Name = "txtIf";
            this.txtIf.Size = new System.Drawing.Size(160, 20);
            this.txtIf.TabIndex = 1;
            // 
            // lblDo
            // 
            this.lblDo.ForeColor = System.Drawing.Color.White;
            this.lblDo.Location = new System.Drawing.Point(10, 60);
            this.lblDo.Name = "lblDo";
            this.lblDo.Size = new System.Drawing.Size(120, 23);
            this.lblDo.TabIndex = 2;
            this.lblDo.Text = "Действие:";
            // 
            // cmbDo
            // 
            this.cmbDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDo.Location = new System.Drawing.Point(130, 60);
            this.cmbDo.Name = "cmbDo";
            this.cmbDo.Size = new System.Drawing.Size(160, 21);
            this.cmbDo.TabIndex = 3;
            // 
            // lblParam
            // 
            this.lblParam.ForeColor = System.Drawing.Color.White;
            this.lblParam.Location = new System.Drawing.Point(10, 100);
            this.lblParam.Name = "lblParam";
            this.lblParam.Size = new System.Drawing.Size(120, 23);
            this.lblParam.TabIndex = 4;
            this.lblParam.Text = "Параметр:";
            // 
            // txtParam
            // 
            this.txtParam.Location = new System.Drawing.Point(130, 100);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(160, 20);
            this.txtParam.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(110, 150);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 30);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // FormРедакторПравила
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(320, 193);
            this.Controls.Add(this.lblIf);
            this.Controls.Add(this.txtIf);
            this.Controls.Add(this.lblDo);
            this.Controls.Add(this.cmbDo);
            this.Controls.Add(this.lblParam);
            this.Controls.Add(this.txtParam);
            this.Controls.Add(this.btnOk);
            this.Name = "FormРедакторПравила";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование правила";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
