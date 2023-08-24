namespace QURAANEY
{
    partial class F_INHERATENZ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_INHERATENZ));
            this.pan_btn = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.btn_new = new DevExpress.XtraEditors.SimpleButton();
            this.btn_delete = new DevExpress.XtraEditors.SimpleButton();
            this.btn_clear = new DevExpress.XtraEditors.SimpleButton();
            this.btn_print = new DevExpress.XtraEditors.SimpleButton();
            this.btn_show = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exite = new DevExpress.XtraEditors.SimpleButton();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bar_states = new DevExpress.XtraBars.BarStaticItem();
            this.bar_user_name = new DevExpress.XtraBars.BarStaticItem();
            this.bar_date = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.timer_states_bar = new System.Windows.Forms.Timer(this.components);
            this.timer_date = new System.Windows.Forms.Timer(this.components);
            this.bar_time = new DevExpress.XtraBars.BarStaticItem();
            this.pan_btn.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            this.SuspendLayout();
            // 
            // pan_btn
            // 
            this.pan_btn.Controls.Add(this.flowLayoutPanel1);
            this.pan_btn.Controls.Add(this.btn_exite);
            this.pan_btn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_btn.Location = new System.Drawing.Point(0, 232);
            this.pan_btn.Name = "pan_btn";
            this.pan_btn.Padding = new System.Windows.Forms.Padding(2);
            this.pan_btn.Size = new System.Drawing.Size(830, 41);
            this.pan_btn.TabIndex = 36;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel1.Controls.Add(this.btn_save);
            this.flowLayoutPanel1.Controls.Add(this.btn_new);
            this.flowLayoutPanel1.Controls.Add(this.btn_delete);
            this.flowLayoutPanel1.Controls.Add(this.btn_clear);
            this.flowLayoutPanel1.Controls.Add(this.btn_print);
            this.flowLayoutPanel1.Controls.Add(this.btn_show);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(158, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(514, 37);
            this.flowLayoutPanel1.TabIndex = 41;
            // 
            // btn_save
            // 
            this.btn_save.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_save.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_save.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Appearance.Options.UseBackColor = true;
            this.btn_save.Appearance.Options.UseFont = true;
            this.btn_save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.ImageOptions.Image")));
            this.btn_save.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btn_save.Location = new System.Drawing.Point(433, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(78, 32);
            this.btn_save.TabIndex = 24;
            this.btn_save.Text = "حفظ";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_new
            // 
            this.btn_new.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_new.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_new.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new.Appearance.Options.UseBackColor = true;
            this.btn_new.Appearance.Options.UseFont = true;
            this.btn_new.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_new.ImageOptions.Image")));
            this.btn_new.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btn_new.Location = new System.Drawing.Point(349, 3);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(78, 32);
            this.btn_new.TabIndex = 22;
            this.btn_new.Text = "جديد";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_delete.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_delete.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Appearance.Options.UseBackColor = true;
            this.btn_delete.Appearance.Options.UseFont = true;
            this.btn_delete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_delete.ImageOptions.Image")));
            this.btn_delete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btn_delete.Location = new System.Drawing.Point(265, 3);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(78, 32);
            this.btn_delete.TabIndex = 23;
            this.btn_delete.Text = "حذف";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_clear.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_clear.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clear.Appearance.Options.UseBackColor = true;
            this.btn_clear.Appearance.Options.UseFont = true;
            this.btn_clear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_clear.ImageOptions.Image")));
            this.btn_clear.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btn_clear.Location = new System.Drawing.Point(181, 3);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(78, 32);
            this.btn_clear.TabIndex = 25;
            this.btn_clear.Text = "مسح";
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_print
            // 
            this.btn_print.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_print.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_print.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Appearance.Options.UseBackColor = true;
            this.btn_print.Appearance.Options.UseFont = true;
            this.btn_print.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_print.ImageOptions.Image")));
            this.btn_print.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btn_print.Location = new System.Drawing.Point(97, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(78, 32);
            this.btn_print.TabIndex = 26;
            this.btn_print.Text = "طباعة";
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_show
            // 
            this.btn_show.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_show.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_show.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_show.Appearance.Options.UseBackColor = true;
            this.btn_show.Appearance.Options.UseFont = true;
            this.btn_show.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_show.ImageOptions.Image")));
            this.btn_show.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btn_show.Location = new System.Drawing.Point(13, 3);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(78, 32);
            this.btn_show.TabIndex = 28;
            this.btn_show.Text = "عرض";
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // btn_exite
            // 
            this.btn_exite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_exite.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_exite.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exite.Appearance.Options.UseBackColor = true;
            this.btn_exite.Appearance.Options.UseFont = true;
            this.btn_exite.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_exite.ImageOptions.Image")));
            this.btn_exite.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btn_exite.Location = new System.Drawing.Point(6, 4);
            this.btn_exite.Margin = new System.Windows.Forms.Padding(4);
            this.btn_exite.Name = "btn_exite";
            this.btn_exite.Padding = new System.Windows.Forms.Padding(4);
            this.btn_exite.Size = new System.Drawing.Size(78, 32);
            this.btn_exite.TabIndex = 27;
            this.btn_exite.Text = "خروج";
            this.btn_exite.Click += new System.EventHandler(this.btn_exite_Click);
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bar_states,
            this.bar_user_name,
            this.bar_date,
            this.bar_time});
            this.barManager2.MaxItemId = 4;
            this.barManager2.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bar_states),
            new DevExpress.XtraBars.LinkPersistInfo(this.bar_user_name),
            new DevExpress.XtraBars.LinkPersistInfo(this.bar_date),
            new DevExpress.XtraBars.LinkPersistInfo(this.bar_time)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // bar_states
            // 
            this.bar_states.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.bar_states.Caption = "...";
            this.bar_states.Id = 0;
            this.bar_states.Name = "bar_states";
            this.bar_states.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // bar_user_name
            // 
            this.bar_user_name.Caption = "أهلا : ";
            this.bar_user_name.Id = 1;
            this.bar_user_name.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("user_name.ImageOptions.Image")));
            this.bar_user_name.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("user_name.ImageOptions.LargeImage")));
            this.bar_user_name.Name = "bar_user_name";
            this.bar_user_name.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bar_user_name.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // bar_date
            // 
            this.bar_date.Caption = "...";
            this.bar_date.Id = 2;
            this.bar_date.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("date_time.ImageOptions.Image")));
            this.bar_date.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("date_time.ImageOptions.LargeImage")));
            this.bar_date.Name = "bar_date";
            this.bar_date.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bar_date.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.barManager2;
            this.barDockControl1.Size = new System.Drawing.Size(830, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 273);
            this.barDockControl2.Manager = this.barManager2;
            this.barDockControl2.Size = new System.Drawing.Size(830, 26);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Manager = this.barManager2;
            this.barDockControl3.Size = new System.Drawing.Size(0, 273);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(830, 0);
            this.barDockControl4.Manager = this.barManager2;
            this.barDockControl4.Size = new System.Drawing.Size(0, 273);
            // 
            // timer_states_bar
            // 
            this.timer_states_bar.Interval = 5000;
            this.timer_states_bar.Tick += new System.EventHandler(this.timer_states_bar_Tick);
            // 
            // timer_date
            // 
            this.timer_date.Interval = 1000;
            this.timer_date.Tick += new System.EventHandler(this.timer_date_Tick);
            // 
            // bar_time
            // 
            this.bar_time.Caption = "...";
            this.bar_time.Id = 3;
            this.bar_time.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barStaticItem1.ImageOptions.Image")));
            this.bar_time.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barStaticItem1.ImageOptions.LargeImage")));
            this.bar_time.Name = "bar_time";
            this.bar_time.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // F_INHERATENZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 299);
            this.Controls.Add(this.pan_btn);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "F_INHERATENZ";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "F_INHERATENZ";
            this.Text = "F_INHERATENZ";
            this.Load += new System.EventHandler(this.F_INHERATENZ_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.F_INHERATENZ_KeyDown);
            this.pan_btn.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Panel pan_btn;
        public DevExpress.XtraEditors.SimpleButton btn_print;
        public DevExpress.XtraEditors.SimpleButton btn_clear;
        public DevExpress.XtraEditors.SimpleButton btn_save;
        public DevExpress.XtraEditors.SimpleButton btn_delete;
        public DevExpress.XtraEditors.SimpleButton btn_new;
        public DevExpress.XtraEditors.SimpleButton btn_exite;
        public DevExpress.XtraEditors.SimpleButton btn_show;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem bar_states;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private System.Windows.Forms.Timer timer_states_bar;
        private DevExpress.XtraBars.BarStaticItem bar_user_name;
        private DevExpress.XtraBars.BarStaticItem bar_date;
        private System.Windows.Forms.Timer timer_date;
        private DevExpress.XtraBars.BarStaticItem bar_time;
    }
}