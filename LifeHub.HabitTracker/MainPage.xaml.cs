using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Globalization;

namespace LifeHub.HabitTracker;

public partial class MainPage : ContentPage
{
    public ObservableCollection<HabitItem> Habits { get; set; }
    public ICommand AddHabitCommand { get; private set; }
    public ICommand DeleteHabitCommand { get; private set; }
    public ICommand ResetAllCommand { get; private set; }

    private int _totalHabits = 0;
    private int _completedHabits = 0;

    public int TotalHabits
    {
        get => _totalHabits;
        set
        {
            _totalHabits = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CompletionRate));
        }
    }

    public int CompletedHabits
    {
        get => _completedHabits;
        set
        {
            _completedHabits = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CompletionRate));
        }
    }

    public double CompletionRate
    {
        get => TotalHabits > 0 ? Math.Round((CompletedHabits / (double)TotalHabits) * 100, 1) : 0;
    }

    public MainPage()
    {
        InitializeComponent();
        
        Habits = new ObservableCollection<HabitItem>();
        
        // ICommand implementations
        AddHabitCommand = new Command(OnAddHabit);
        DeleteHabitCommand = new Command<HabitItem>(OnDeleteHabit);
        ResetAllCommand = new Command(OnResetAll);

        BindingContext = this;

        // Load sample data
        LoadSampleHabits();
        UpdateStatistics();
    }

    private void LoadSampleHabits()
    {
        // Miller's Law - Show only last 7 items
        // En yeni öğeler en üstte olacak şekilde sıralama
        var sampleHabits = new List<HabitItem>
        {
            new HabitItem { Name = "Su İçmek (2L)", Timestamp = DateTime.Now.AddHours(-5), IsCompleted = true, CompletionCount = 7 }, // En yeni - en üstte
            new HabitItem { Name = "Kitap Okumak", Timestamp = DateTime.Now.AddDays(-1), IsCompleted = false, CompletionCount = 3 },
            new HabitItem { Name = "Sabah Egzersizi", Timestamp = DateTime.Now.AddDays(-2), IsCompleted = true, CompletionCount = 5 } // En eski - en altta
        };

        // En yeni en üstte olacak şekilde ekle (doğrudan Insert(0) ile)
        foreach (var habit in sampleHabits)
        {
            Habits.Insert(0, habit);
        }
    }

    private void OnAddHabit(object? obj)
    {
        // Postel's Law - Be liberal in what you accept
        string habitName = HabitNameEntry.Text?.Trim() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(habitName))
        {
            DisplayAlert("Uyarı", "Lütfen bir alışkanlık adı girin.", "Tamam");
            return;
        }

        // Miller's Law - Keep only last 7 items
        if (Habits.Count >= 7)
        {
            Habits.RemoveAt(Habits.Count - 1); // En eski öğeyi (en alttaki) sil
        }

        var newHabit = new HabitItem
        {
            Name = habitName,
            Timestamp = DateTime.Now,
            IsCompleted = false,
            CompletionCount = 0
        };

        Habits.Insert(0, newHabit); // En yeni öğeyi en başa ekle
        HabitNameEntry.Text = string.Empty;
        
        UpdateStatistics();
        SemanticScreenReader.Announce($"{habitName} alışkanlığı eklendi");
    }

    private void OnDeleteHabit(HabitItem? habit)
    {
        if (habit == null) return;

        bool removed = Habits.Remove(habit);
        if (removed)
        {
            UpdateStatistics();
            SemanticScreenReader.Announce($"{habit.Name} alışkanlığı silindi");
        }
    }

    private async void OnResetAll(object? obj)
    {
        bool confirmed = await DisplayAlert(
            "Onay", 
            "Tüm alışkanlıkları sıfırlamak istediğinizden emin misiniz?", 
            "Evet", 
            "Hayır");

        if (confirmed)
        {
            Habits.Clear();
            UpdateStatistics();
            SemanticScreenReader.Announce("Tüm alışkanlıklar sıfırlandı");
        }
    }

    private void OnClearEntryClicked(object? sender, EventArgs e)
    {
        HabitNameEntry.Text = string.Empty;
    }

    private void OnHabitCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is HabitItem habit)
        {
            habit.IsCompleted = e.Value;
            
            if (e.Value)
            {
                habit.CompletionCount++;
                habit.LastCompleted = DateTime.Now;
            }
            
            UpdateStatistics();
            
            // Doherty Threshold - Immediate feedback
            string message = e.Value ? $"{habit.Name} tamamlandı!" : $"{habit.Name} işaretlemesi kaldırıldı";
            SemanticScreenReader.Announce(message);
        }
    }

    private void UpdateStatistics()
    {
        TotalHabits = Habits.Count;
        CompletedHabits = Habits.Count(h => h.IsCompleted);
    }
}

public class HabitItem : BindableObject
{
    private bool _isCompleted;
    private int _completionCount;

    public string Name { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    
    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            _isCompleted = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TextColor));
            OnPropertyChanged(nameof(TextDecoration));
            OnPropertyChanged(nameof(CompletionInfo));
        }
    }

    public int CompletionCount
    {
        get => _completionCount;
        set
        {
            _completionCount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasStreak));
            OnPropertyChanged(nameof(StreakText));
        }
    }

    public DateTime? LastCompleted { get; set; }

    public Color TextColor => IsCompleted ? Color.FromRgb(150, 150, 150) : Color.FromRgb(51, 51, 51);
    public TextDecorations TextDecoration => IsCompleted ? TextDecorations.Strikethrough : TextDecorations.None;
    
    public string CompletionInfo => IsCompleted && LastCompleted.HasValue 
        ? $"Tamamlandı: {LastCompleted.Value:dd MMM yyyy HH:mm}" 
        : string.Empty;

    public bool HasStreak => CompletionCount >= 3;
    
    public string StreakText => $"🔥 {CompletionCount} günlük seri! Harika gidiyorsunuz!";
}
