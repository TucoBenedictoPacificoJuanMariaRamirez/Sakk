/*
 * Created by SharpDevelop.
 * User: Bence
 * Date: 2016.07.28.
 * Time: 15:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Sakk
{
	partial class GyalogcsereForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button vezerButton;
		private System.Windows.Forms.Button bastyaButton;
		private System.Windows.Forms.Button futoButton;
		private System.Windows.Forms.Button loButton;
		private System.Windows.Forms.Label hoverLabel;
		
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
			this.vezerButton = new System.Windows.Forms.Button();
			this.bastyaButton = new System.Windows.Forms.Button();
			this.futoButton = new System.Windows.Forms.Button();
			this.loButton = new System.Windows.Forms.Button();
			this.hoverLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// vezerButton
			// 
			this.vezerButton.Location = new System.Drawing.Point(13, 240);
			this.vezerButton.Name = "vezerButton";
			this.vezerButton.Size = new System.Drawing.Size(75, 23);
			this.vezerButton.TabIndex = 0;
			this.vezerButton.Text = "Vezér";
			this.vezerButton.UseVisualStyleBackColor = true;
			this.vezerButton.Click += new System.EventHandler(this.vezerClick);
			// 
			// bastyaButton
			// 
			this.bastyaButton.Location = new System.Drawing.Point(144, 240);
			this.bastyaButton.Name = "bastyaButton";
			this.bastyaButton.Size = new System.Drawing.Size(75, 23);
			this.bastyaButton.TabIndex = 1;
			this.bastyaButton.Text = "Bástya";
			this.bastyaButton.UseVisualStyleBackColor = true;
			this.bastyaButton.Click += new System.EventHandler(this.bastyaClick);
			// 
			// futoButton
			// 
			this.futoButton.Location = new System.Drawing.Point(275, 240);
			this.futoButton.Name = "futoButton";
			this.futoButton.Size = new System.Drawing.Size(75, 23);
			this.futoButton.TabIndex = 2;
			this.futoButton.Text = "Futó";
			this.futoButton.UseVisualStyleBackColor = true;
			this.futoButton.Click += new System.EventHandler(this.futoClick);
			// 
			// loButton
			// 
			this.loButton.Location = new System.Drawing.Point(406, 240);
			this.loButton.Name = "loButton";
			this.loButton.Size = new System.Drawing.Size(75, 23);
			this.loButton.TabIndex = 3;
			this.loButton.Text = "Ló";
			this.loButton.UseVisualStyleBackColor = true;
			this.loButton.Click += new System.EventHandler(this.loClick);
			// 
			// hoverLabel
			// 
			this.hoverLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.hoverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.hoverLabel.Location = new System.Drawing.Point(144, 322);
			this.hoverLabel.Name = "hoverLabel";
			this.hoverLabel.Size = new System.Drawing.Size(206, 56);
			this.hoverLabel.TabIndex = 5;
			this.hoverLabel.Text = "Húzd ide az egeret, hogy láthasd a táblát!";
			this.hoverLabel.MouseEnter += new System.EventHandler(this.elrejtControl);
			this.hoverLabel.MouseLeave += new System.EventHandler(this.felfedControl);
			// 
			// GyalogcsereForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(504, 504);
			this.ControlBox = false;
			this.Controls.Add(this.hoverLabel);
			this.Controls.Add(this.loButton);
			this.Controls.Add(this.futoButton);
			this.Controls.Add(this.bastyaButton);
			this.Controls.Add(this.vezerButton);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(520, 542);
			this.MinimizeBox = false;
			this.Name = "GyalogcsereForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sakk";
			this.ResumeLayout(false);

		}
	}
}
