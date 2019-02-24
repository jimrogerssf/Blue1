namespace Blue1
{
   partial class Blue1TestForm
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
         this.BtnConnect = new System.Windows.Forms.Button();
         this.TBMessages = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // BtnConnect
         // 
         this.BtnConnect.Location = new System.Drawing.Point(13, 13);
         this.BtnConnect.Name = "BtnConnect";
         this.BtnConnect.Size = new System.Drawing.Size(173, 52);
         this.BtnConnect.TabIndex = 0;
         this.BtnConnect.Text = "Connect";
         this.BtnConnect.UseVisualStyleBackColor = true;
         this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
         // 
         // TBMessages
         // 
         this.TBMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
         this.TBMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.TBMessages.Location = new System.Drawing.Point(13, 72);
         this.TBMessages.Multiline = true;
         this.TBMessages.Name = "TBMessages";
         this.TBMessages.Size = new System.Drawing.Size(375, 366);
         this.TBMessages.TabIndex = 1;
         // 
         // Blue1TestForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.TBMessages);
         this.Controls.Add(this.BtnConnect);
         this.Name = "Blue1TestForm";
         this.Text = "Bluetooth Test App";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button BtnConnect;
      private System.Windows.Forms.TextBox TBMessages;
   }
}

