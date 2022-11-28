
namespace Decimation
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlMap = new System.Windows.Forms.Panel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.lblHP = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pnlMap
			// 
			this.pnlMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMap.BackColor = System.Drawing.Color.Black;
			this.pnlMap.Location = new System.Drawing.Point(31, 158);
			this.pnlMap.Margin = new System.Windows.Forms.Padding(19, 20, 19, 20);
			this.pnlMap.Name = "pnlMap";
			this.pnlMap.Size = new System.Drawing.Size(563, 558);
			this.pnlMap.TabIndex = 0;
			this.pnlMap.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlMapPaint);
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.BackColor = System.Drawing.Color.Black;
			this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMessage.Location = new System.Drawing.Point(31, 10);
			this.lblMessage.Margin = new System.Windows.Forms.Padding(19, 0, 19, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(563, 91);
			this.lblMessage.TabIndex = 1;
			this.lblMessage.Text = "Annihilate the numbers! Make them zero! (Press numpad 0 for help)";
			// 
			// lblHP
			// 
			this.lblHP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lblHP.BackColor = System.Drawing.Color.Black;
			this.lblHP.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHP.Location = new System.Drawing.Point(31, 109);
			this.lblHP.Margin = new System.Windows.Forms.Padding(19, 0, 19, 0);
			this.lblHP.Name = "lblHP";
			this.lblHP.Size = new System.Drawing.Size(563, 41);
			this.lblHP.TabIndex = 2;
			this.lblHP.Text = "Rationality: ???";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(623, 744);
			this.Controls.Add(this.lblHP);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.pnlMap);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(19, 20, 19, 20);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Decimation";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyUp);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lblHP;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Panel pnlMap;
	}
}
