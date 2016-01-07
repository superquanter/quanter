namespace Quanter.Trader.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartMarket = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnStartStrategy = new System.Windows.Forms.Button();
            this.btnRegStrategy = new System.Windows.Forms.Button();
            this.btnInitPersistence = new System.Windows.Forms.Button();
            this.btnStopMarket = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartMarket
            // 
            this.btnStartMarket.Location = new System.Drawing.Point(131, 36);
            this.btnStartMarket.Name = "btnStartMarket";
            this.btnStartMarket.Size = new System.Drawing.Size(75, 23);
            this.btnStartMarket.TabIndex = 0;
            this.btnStartMarket.Text = "启动市场";
            this.btnStartMarket.UseVisualStyleBackColor = true;
            this.btnStartMarket.Click += new System.EventHandler(this.btnStartMarket_Click);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(12, 36);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "初始化市场";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnStartStrategy
            // 
            this.btnStartStrategy.Location = new System.Drawing.Point(131, 80);
            this.btnStartStrategy.Name = "btnStartStrategy";
            this.btnStartStrategy.Size = new System.Drawing.Size(75, 23);
            this.btnStartStrategy.TabIndex = 0;
            this.btnStartStrategy.Text = "启动策略";
            this.btnStartStrategy.UseVisualStyleBackColor = true;
            this.btnStartStrategy.Click += new System.EventHandler(this.btnStartStrategy_Click);
            // 
            // btnRegStrategy
            // 
            this.btnRegStrategy.Location = new System.Drawing.Point(12, 80);
            this.btnRegStrategy.Name = "btnRegStrategy";
            this.btnRegStrategy.Size = new System.Drawing.Size(75, 23);
            this.btnRegStrategy.TabIndex = 0;
            this.btnRegStrategy.Text = "注册策略";
            this.btnRegStrategy.UseVisualStyleBackColor = true;
            this.btnRegStrategy.Click += new System.EventHandler(this.btnRegStrategy_Click);
            // 
            // btnInitPersistence
            // 
            this.btnInitPersistence.Location = new System.Drawing.Point(12, 124);
            this.btnInitPersistence.Name = "btnInitPersistence";
            this.btnInitPersistence.Size = new System.Drawing.Size(75, 23);
            this.btnInitPersistence.TabIndex = 0;
            this.btnInitPersistence.Text = "初始化持久化";
            this.btnInitPersistence.UseVisualStyleBackColor = true;
            // 
            // btnStopMarket
            // 
            this.btnStopMarket.Location = new System.Drawing.Point(255, 36);
            this.btnStopMarket.Name = "btnStopMarket";
            this.btnStopMarket.Size = new System.Drawing.Size(75, 23);
            this.btnStopMarket.TabIndex = 0;
            this.btnStopMarket.Text = "停止市场";
            this.btnStopMarket.UseVisualStyleBackColor = true;
            this.btnStopMarket.Click += new System.EventHandler(this.btnStopMarket_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 337);
            this.Controls.Add(this.btnInitPersistence);
            this.Controls.Add(this.btnRegStrategy);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.btnStartStrategy);
            this.Controls.Add(this.btnStopMarket);
            this.Controls.Add(this.btnStartMarket);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartMarket;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnStartStrategy;
        private System.Windows.Forms.Button btnRegStrategy;
        private System.Windows.Forms.Button btnInitPersistence;
        private System.Windows.Forms.Button btnStopMarket;
    }
}

