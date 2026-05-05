using System;
using System.Windows.Forms;
using CarRentalApp.Models;

namespace CarRentalApp
{
    public partial class EditBookingForm : Form
    {
        private Booking booking;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Label lblInfo;
        private Button btnSave;
        private Button btnCancel;

        public EditBookingForm(Booking booking)
        {
            this.booking = booking;
            InitializeComponent();

            lblInfo.Text = $"Автомобиль: {booking.Car}\nКлиент: {booking.Customer}";
            dtpStart.Value = booking.StartDate;
            dtpEnd.Value = booking.EndDate;
        }

        private void InitializeComponent()
        {
            this.Text = "Редактирование бронирования";
            this.Size = new System.Drawing.Size(450, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int y = 20;

            // Информация
            lblInfo = new Label
            {
                Left = 20,
                Top = y,
                Width = 390,
                Height = 60,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5)
            };
            this.Controls.Add(lblInfo);
            y += 70;

            // Дата начала
            var lblStart = new Label { Text = "Дата начала:", Left = 20, Top = y, Width = 100 };
            dtpStart = new DateTimePicker { Left = 130, Top = y, Width = 280 };
            this.Controls.Add(lblStart);
            this.Controls.Add(dtpStart);
            y += 35;

            // Дата окончания
            var lblEnd = new Label { Text = "Дата окончания:", Left = 20, Top = y, Width = 100 };
            dtpEnd = new DateTimePicker { Left = 130, Top = y, Width = 280 };
            this.Controls.Add(lblEnd);
            this.Controls.Add(dtpEnd);
            y += 50;

            // Кнопки
            btnSave = new Button { Text = "Сохранить", Left = 250, Top = y, Width = 80 };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button { Text = "Отмена", Left = 340, Top = y, Width = 70 };
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dtpEnd.Value <= dtpStart.Value)
            {
                MessageBox.Show("Дата окончания должна быть позже даты начала.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            booking.StartDate = dtpStart.Value;
            booking.EndDate = dtpEnd.Value;

            this.DialogResult = DialogResult.OK;
        }
    }
}