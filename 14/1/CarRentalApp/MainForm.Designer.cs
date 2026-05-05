namespace CarRentalApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.DataGridView dataGridViewBookings;
        private System.Windows.Forms.StatusStrip statusStrip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.dataGridViewBookings = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBookings)).BeginInit();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(900, 24);

            // Меню "Файл"
            var fileMenu = new System.Windows.Forms.ToolStripMenuItem("Файл");
            var exitMenuItem = new System.Windows.Forms.ToolStripMenuItem("Выход");
            exitMenuItem.Click += ExitMenuItem_Click;
            fileMenu.DropDownItems.Add(exitMenuItem);

            // Меню "Автомобили"
            var carsMenu = new System.Windows.Forms.ToolStripMenuItem("Автомобили");
            var showCarsMenuItem = new System.Windows.Forms.ToolStripMenuItem("Список автомобилей");
            showCarsMenuItem.Click += ShowCarsMenuItem_Click;
            carsMenu.DropDownItems.Add(showCarsMenuItem);

            // Меню "Бронирование"
            var bookingMenu = new System.Windows.Forms.ToolStripMenuItem("Бронирование");
            var bookCarMenuItem = new System.Windows.Forms.ToolStripMenuItem("Забронировать автомобиль");
            bookCarMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
            bookCarMenuItem.Click += BookCarMenuItem_Click;

            var editBookingMenuItem = new System.Windows.Forms.ToolStripMenuItem("Редактировать бронь");
            editBookingMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
            editBookingMenuItem.Click += EditBookingMenuItem_Click;

            var cancelBookingMenuItem = new System.Windows.Forms.ToolStripMenuItem("Отменить бронь");
            cancelBookingMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D;
            cancelBookingMenuItem.Click += CancelBookingMenuItem_Click;

            bookingMenu.DropDownItems.Add(bookCarMenuItem);
            bookingMenu.DropDownItems.Add(editBookingMenuItem);
            bookingMenu.DropDownItems.Add(cancelBookingMenuItem);

            // Меню "Настройки"
            var settingsMenu = new System.Windows.Forms.ToolStripMenuItem("Настройки");
            var settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem("Параметры");
            settingsMenuItem.Click += SettingsMenuItem_Click;
            settingsMenu.DropDownItems.Add(settingsMenuItem);

            // Меню "Помощь"
            var helpMenu = new System.Windows.Forms.ToolStripMenuItem("Помощь");
            var aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem("О программе");
            aboutMenuItem.Click += AboutMenuItem_Click;
            helpMenu.DropDownItems.Add(aboutMenuItem);

            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                fileMenu, carsMenu, bookingMenu, settingsMenu, helpMenu
            });

            // toolStrip (панель инструментов)
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(900, 25);

            var btnBook = new System.Windows.Forms.ToolStripButton("Забронировать");
            btnBook.Click += BookCarMenuItem_Click;
            btnBook.ToolTipText = "Забронировать автомобиль (Ctrl+N)";

            var btnEdit = new System.Windows.Forms.ToolStripButton("Редактировать");
            btnEdit.Click += EditBookingMenuItem_Click;
            btnEdit.ToolTipText = "Редактировать бронь (Ctrl+E)";

            var btnCancel = new System.Windows.Forms.ToolStripButton("Отменить");
            btnCancel.Click += CancelBookingMenuItem_Click;
            btnCancel.ToolTipText = "Отменить бронь (Ctrl+D)";

            this.toolStrip.Items.Add(btnBook);
            this.toolStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            this.toolStrip.Items.Add(btnEdit);
            this.toolStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            this.toolStrip.Items.Add(btnCancel);

            // dataGridViewBookings
            this.dataGridViewBookings.AllowUserToAddRows = false;
            this.dataGridViewBookings.AllowUserToDeleteRows = false;
            this.dataGridViewBookings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBookings.Location = new System.Drawing.Point(0, 49);
            this.dataGridViewBookings.Name = "dataGridViewBookings";
            this.dataGridViewBookings.ReadOnly = true;
            this.dataGridViewBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBookings.Size = new System.Drawing.Size(900, 479);

            // statusStrip
            this.statusStrip.Location = new System.Drawing.Point(0, 528);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(900, 22);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.dataGridViewBookings);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Аренда автомобилей";

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBookings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}