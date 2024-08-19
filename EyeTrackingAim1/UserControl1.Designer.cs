namespace EyeTrackingAim1
{
    partial class UserControl1
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonGoSetting = new System.Windows.Forms.Button();
            this.buttonGoEyeTracking = new System.Windows.Forms.Button();
            this.buttonPruss = new System.Windows.Forms.Button();
            this.buttonPrediction = new System.Windows.Forms.Button();
            this.buttonCaliCoodinate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(320, 100);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // buttonGoSetting
            // 
            this.buttonGoSetting.Location = new System.Drawing.Point(320, 213);
            this.buttonGoSetting.Name = "buttonGoSetting";
            this.buttonGoSetting.Size = new System.Drawing.Size(121, 23);
            this.buttonGoSetting.TabIndex = 2;
            this.buttonGoSetting.Text = "Setting";
            this.buttonGoSetting.UseVisualStyleBackColor = true;
            this.buttonGoSetting.Click += new System.EventHandler(this.buttonGoSetting_Click);
            // 
            // buttonGoEyeTracking
            // 
            this.buttonGoEyeTracking.Location = new System.Drawing.Point(527, 213);
            this.buttonGoEyeTracking.Name = "buttonGoEyeTracking";
            this.buttonGoEyeTracking.Size = new System.Drawing.Size(121, 23);
            this.buttonGoEyeTracking.TabIndex = 3;
            this.buttonGoEyeTracking.Text = "EyeTracking";
            this.buttonGoEyeTracking.UseVisualStyleBackColor = true;
            this.buttonGoEyeTracking.Click += new System.EventHandler(this.buttonGoEyeTracking_Click);
            // 
            // buttonPruss
            // 
            this.buttonPruss.Location = new System.Drawing.Point(276, 100);
            this.buttonPruss.Name = "buttonPruss";
            this.buttonPruss.Size = new System.Drawing.Size(23, 20);
            this.buttonPruss.TabIndex = 4;
            this.buttonPruss.Text = "+";
            this.buttonPruss.UseVisualStyleBackColor = true;
            this.buttonPruss.Click += new System.EventHandler(this.buttonPruss_Click);
            // 
            // buttonPrediction
            // 
            this.buttonPrediction.Location = new System.Drawing.Point(97, 213);
            this.buttonPrediction.Name = "buttonPrediction";
            this.buttonPrediction.Size = new System.Drawing.Size(121, 23);
            this.buttonPrediction.TabIndex = 6;
            this.buttonPrediction.Text = "Prediction";
            this.buttonPrediction.UseVisualStyleBackColor = true;
            this.buttonPrediction.Click += new System.EventHandler(this.buttonCaliOffset_Click);
            // 
            // buttonCaliCoodinate
            // 
            this.buttonCaliCoodinate.Location = new System.Drawing.Point(97, 154);
            this.buttonCaliCoodinate.Name = "buttonCaliCoodinate";
            this.buttonCaliCoodinate.Size = new System.Drawing.Size(121, 23);
            this.buttonCaliCoodinate.TabIndex = 7;
            this.buttonCaliCoodinate.Text = "CaliCoodinate";
            this.buttonCaliCoodinate.UseVisualStyleBackColor = true;
            this.buttonCaliCoodinate.Click += new System.EventHandler(this.buttonCaliCoodinate_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCaliCoodinate);
            this.Controls.Add(this.buttonPrediction);
            this.Controls.Add(this.buttonPruss);
            this.Controls.Add(this.buttonGoEyeTracking);
            this.Controls.Add(this.buttonGoSetting);
            this.Controls.Add(this.comboBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(761, 321);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonGoSetting;
        private System.Windows.Forms.Button buttonGoEyeTracking;
        private System.Windows.Forms.Button buttonPruss;
        private System.Windows.Forms.Button buttonPrediction;
        private System.Windows.Forms.Button buttonCaliCoodinate;
    }
}
