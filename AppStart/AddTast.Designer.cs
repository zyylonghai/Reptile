namespace AppStart
{
    partial class AddTast
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
            this.label1 = new System.Windows.Forms.Label();
            this.txturl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTastNm = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ElemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElemNm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElemClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElemTagNm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldNm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsGetnum = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PostEmail = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtloginurl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtuserId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtuseridinput = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtpwdinput = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtloginbtn = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txbsearchkey = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearchBtnID = new System.Windows.Forms.TextBox();
            this.txtInputID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtamzxpath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL:";
            // 
            // txturl
            // 
            this.txturl.Location = new System.Drawing.Point(86, 60);
            this.txturl.Name = "txturl";
            this.txturl.Size = new System.Drawing.Size(557, 21);
            this.txturl.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "任务名：";
            // 
            // txtTastNm
            // 
            this.txtTastNm.Location = new System.Drawing.Point(86, 103);
            this.txtTastNm.Name = "txtTastNm";
            this.txtTastNm.Size = new System.Drawing.Size(557, 21);
            this.txtTastNm.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ElemID,
            this.ElemNm,
            this.ElemClass,
            this.ElemTagNm,
            this.XPath,
            this.FieldNm,
            this.IsGetnum,
            this.PostEmail});
            this.dataGridView1.Location = new System.Drawing.Point(33, 288);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1221, 223);
            this.dataGridView1.TabIndex = 2;
            // 
            // ElemID
            // 
            this.ElemID.HeaderText = "元素ID";
            this.ElemID.Name = "ElemID";
            // 
            // ElemNm
            // 
            this.ElemNm.HeaderText = "元素Name";
            this.ElemNm.Name = "ElemNm";
            // 
            // ElemClass
            // 
            this.ElemClass.HeaderText = "元素样式类";
            this.ElemClass.Name = "ElemClass";
            // 
            // ElemTagNm
            // 
            this.ElemTagNm.HeaderText = "元素TagNm";
            this.ElemTagNm.Name = "ElemTagNm";
            // 
            // XPath
            // 
            this.XPath.HeaderText = "XPath";
            this.XPath.Name = "XPath";
            // 
            // FieldNm
            // 
            this.FieldNm.HeaderText = "字段名";
            this.FieldNm.Name = "FieldNm";
            // 
            // IsGetnum
            // 
            this.IsGetnum.HeaderText = "是否只获取数字";
            this.IsGetnum.Name = "IsGetnum";
            // 
            // PostEmail
            // 
            this.PostEmail.HeaderText = "是否发邮件";
            this.PostEmail.Name = "PostEmail";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "需要抓取的信息";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 530);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(611, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(86, 24);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(557, 21);
            this.txtID.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "登录网址：";
            // 
            // txtloginurl
            // 
            this.txtloginurl.Location = new System.Drawing.Point(86, 145);
            this.txtloginurl.Name = "txtloginurl";
            this.txtloginurl.Size = new System.Drawing.Size(557, 21);
            this.txtloginurl.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(656, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "帐号：";
            // 
            // txtuserId
            // 
            this.txtuserId.Location = new System.Drawing.Point(699, 22);
            this.txtuserId.Name = "txtuserId";
            this.txtuserId.Size = new System.Drawing.Size(198, 21);
            this.txtuserId.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(913, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "帐号元素ID";
            // 
            // txtuseridinput
            // 
            this.txtuseridinput.Location = new System.Drawing.Point(983, 22);
            this.txtuseridinput.Name = "txtuseridinput";
            this.txtuseridinput.Size = new System.Drawing.Size(250, 21);
            this.txtuseridinput.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(656, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "密码：";
            // 
            // txtpwd
            // 
            this.txtpwd.Location = new System.Drawing.Point(699, 56);
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.Size = new System.Drawing.Size(198, 21);
            this.txtpwd.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(913, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "密码元素ID：";
            // 
            // txtpwdinput
            // 
            this.txtpwdinput.Location = new System.Drawing.Point(983, 54);
            this.txtpwdinput.Name = "txtpwdinput";
            this.txtpwdinput.Size = new System.Drawing.Size(250, 21);
            this.txtpwdinput.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(656, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "登录按钮元素ID";
            // 
            // txtloginbtn
            // 
            this.txtloginbtn.Location = new System.Drawing.Point(751, 98);
            this.txtloginbtn.Name = "txtloginbtn";
            this.txtloginbtn.Size = new System.Drawing.Size(159, 21);
            this.txtloginbtn.TabIndex = 16;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(946, 100);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(126, 16);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "是否从amz插件获取";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 184);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "搜索关键字";
            // 
            // txbsearchkey
            // 
            this.txbsearchkey.Location = new System.Drawing.Point(102, 182);
            this.txbsearchkey.Margin = new System.Windows.Forms.Padding(2);
            this.txbsearchkey.Name = "txbsearchkey";
            this.txbsearchkey.Size = new System.Drawing.Size(541, 21);
            this.txbsearchkey.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(653, 188);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "搜索按钮ID";
            // 
            // txtSearchBtnID
            // 
            this.txtSearchBtnID.Location = new System.Drawing.Point(741, 185);
            this.txtSearchBtnID.Name = "txtSearchBtnID";
            this.txtSearchBtnID.Size = new System.Drawing.Size(513, 21);
            this.txtSearchBtnID.TabIndex = 24;
            // 
            // txtInputID
            // 
            this.txtInputID.Location = new System.Drawing.Point(741, 139);
            this.txtInputID.Name = "txtInputID";
            this.txtInputID.Size = new System.Drawing.Size(513, 21);
            this.txtInputID.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(653, 142);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "搜索输入框ID";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(31, 223);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 27;
            this.label14.Text = "AMZxpath";
            // 
            // txtamzxpath
            // 
            this.txtamzxpath.Location = new System.Drawing.Point(102, 220);
            this.txtamzxpath.Name = "txtamzxpath";
            this.txtamzxpath.Size = new System.Drawing.Size(541, 21);
            this.txtamzxpath.TabIndex = 28;
            // 
            // AddTast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 572);
            this.Controls.Add(this.txtamzxpath);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtSearchBtnID);
            this.Controls.Add(this.txtInputID);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txbsearchkey);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtloginbtn);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtpwdinput);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtpwd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtuseridinput);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtuserId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtloginurl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtTastNm);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txturl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddTast";
            this.Text = "AddTast";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txturl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTastNm;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtloginurl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtuserId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtuseridinput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtpwdinput;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtloginbtn;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElemNm;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElemClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElemTagNm;
        private System.Windows.Forms.DataGridViewTextBoxColumn XPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldNm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsGetnum;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PostEmail;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txbsearchkey;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearchBtnID;
        private System.Windows.Forms.TextBox txtInputID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtamzxpath;
    }
}