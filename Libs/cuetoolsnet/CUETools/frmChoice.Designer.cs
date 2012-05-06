namespace JDP
{
	partial class frmChoice
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChoice));
			this.buttonOk = new System.Windows.Forms.Button();
			this.listChoices = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanelMeta = new System.Windows.Forms.TableLayoutPanel();
			this.dataGridViewMetadata = new System.Windows.Forms.DataGridView();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.dataGridViewTracks = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumnTrackNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumnTrackTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumnTrackStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumnTrackLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanelMeta.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMetadata)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.buttonOk, "buttonOk");
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseVisualStyleBackColor = true;
			// 
			// listChoices
			// 
			this.listChoices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			resources.ApplyResources(this.listChoices, "listChoices");
			this.listChoices.FullRowSelect = true;
			this.listChoices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listChoices.HideSelection = false;
			this.listChoices.MultiSelect = false;
			this.listChoices.Name = "listChoices";
			this.listChoices.ShowItemToolTips = true;
			this.listChoices.SmallImageList = this.imageList1;
			this.listChoices.UseCompatibleStateImageBehavior = false;
			this.listChoices.View = System.Windows.Forms.View.Details;
			this.listChoices.SelectedIndexChanged += new System.EventHandler(this.listChoices_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			resources.ApplyResources(this.columnHeader1, "columnHeader1");
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "eac");
			this.imageList1.Images.SetKeyName(1, "freedb");
			this.imageList1.Images.SetKeyName(2, "musicbrainz");
			this.imageList1.Images.SetKeyName(3, "cue");
			this.imageList1.Images.SetKeyName(4, "tags");
			this.imageList1.Images.SetKeyName(5, "local");
			this.imageList1.Images.SetKeyName(6, "ctdb");
			this.imageList1.Images.SetKeyName(7, "discogs");
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			resources.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.listChoices, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelMeta, 0, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.buttonOk, 3, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// pictureBox1
			// 
			resources.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
			// 
			// tableLayoutPanelMeta
			// 
			resources.ApplyResources(this.tableLayoutPanelMeta, "tableLayoutPanelMeta");
			this.tableLayoutPanelMeta.Controls.Add(this.dataGridViewTracks, 0, 0);
			this.tableLayoutPanelMeta.Controls.Add(this.dataGridViewMetadata, 0, 0);
			this.tableLayoutPanelMeta.Name = "tableLayoutPanelMeta";
			// 
			// dataGridViewMetadata
			// 
			this.dataGridViewMetadata.AllowUserToAddRows = false;
			this.dataGridViewMetadata.AllowUserToDeleteRows = false;
			this.dataGridViewMetadata.AllowUserToResizeColumns = false;
			this.dataGridViewMetadata.AllowUserToResizeRows = false;
			this.dataGridViewMetadata.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dataGridViewMetadata.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridViewMetadata.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridViewMetadata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewMetadata.ColumnHeadersVisible = false;
			this.dataGridViewMetadata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.Value});
			resources.ApplyResources(this.dataGridViewMetadata, "dataGridViewMetadata");
			this.dataGridViewMetadata.GridColor = System.Drawing.SystemColors.ControlLight;
			this.dataGridViewMetadata.MultiSelect = false;
			this.dataGridViewMetadata.Name = "dataGridViewMetadata";
			this.dataGridViewMetadata.RowHeadersVisible = false;
			this.dataGridViewMetadata.RowTemplate.Height = 24;
			this.dataGridViewMetadata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewMetadata.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMetadata_CellEndEdit);
			this.dataGridViewMetadata.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewMetadata_EditingControlShowing);
			this.dataGridViewMetadata.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewMetadata_KeyDown);
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// dataGridViewTracks
			// 
			this.dataGridViewTracks.AllowUserToAddRows = false;
			this.dataGridViewTracks.AllowUserToDeleteRows = false;
			this.dataGridViewTracks.AllowUserToResizeColumns = false;
			this.dataGridViewTracks.AllowUserToResizeRows = false;
			this.dataGridViewTracks.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dataGridViewTracks.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridViewTracks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridViewTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewTracks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumnTrackNo,
            this.dataGridViewTextBoxColumnTrackTitle,
            this.dataGridViewTextBoxColumnTrackStart,
            this.dataGridViewTextBoxColumnTrackLength});
			resources.ApplyResources(this.dataGridViewTracks, "dataGridViewTracks");
			this.dataGridViewTracks.GridColor = System.Drawing.SystemColors.ControlLight;
			this.dataGridViewTracks.MultiSelect = false;
			this.dataGridViewTracks.Name = "dataGridViewTracks";
			this.dataGridViewTracks.RowHeadersVisible = false;
			this.dataGridViewTracks.RowTemplate.Height = 24;
			this.dataGridViewTracks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewTracks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTracks_CellEndEdit);
			// 
			// dataGridViewTextBoxColumnTrackNo
			// 
			this.dataGridViewTextBoxColumnTrackNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumnTrackNo.Frozen = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumnTrackNo, "dataGridViewTextBoxColumnTrackNo");
			this.dataGridViewTextBoxColumnTrackNo.Name = "dataGridViewTextBoxColumnTrackNo";
			this.dataGridViewTextBoxColumnTrackNo.ReadOnly = true;
			this.dataGridViewTextBoxColumnTrackNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewTextBoxColumnTrackTitle
			// 
			this.dataGridViewTextBoxColumnTrackTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			resources.ApplyResources(this.dataGridViewTextBoxColumnTrackTitle, "dataGridViewTextBoxColumnTrackTitle");
			this.dataGridViewTextBoxColumnTrackTitle.Name = "dataGridViewTextBoxColumnTrackTitle";
			this.dataGridViewTextBoxColumnTrackTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewTextBoxColumnTrackStart
			// 
			this.dataGridViewTextBoxColumnTrackStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			resources.ApplyResources(this.dataGridViewTextBoxColumnTrackStart, "dataGridViewTextBoxColumnTrackStart");
			this.dataGridViewTextBoxColumnTrackStart.Name = "dataGridViewTextBoxColumnTrackStart";
			this.dataGridViewTextBoxColumnTrackStart.ReadOnly = true;
			this.dataGridViewTextBoxColumnTrackStart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewTextBoxColumnTrackLength
			// 
			this.dataGridViewTextBoxColumnTrackLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			resources.ApplyResources(this.dataGridViewTextBoxColumnTrackLength, "dataGridViewTextBoxColumnTrackLength");
			this.dataGridViewTextBoxColumnTrackLength.Name = "dataGridViewTextBoxColumnTrackLength";
			this.dataGridViewTextBoxColumnTrackLength.ReadOnly = true;
			this.dataGridViewTextBoxColumnTrackLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// Item
			// 
			this.Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Item.Frozen = true;
			resources.ApplyResources(this.Item, "Item");
			this.Item.Name = "Item";
			this.Item.ReadOnly = true;
			// 
			// Value
			// 
			this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			resources.ApplyResources(this.Value, "Value");
			this.Value.Name = "Value";
			// 
			// frmChoice
			// 
			this.AcceptButton = this.buttonOk;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "frmChoice";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.frmChoice_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChoice_FormClosing);
			this.Resize += new System.EventHandler(this.frmChoice_Resize);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tableLayoutPanelMeta.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMetadata)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ListView listChoices;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMeta;
		private System.Windows.Forms.DataGridView dataGridViewMetadata;
		private System.Windows.Forms.DataGridView dataGridViewTracks;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnTrackNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnTrackTitle;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnTrackStart;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnTrackLength;
		private System.Windows.Forms.DataGridViewTextBoxColumn Item;
		private System.Windows.Forms.DataGridViewTextBoxColumn Value;
	}
}