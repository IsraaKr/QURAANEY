namespace QURAANEY.NASHAT
{
    partial class F_REP_NASHAT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_REP_NASHAT));
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lkp_nashat_name = new DevExpress.XtraEditors.LookUpEdit();
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tileControl11 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.ti_true = new DevExpress.XtraEditors.TileItem();
            this.ti_false = new DevExpress.XtraEditors.TileItem();
            this.ti_nashat_count = new DevExpress.XtraEditors.TileItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.pan_btn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkp_nashat_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // pan_btn
            // 
            this.pan_btn.Location = new System.Drawing.Point(0, 387);
            this.pan_btn.Size = new System.Drawing.Size(800, 41);
            // 
            // btn_exite
            // 
            this.btn_exite.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_exite.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exite.Appearance.Options.UseBackColor = true;
            this.btn_exite.Appearance.Options.UseFont = true;
            this.btn_exite.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_exite.ImageOptions.Image")));
            this.btn_exite.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lkp_nashat_name);
            this.layoutControl1.Controls.Add(this.gc);
            this.layoutControl1.Controls.Add(this.tileControl11);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(800, 428);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lkp_nashat_name
            // 
            this.lkp_nashat_name.Location = new System.Drawing.Point(303, 28);
            this.lkp_nashat_name.Name = "lkp_nashat_name";
            this.lkp_nashat_name.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkp_nashat_name.Properties.NullText = "";
            this.lkp_nashat_name.Size = new System.Drawing.Size(485, 20);
            this.lkp_nashat_name.StyleController = this.layoutControl1;
            this.lkp_nashat_name.TabIndex = 106;
            this.lkp_nashat_name.EditValueChanged += new System.EventHandler(this.lkp_nashat_name_EditValueChanged);
            // 
            // gc
            // 
            this.gc.Location = new System.Drawing.Point(303, 52);
            this.gc.MainView = this.gv;
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(485, 364);
            this.gc.TabIndex = 105;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Appearance.EvenRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gv.Appearance.EvenRow.Options.UseBackColor = true;
            this.gv.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv.Appearance.Row.Options.UseTextOptions = true;
            this.gv.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv.GridControl = this.gc;
            this.gv.Name = "gv";
            this.gv.OptionsBehavior.Editable = false;
            this.gv.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.True;
            this.gv.OptionsFind.AlwaysVisible = true;
            this.gv.OptionsView.EnableAppearanceEvenRow = true;
            this.gv.OptionsView.ShowAutoFilterRow = true;
            // 
            // tileControl11
            // 
            this.tileControl11.Groups.Add(this.tileGroup2);
            this.tileControl11.ItemBorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
            this.tileControl11.ItemPadding = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.tileControl11.Location = new System.Drawing.Point(12, 12);
            this.tileControl11.MaxId = 15;
            this.tileControl11.Name = "tileControl11";
            this.tileControl11.Padding = new System.Windows.Forms.Padding(0);
            this.tileControl11.Size = new System.Drawing.Size(287, 404);
            this.tileControl11.TabIndex = 93;
            this.tileControl11.Text = "tileControl1";
            this.tileControl11.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Center;
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.ti_true);
            this.tileGroup2.Items.Add(this.ti_false);
            this.tileGroup2.Items.Add(this.ti_nashat_count);
            this.tileGroup2.Name = "tileGroup2";
            this.tileGroup2.Text = "التسليم";
            // 
            // ti_true
            // 
            this.ti_true.AppearanceItem.Normal.BackColor = System.Drawing.Color.SteelBlue;
            this.ti_true.AppearanceItem.Normal.Options.UseBackColor = true;
            tileItemElement1.AnchorAlignment = DevExpress.Utils.AnchorAlignment.Top;
            tileItemElement1.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileItemElement1.Appearance.Normal.Options.UseFont = true;
            tileItemElement1.Text = "غدد الحضور";
            tileItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileItemElement2.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileItemElement2.Appearance.Normal.Options.UseFont = true;
            tileItemElement2.Text = "...";
            tileItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.ti_true.Elements.Add(tileItemElement1);
            this.ti_true.Elements.Add(tileItemElement2);
            this.ti_true.Id = 6;
            this.ti_true.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.ti_true.Name = "ti_true";
            // 
            // ti_false
            // 
            this.ti_false.AppearanceItem.Normal.BackColor = System.Drawing.Color.SteelBlue;
            this.ti_false.AppearanceItem.Normal.Options.UseBackColor = true;
            tileItemElement3.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileItemElement3.Appearance.Normal.Options.UseFont = true;
            tileItemElement3.Text = "عدد الغياب";
            tileItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileItemElement4.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileItemElement4.Appearance.Normal.Options.UseFont = true;
            tileItemElement4.Text = "...";
            tileItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.ti_false.Elements.Add(tileItemElement3);
            this.ti_false.Elements.Add(tileItemElement4);
            this.ti_false.Id = 8;
            this.ti_false.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.ti_false.Name = "ti_false";
            // 
            // ti_nashat_count
            // 
            this.ti_nashat_count.AppearanceItem.Normal.BackColor = System.Drawing.Color.SteelBlue;
            this.ti_nashat_count.AppearanceItem.Normal.Options.UseBackColor = true;
            tileItemElement5.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            tileItemElement5.Appearance.Normal.Options.UseFont = true;
            tileItemElement5.Text = "عدد النشاطات ";
            tileItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileItemElement6.Text = "...";
            tileItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.ti_nashat_count.Elements.Add(tileItemElement5);
            this.ti_nashat_count.Elements.Add(tileItemElement6);
            this.ti_nashat_count.Id = 14;
            this.ti_nashat_count.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.ti_nashat_count.Name = "ti_nashat_count";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(800, 428);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gc;
            this.layoutControlItem2.Location = new System.Drawing.Point(291, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(489, 368);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tileControl11;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(291, 408);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lkp_nashat_name;
            this.layoutControlItem3.Location = new System.Drawing.Point(291, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(489, 40);
            this.layoutControlItem3.Text = "اختر نشاط";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(47, 13);
            // 
            // F_REP_NASHAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.layoutControl1);
            this.Name = "F_REP_NASHAT";
            this.Text = "F_REP_NASHAT";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.pan_btn, 0);
            this.pan_btn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lkp_nashat_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TileControl tileControl11;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem ti_true;
        private DevExpress.XtraEditors.TileItem ti_false;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LookUpEdit lkp_nashat_name;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TileItem ti_nashat_count;
    }
}