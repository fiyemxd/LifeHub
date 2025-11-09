using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LifeHub.Dashboard;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ActivityItem> RecentActivities { get; set; }
    public ICommand ClearAllCommand { get; private set; }

    public MainPage()
    {
        InitializeComponent();
        
        // En yeni öğeler en üstte olacak şekilde sıralama (en yeni en üstte)
        RecentActivities = new ObservableCollection<ActivityItem>
        {
            new ActivityItem { Icon = "📅", Title = "Planlayıcı", Description = "5 görev tamamlandı", Timestamp = DateTime.Now.AddMinutes(-30) },
            new ActivityItem { Icon = "😊", Title = "Ruh Hali", Description = "Mutlu ruh hali kaydedildi", Timestamp = DateTime.Now.AddHours(-1) },
            new ActivityItem { Icon = "📊", Title = "Alışkanlık Takibi", Description = "Bugün 3 alışkanlık tamamlandı", Timestamp = DateTime.Now.AddHours(-2) }
        };

        ClearAllCommand = new Command(OnClearAll);

        BindingContext = this;
    }

    private void OnHabitTrackerTapped(object? sender, EventArgs e)
    {
        DisplayAlert("Bilgi", "Alışkanlık Takibi uygulamasına yönlendiriliyorsunuz...", "Tamam");
    }

    private void OnMoodJournalTapped(object? sender, EventArgs e)
    {
        DisplayAlert("Bilgi", "Ruh Hali Günlüğü uygulamasına yönlendiriliyorsunuz...", "Tamam");
    }

    private void OnPlannerTapped(object? sender, EventArgs e)
    {
        DisplayAlert("Bilgi", "Günlük Planlayıcı uygulamasına yönlendiriliyorsunuz...", "Tamam");
    }

    private async void OnClearAll(object? obj)
    {
        // Postel's Law - Be conservative in what you send, liberal in what you accept
        if (RecentActivities.Count == 0)
        {
            return;
        }

        bool confirmed = await DisplayAlert(
            "Onay",
            "Tüm aktiviteleri temizlemek istediğinizden emin misiniz?",
            "Evet",
            "Hayır");

        if (confirmed)
        {
            RecentActivities.Clear();
            SemanticScreenReader.Announce("Tüm aktiviteler temizlendi");
        }
    }
}

public class ActivityItem
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
