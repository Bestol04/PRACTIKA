using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CarRentalApp.Commands;
using CarRentalApp.Models;

namespace CarRentalApp
{
    public partial class MainForm : Form
    {
        private List<Car> cars = new List<Car>();
        private List<Customer> customers = new List<Customer>();
        private BindingList<Booking> bookings = new BindingList<Booking>();

        private BookCarCommand bookCarCommand;
        private EditBookingCommand editBookingCommand;
        private CancelBookingCommand cancelBookingCommand;

        public Booking SelectedBooking
        {
            get
            {
                if (dataGridViewBookings.SelectedRows.Count > 0)
                {
                    return dataGridViewBookings.SelectedRows[0].DataBoundItem as Booking;
                }
                return null;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeData();
            SetupDataGrid();
            InitializeCommands();
            UpdateStatusBar();

            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
        }

        private void InitializeCommands()
        {
            bookCarCommand = new BookCarCommand(this);
            editBookingCommand = new EditBookingCommand(this);
            cancelBookingCommand = new CancelBookingCommand(this);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                bookCarCommand.Execute();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (editBookingCommand.CanExecute())
                    editBookingCommand.Execute();
                e.Handled = true;
            }
            else if ((e.Control && e.KeyCode == Keys.D) || e.KeyCode == Keys.Delete)
            {
                if (cancelBookingCommand.CanExecute())
                    cancelBookingCommand.Execute();
                e.Handled = true;
            }
        }

        private void InitializeData()
        {

            cars.Add(new Car { Id = 1, Brand = "Toyota", Model = "Camry", PricePerDay = 2500, IsAvailable = true });
            cars.Add(new Car { Id = 2, Brand = "BMW", Model = "X5", PricePerDay = 5000, IsAvailable = true });
            cars.Add(new Car { Id = 3, Brand = "Mercedes", Model = "E-Class", PricePerDay = 4500, IsAvailable = true });
            cars.Add(new Car { Id = 4, Brand = "Audi", Model = "A6", PricePerDay = 4000, IsAvailable = true });
            cars.Add(new Car { Id = 5, Brand = "Volkswagen", Model = "Polo", PricePerDay = 1800, IsAvailable = true });

            customers.Add(new Customer { Id = 1, Name = "Иванов Иван", Phone = "+7 (999) 123-45-67" });
            customers.Add(new Customer { Id = 2, Name = "Петров Петр", Phone = "+7 (999) 234-56-78" });
            customers.Add(new Customer { Id = 3, Name = "Сидорова Анна", Phone = "+7 (999) 345-67-89" });
        }

        private void SetupDataGrid()
        {
            dataGridViewBookings.DataSource = bookings;
            dataGridViewBookings.Columns.Clear();

            dataGridViewBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            var carColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Автомобиль",
                Width = 200
            };
            carColumn.DataPropertyName = "Car";
            dataGridViewBookings.Columns.Add(carColumn);

            var customerColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Клиент",
                Width = 150
            };
            customerColumn.DataPropertyName = "Customer";
            dataGridViewBookings.Columns.Add(customerColumn);

            dataGridViewBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StartDate",
                HeaderText = "Начало",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
            });

            dataGridViewBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EndDate",
                HeaderText = "Окончание",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
            });

            dataGridViewBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Days",
                HeaderText = "Дней",
                Width = 60
            });

            dataGridViewBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPrice",
                HeaderText = "Сумма",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }
            });

            dataGridViewBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Статус",
                Width = 100
            });
        }

        private void UpdateStatusBar()
        {
            statusStrip.Items.Clear();
            statusStrip.Items.Add($"Всего бронирований: {bookings.Count}");
            statusStrip.Items.Add(new ToolStripSeparator());
            statusStrip.Items.Add($"Автомобилей: {cars.Count}");
            statusStrip.Items.Add(new ToolStripSeparator());
            statusStrip.Items.Add($"Доступно: {cars.Count(c => c.IsAvailable)}");
        }

        public void ShowBookingForm()
        {
            var availableCars = cars.Where(c => c.IsAvailable).ToList();
            if (availableCars.Count == 0)
            {
                MessageBox.Show("Нет доступных автомобилей для аренды.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var bookingForm = new BookingForm(availableCars, customers);
            if (bookingForm.ShowDialog() == DialogResult.OK)
            {
                var booking = bookingForm.CreatedBooking;
                booking.Id = bookings.Count + 1;
                booking.Status = "Активно";

                // Рассчитываем стоимость
                int days = (booking.EndDate - booking.StartDate).Days;
                booking.TotalPrice = days * booking.Car.PricePerDay;

                // Помечаем автомобиль как занятый
                booking.Car.IsAvailable = false;

                bookings.Add(booking);
                UpdateStatusBar();

                MessageBox.Show("Бронирование успешно создано!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void EditSelectedBooking()
        {
            if (SelectedBooking == null)
            {
                MessageBox.Show("Выберите бронирование для редактирования.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var editForm = new EditBookingForm(SelectedBooking);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                int days = (SelectedBooking.EndDate - SelectedBooking.StartDate).Days;
                SelectedBooking.TotalPrice = days * SelectedBooking.Car.PricePerDay;

                dataGridViewBookings.Refresh();
                MessageBox.Show("Бронирование успешно обновлено!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CancelSelectedBooking()
        {
            if (SelectedBooking == null)
            {
                MessageBox.Show("Выберите бронирование для отмены.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Вы уверены, что хотите отменить бронирование автомобиля {SelectedBooking.Car}?",
                "Подтверждение отмены",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SelectedBooking.Car.IsAvailable = true;
                bookings.Remove(SelectedBooking);
                UpdateStatusBar();

                MessageBox.Show("Бронирование отменено.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowCarsMenuItem_Click(object sender, EventArgs e)
        {
            string carsList = string.Join("\n", cars.Select(c =>
                $"{c} - {(c.IsAvailable ? "Доступен" : "Занят")}"));
            MessageBox.Show(carsList, "Список автомобилей",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BookCarMenuItem_Click(object sender, EventArgs e)
        {
            bookCarCommand.Execute();
        }

        private void EditBookingMenuItem_Click(object sender, EventArgs e)
        {
            if (editBookingCommand.CanExecute())
                editBookingCommand.Execute();
        }

        private void CancelBookingMenuItem_Click(object sender, EventArgs e)
        {
            if (cancelBookingCommand.CanExecute())
                cancelBookingCommand.Execute();
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Настройки приложения", "Настройки",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Система управления арендой автомобилей\nВерсия 1.0\n© 2024",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}