using System.ComponentModel;

public class Car : INotifyPropertyChanged
{
    private bool isRented;

    public string Name { get; set; } = string.Empty;

    public decimal PricePerDay { get; set; }

    public bool IsRented
    {
        get => isRented;
        set
        {
            isRented = value;
            OnPropertyChanged(nameof(IsRented));
            OnPropertyChanged(nameof(Status));
        }
    }

    public string Status => IsRented ? "Арендован" : "Свободен";

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}