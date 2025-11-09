using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LifeHub.MoodJournal;

public partial class MainPage : ContentPage
{
    public ObservableCollection<MoodItem> Moods { get; set; }
    public ICommand AddMoodCommand { get; private set; }
    public ICommand SaveMoodCommand { get; private set; }
    public ICommand DeleteMoodCommand { get; private set; }
    public ICommand ClearAllCommand { get; private set; }

    private string _selectedMoodType = string.Empty;
    private int _totalMoods = 0;
    private double _averageMood = 0;

    public string AverageMood
    {
        get
        {
            if (_averageMood >= 3.5) return "Çok İyi 😄";
            if (_averageMood >= 2.5) return "İyi 🙂";
            if (_averageMood >= 1.5) return "Normal 😐";
            return "Kötü 😔";
        }
    }

    public int TotalMoods
    {
        get => _totalMoods;
        set
        {
            _totalMoods = value;
            OnPropertyChanged();
        }
    }

    public MainPage()
    {
        InitializeComponent();
        
        Moods = new ObservableCollection<MoodItem>();
        
        // ICommand implementations
        AddMoodCommand = new Command<string>(OnMoodSelected);
        SaveMoodCommand = new Command(OnSaveMood);
        DeleteMoodCommand = new Command<MoodItem>(OnDeleteMood);
        ClearAllCommand = new Command(OnClearAll);

        BindingContext = this;

        // Load sample data
        LoadSampleMoods();
        UpdateStatistics();
    }

    private void LoadSampleMoods()
    {
        // En yeni öğeler en üstte olacak şekilde sıralama
        var sampleMoods = new List<MoodItem>
        {
            new MoodItem { MoodType = "Çok İyi", MoodIcon = "😄", Timestamp = DateTime.Now.AddHours(-3), Note = "Harika bir gün geçirdim!", MoodValue = 4 }, // En yeni - en üstte
            new MoodItem { MoodType = "İyi", MoodIcon = "🙂", Timestamp = DateTime.Now.AddDays(-1), Note = "Güzel bir gün", MoodValue = 3 },
            new MoodItem { MoodType = "Normal", MoodIcon = "😐", Timestamp = DateTime.Now.AddDays(-2), MoodValue = 2 } // En eski - en altta
        };

        // En yeni en üstte olacak şekilde ekle (doğrudan Insert(0) ile)
        foreach (var mood in sampleMoods)
        {
            Moods.Insert(0, mood);
        }
    }

    private void OnMoodSelected(string? moodType)
    {
        // Doherty Threshold - Immediate feedback
        if (!string.IsNullOrEmpty(moodType))
        {
            _selectedMoodType = moodType;
            SemanticScreenReader.Announce($"{moodType} ruh hali seçildi");
        }
    }

    private void OnSaveMood(object? obj)
    {
        // Postel's Law - Be liberal in what you accept
        if (string.IsNullOrWhiteSpace(_selectedMoodType))
        {
            DisplayAlert("Uyarı", "Lütfen bir ruh hali seçin.", "Tamam");
            return;
        }

        // Miller's Law - Keep only last 7 items
        if (Moods.Count >= 7)
        {
            Moods.RemoveAt(Moods.Count - 1); // En eski öğeyi (en alttaki) sil
        }

        string note = MoodNoteEntry.Text?.Trim() ?? string.Empty;
        
        var moodValue = _selectedMoodType switch
        {
            "Çok İyi" => 4,
            "İyi" => 3,
            "Normal" => 2,
            "Kötü" => 1,
            _ => 2
        };

        var moodIcon = _selectedMoodType switch
        {
            "Çok İyi" => "😄",
            "İyi" => "🙂",
            "Normal" => "😐",
            "Kötü" => "😔",
            _ => "😐"
        };

        var newMood = new MoodItem
        {
            MoodType = _selectedMoodType,
            MoodIcon = moodIcon,
            Timestamp = DateTime.Now,
            Note = note,
            MoodValue = moodValue
        };

        string savedMoodType = _selectedMoodType;
        Moods.Insert(0, newMood); // En yeni öğeyi en başa ekle
        MoodNoteEntry.Text = string.Empty;
        _selectedMoodType = string.Empty;
        
        UpdateStatistics();
        SemanticScreenReader.Announce($"{savedMoodType} ruh hali kaydedildi");
    }

    private void OnDeleteMood(MoodItem? mood)
    {
        if (mood == null) return;

        bool removed = Moods.Remove(mood);
        if (removed)
        {
            UpdateStatistics();
            SemanticScreenReader.Announce($"{mood.MoodType} ruh hali silindi");
        }
    }

    private async void OnClearAll(object? obj)
    {
        bool confirmed = await DisplayAlert(
            "Onay", 
            "Tüm ruh hali kayıtlarını temizlemek istediğinizden emin misiniz?", 
            "Evet", 
            "Hayır");

        if (confirmed)
        {
            Moods.Clear();
            UpdateStatistics();
            SemanticScreenReader.Announce("Tüm ruh hali kayıtları temizlendi");
        }
    }

    private void UpdateStatistics()
    {
        TotalMoods = Moods.Count;
        
        if (Moods.Count > 0)
        {
            _averageMood = Moods.Average(m => m.MoodValue);
        }
        else
        {
            _averageMood = 0;
        }
        
        OnPropertyChanged(nameof(AverageMood));
    }
}

public class MoodItem
{
    public string MoodType { get; set; } = string.Empty;
    public string MoodIcon { get; set; } = "😐";
    public DateTime Timestamp { get; set; }
    public string Note { get; set; } = string.Empty;
    public int MoodValue { get; set; } = 2;

    public bool HasNote => !string.IsNullOrWhiteSpace(Note);

    public Color MoodColor
    {
        get
        {
            return MoodType switch
            {
                "Çok İyi" => Color.FromRgb(232, 245, 233), // Light Green
                "İyi" => Color.FromRgb(225, 245, 254),      // Light Blue
                "Normal" => Color.FromRgb(255, 249, 196),   // Light Yellow
                "Kötü" => Color.FromRgb(255, 224, 178),    // Light Orange
                _ => Color.FromRgb(245, 245, 245)           // Light Gray
            };
        }
    }
}
