using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LifeHub.Planner;

public partial class MainPage : ContentPage
{
    public ObservableCollection<TaskItem> Tasks { get; set; }
    public ICommand AddTaskCommand { get; private set; }
    public ICommand SetPriorityCommand { get; private set; }
    public ICommand DeleteTaskCommand { get; private set; }
    public ICommand ClearCompletedCommand { get; private set; }

    private string _selectedPriority = "Orta";
    private int _totalTasks = 0;
    private int _completedTasks = 0;

    public int TotalTasks
    {
        get => _totalTasks;
        set
        {
            _totalTasks = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CompletionRate));
        }
    }

    public int CompletedTasks
    {
        get => _completedTasks;
        set
        {
            _completedTasks = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CompletionRate));
        }
    }

    public double CompletionRate
    {
        get => TotalTasks > 0 ? Math.Round((CompletedTasks / (double)TotalTasks) * 100, 1) : 0;
    }

    public MainPage()
    {
        InitializeComponent();
        
        Tasks = new ObservableCollection<TaskItem>();
        
        // ICommand implementations
        AddTaskCommand = new Command(OnAddTask);
        SetPriorityCommand = new Command<string>(OnSetPriority);
        DeleteTaskCommand = new Command<TaskItem>(OnDeleteTask);
        ClearCompletedCommand = new Command(OnClearCompleted);

        BindingContext = this;

        // Load sample data
        LoadSampleTasks();
        UpdateStatistics();
    }

    private void LoadSampleTasks()
    {
        // En yeni öğeler en üstte olacak şekilde sıralama
        var sampleTasks = new List<TaskItem>
        {
            new TaskItem 
            { 
                Title = "Kitap okuma", 
                Priority = "Düşük",
                Timestamp = DateTime.Now.AddMinutes(-30),
                IsCompleted = false
            }, // En yeni - en üstte
            new TaskItem 
            { 
                Title = "Alışveriş listesi", 
                Description = "Market alışverişi",
                Priority = "Orta",
                Timestamp = DateTime.Now.AddHours(-1),
                IsCompleted = true
            },
            new TaskItem 
            { 
                Title = "Proje sunumu hazırla", 
                Description = "PowerPoint sunumu ve notlar",
                Priority = "Yüksek",
                Timestamp = DateTime.Now.AddHours(-2),
                IsCompleted = false
            } // En eski - en altta
        };

        // En yeni en üstte olacak şekilde ekle (doğrudan Insert(0) ile)
        foreach (var task in sampleTasks)
        {
            Tasks.Insert(0, task);
        }
    }

    private void OnAddTask(object? obj)
    {
        // Postel's Law - Be liberal in what you accept
        string title = TaskTitleEntry.Text?.Trim() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(title))
        {
            DisplayAlert("Uyarı", "Lütfen bir görev başlığı girin.", "Tamam");
            return;
        }

        // Miller's Law - Keep only last 7 items
        if (Tasks.Count >= 7)
        {
            Tasks.RemoveAt(Tasks.Count - 1); // En eski öğeyi (en alttaki) sil
        }

        string description = TaskDescriptionEntry.Text?.Trim() ?? string.Empty;

        var newTask = new TaskItem
        {
            Title = title,
            Description = description,
            Priority = _selectedPriority,
            Timestamp = DateTime.Now,
            IsCompleted = false
        };

        Tasks.Insert(0, newTask); // En yeni öğeyi en başa ekle
        TaskTitleEntry.Text = string.Empty;
        TaskDescriptionEntry.Text = string.Empty;
        _selectedPriority = "Orta";
        
        UpdateStatistics();
        SemanticScreenReader.Announce($"{title} görevi eklendi");
    }

    private void OnSetPriority(string? priority)
    {
        if (!string.IsNullOrEmpty(priority))
        {
            _selectedPriority = priority;
            // Doherty Threshold - Immediate feedback
            SemanticScreenReader.Announce($"{priority} öncelik seçildi");
        }
    }

    private void OnDeleteTask(TaskItem? task)
    {
        if (task == null) return;

        bool removed = Tasks.Remove(task);
        if (removed)
        {
            UpdateStatistics();
            SemanticScreenReader.Announce($"{task.Title} görevi silindi");
        }
    }

    private async void OnClearCompleted(object? obj)
    {
        var completedTasks = Tasks.Where(t => t.IsCompleted).ToList();
        
        if (completedTasks.Count == 0)
        {
            await DisplayAlert("Bilgi", "Tamamlanan görev bulunmuyor.", "Tamam");
            return;
        }

        bool confirmed = await DisplayAlert(
            "Onay", 
            $"{completedTasks.Count} tamamlanan görevi temizlemek istediğinizden emin misiniz?", 
            "Evet", 
            "Hayır");

        if (confirmed)
        {
            // Güvenli silme - ters sırada silme
            for (int i = Tasks.Count - 1; i >= 0; i--)
            {
                if (Tasks[i].IsCompleted)
                {
                    Tasks.RemoveAt(i);
                }
            }
            UpdateStatistics();
            SemanticScreenReader.Announce("Tamamlanan görevler temizlendi");
        }
    }

    private void OnClearEntriesClicked(object? sender, EventArgs e)
    {
        TaskTitleEntry.Text = string.Empty;
        TaskDescriptionEntry.Text = string.Empty;
        _selectedPriority = "Orta";
    }

    private void OnTaskCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is TaskItem task)
        {
            task.IsCompleted = e.Value;
            
            if (e.Value)
            {
                task.CompletedAt = DateTime.Now;
            }
            
            UpdateStatistics();
            
            // Doherty Threshold - Immediate feedback
            string message = e.Value ? $"{task.Title} tamamlandı!" : $"{task.Title} işaretlemesi kaldırıldı";
            SemanticScreenReader.Announce(message);
        }
    }

    private void UpdateStatistics()
    {
        TotalTasks = Tasks.Count;
        CompletedTasks = Tasks.Count(t => t.IsCompleted);
    }
}

public class TaskItem : BindableObject
{
    private bool _isCompleted;

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = "Orta";
    public DateTime Timestamp { get; set; }
    public DateTime? CompletedAt { get; set; }

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            _isCompleted = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TextColor));
            OnPropertyChanged(nameof(TextDecoration));
        }
    }

    public bool HasDescription => !string.IsNullOrWhiteSpace(Description);

    public Color TextColor => IsCompleted ? Color.FromRgb(150, 150, 150) : Color.FromRgb(51, 51, 51);
    public TextDecorations TextDecoration => IsCompleted ? TextDecorations.Strikethrough : TextDecorations.None;

    public Color PriorityColor
    {
        get
        {
            return Priority switch
            {
                "Yüksek" => Color.FromRgb(244, 67, 54),   // Red
                "Orta" => Color.FromRgb(255, 152, 0),     // Orange
                "Düşük" => Color.FromRgb(76, 175, 80),    // Green
                _ => Color.FromRgb(158, 158, 158)          // Gray
            };
        }
    }
}
