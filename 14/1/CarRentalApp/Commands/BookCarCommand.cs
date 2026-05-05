using System.Windows.Forms;

namespace CarRentalApp.Commands
{
    public interface ICommand
    {
        bool CanExecute();
        void Execute();
    }

    public class BookCarCommand : ICommand
    {
        private MainForm _mainForm;

        public BookCarCommand(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            _mainForm.ShowBookingForm();
        }
    }

    public class EditBookingCommand : ICommand
    {
        private MainForm _mainForm;

        public EditBookingCommand(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public bool CanExecute()
        {
            return _mainForm.SelectedBooking != null;
        }

        public void Execute()
        {
            _mainForm.EditSelectedBooking();
        }
    }

    public class CancelBookingCommand : ICommand
    {
        private MainForm _mainForm;

        public CancelBookingCommand(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public bool CanExecute()
        {
            return _mainForm.SelectedBooking != null;
        }

        public void Execute()
        {
            _mainForm.CancelSelectedBooking();
        }
    }
}