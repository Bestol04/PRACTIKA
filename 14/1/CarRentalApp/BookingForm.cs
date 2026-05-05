using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CarRentalApp.Models;

namespace CarRentalApp
{
    public partial class BookingForm : Form
    {
        public Booking CreatedBooking { get; private set; }

        private ComboBox cmbCar;
        private ComboBox cmbCustomer;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Label lblTotalPrice;
        private Button btnBook;
        private Button btnCancel;

        public BookingForm(List<Car> cars, List<Customer> customers)
        {
            InitializeComponent();

            cmbCar.DataSource = cars;
            cmbCustomer.DataSource = customers;

            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today.AddDays(1);

            dtpStart.ValueChanged += DateChanged;
            dtpEnd.ValueChanged += DateChanged;
            cmbCar.SelectedIndexChanged += DateChanged;

            CalculateTotalPrice();
        }

        private void InitializeComponent()
        {
            this.Text = "Новое бронирование";
            this.Size = new System.Drawing.Size(450, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int y = 20;

            // Автомобиль
            var lblCar = new Label { Text = "Автомобиль:", Left = 20, Top = y, Width = 100 };
            cmbCar = new ComboBox { Left = 130, Top = y, Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            this.Controls.Add(lblCar);
            this.Controls.Add(cmbCar);
            y += 35;

            // Клиент
            var lblCustomer = new Label { Text = "Клиент:", Left = 20, Top = y, Width = 100 };
            cmbCustomer = new ComboBox { Left = 130, Top = y, Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            this.Controls.Add(lblCustomer);
            this.Controls.Add(cmbCustomer);
            y += 35;

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
            y += 35;

            // Общая стоимость
            lblTotalPrice = new Label
            {
                Text = "Общая стоимость: 0 руб.",
                Left = 20,
                Top = y,
                Width = 390,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(lblTotalPrice);
            y += 50;

            // Кнопки
            btnBook = new Button { Text = "Забронировать", Left = 210, Top = y, Width = 120 };
            btnBook.Click += BtnBook_Click;

            btnCancel = new Button { Text = "Отмена", Left = 340, Top = y, Width = 70 };
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.Add(btnBook);
            this.Controls.Add(btnCancel);
        }

        private void DateChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            if (cmbCar.SelectedItem is Car car && dtpEnd.Value > dtpStart.Value)
            {
                int days = (dtpEnd.Value - dtpStart.Value).Days;
                decimal total = days * car.PricePerDay;
                lblTotalPrice.Text = $"Общая стоимость: {total:C} ({days} дн.)";
            }
            else
            {
                lblTotalPrice.Text = "Общая стоимость: 0 руб.";
            }
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            if (cmbCar.SelectedItem == null)
            {
                MessageBox.Show("Выберите автомобиль.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCustomer.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpEnd.Value <= dtpStart.Value)
            {
                MessageBox.Show("Дата окончания должна быть позже даты начала.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CreatedBooking = new Booking
            {
                Car = (Car)cmbCar.SelectedItem,
                Customer = (Customer)cmbCustomer.SelectedItem,
                StartDate = dtpStart.Value,
                EndDate = dtpEnd.Value
            };

            this.DialogResult = DialogResult.OK;
        }
    }
}