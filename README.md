# LifeHub - Kişisel Yaşam Yönetim Platformu

LifeHub, günlük yaşamınızı organize etmenize yardımcı olan bir dizi .NET MAUI uygulamasından oluşan kapsamlı bir kişisel yaşam yönetim platformudur.

## 📱 Uygulamalar

### 🏠 LifeHub.Dashboard
Ana kontrol paneli. Tüm uygulamalardan gelen son aktiviteleri tek bir yerden görüntüleyin.

**Özellikler:**
- Son aktiviteleri görüntüleme
- Hızlı erişim kartları
- Tüm aktiviteleri temizleme

### 📊 LifeHub.HabitTracker
Alışkanlık takip uygulaması. Günlük alışkanlıklarınızı takip edin ve istatistiklerinizi görüntüleyin.

**Özellikler:**
- Alışkanlık ekleme ve silme
- Günlük tamamlanma takibi
- Seri (streak) göstergesi
- Tamamlanma oranı istatistikleri
- Son 7 alışkanlık görüntüleme

### 😊 LifeHub.MoodJournal
Ruh hali günlüğü. Günlük ruh halinizi kaydedin ve geçmişinizi inceleyin.

**Özellikler:**
- 4 farklı ruh hali seçeneği (Çok İyi, İyi, Normal, Kötü)
- Not ekleme
- Ruh hali geçmişi görüntüleme
- Son 7 kayıt görüntüleme

### 📅 LifeHub.Planner
Günlük planlayıcı. Görevlerinizi organize edin ve önceliklendirin.

**Özellikler:**
- Görev ekleme ve silme
- 3 öncelik seviyesi (Yüksek, Orta, Düşük)
- Görev tamamlama takibi
- Tamamlanan görevleri temizleme
- Son 7 görev görüntüleme

## 🎯 Özellikler

### UX Tasarım Prensipleri
Tüm uygulamalar, modern UX yasalarına tam uyumlu olarak tasarlanmıştır:

- ✅ **Fitts's Law**: Büyük ve erişilebilir dokunma alanları
- ✅ **Hick's Law**: Sınırlı ve net seçenekler
- ✅ **Aesthetic–Usability Effect**: Modern ve estetik tasarım
- ✅ **Jakob's Law**: Tanıdık UI desenleri
- ✅ **Gestalt Principles**: İyi gruplandırılmış öğeler
- ✅ **Miller's Law**: Maksimum 7 öğe gösterimi
- ✅ **Von Restorff Effect**: Öne çıkan öğeler
- ✅ **Tesler's Law**: Azaltılmış karmaşıklık
- ✅ **Postel's Law**: Esnek veri kabulü
- ✅ **Doherty Threshold**: Anında geri bildirim

### Platform Desteği
- ✅ Windows 10/11
- ✅ Android (API 21+)

### Teknoloji Stack
- **.NET 8.0**
- **.NET MAUI** (Multi-platform App UI)
- **C#**
- **XAML**

## 🚀 Kurulum

### Gereksinimler
- Visual Studio 2022 (Community, Professional veya Enterprise)
- .NET 8.0 SDK
- .NET MAUI workload
- Windows 10/11 SDK (Windows için)
- Android SDK (Android için)

### Adımlar

1. **Repository'yi klonlayın:**
   ```bash
   git clone <repository-url>
   cd LifeHub
   ```

2. **Visual Studio 2022'de açın:**
   - `LifeHub.sln` dosyasını açın
   - NuGet paketlerinin restore edilmesini bekleyin

3. **.NET MAUI Workload Kontrolü:**
   - Eğer yüklü değilse: **Tools > Get Tools and Features**
   - **Workloads** sekmesinden **.NET Multi-platform App UI development** seçeneğini işaretleyin

4. **Projeyi çalıştırın:**
   - Solution Explorer'da çalıştırmak istediğiniz projeye sağ tıklayın
   - **Set as Startup Project** seçin
   - **F5** tuşuna basın veya yeşil **▶ Start** butonuna tıklayın

Detaylı kurulum rehberi için [VISUAL_STUDIO_REHBER.md](VISUAL_STUDIO_REHBER.md) dosyasına bakabilirsiniz.

## 📖 Kullanım

### Dashboard
Dashboard uygulaması, diğer tüm uygulamalardan gelen son aktiviteleri gösterir. Her kart, ilgili uygulamaya hızlı erişim sağlar.

### HabitTracker
1. Alışkanlık adını girin
2. "Ekle" butonuna tıklayın
3. Alışkanlığı tamamladığınızda checkbox'ı işaretleyin
4. İstatistiklerinizi görüntüleyin

### MoodJournal
1. Ruh halinizi seçin (Çok İyi, İyi, Normal, Kötü)
2. İsteğe bağlı olarak bir not ekleyin
3. "Kaydet" butonuna tıklayın
4. Geçmiş kayıtlarınızı görüntüleyin

### Planner
1. Görev başlığını girin
2. İsteğe bağlı olarak açıklama ekleyin
3. Öncelik seviyesini seçin (Yüksek, Orta, Düşük)
4. "Ekle" butonuna tıklayın
5. Görevi tamamladığınızda checkbox'ı işaretleyin

## 🏗️ Proje Yapısı

```
LifeHub/
├── LifeHub.Dashboard/          # Ana dashboard uygulaması
├── LifeHub.HabitTracker/       # Alışkanlık takip uygulaması
├── LifeHub.MoodJournal/        # Ruh hali günlüğü
├── LifeHub.Planner/            # Günlük planlayıcı
├── LifeHub.sln                 # Solution dosyası
├── UX_YASALARI_ANALIZ.md      # UX yasaları uyumluluk analizi
└── VISUAL_STUDIO_REHBER.md    # Visual Studio kurulum rehberi
```

## 🎨 Tasarım

Her uygulama, kendine özgü bir renk şemasına sahiptir:
- **Dashboard**: Mor (#512BD4)
- **HabitTracker**: Yeşil (#4CAF50)
- **MoodJournal**: Turuncu (#FF9800)
- **Planner**: Mavi (#2196F3)

Tüm uygulamalar modern Material Design prensiplerini takip eder ve yuvarlatılmış köşeler, gradient efektler ve tutarlı spacing kullanır.

## 📊 UX Yasaları Analizi

Projenin UX yasalarına uyumluluğu hakkında detaylı bilgi için [UX_YASALARI_ANALIZ.md](UX_YASALARI_ANALIZ.md) dosyasına bakabilirsiniz.

## 🔧 Geliştirme

### Build
```bash
dotnet build LifeHub.sln
```

### Restore NuGet Packages
```bash
dotnet restore LifeHub.sln
```

### Clean Solution
```bash
dotnet clean LifeHub.sln
```

## 📝 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## 🤝 Katkıda Bulunma

Katkılarınızı bekliyoruz! Lütfen önce bir issue açın veya pull request göndermeden önce değişikliklerinizi tartışın.

## 📧 İletişim

Sorularınız veya önerileriniz için lütfen bir issue açın.

## 🙏 Teşekkürler

Bu proje, modern UX prensipleri ve .NET MAUI teknolojileri kullanılarak geliştirilmiştir.

---

**Not**: Her uygulama bağımsız olarak çalışır ve kendi verilerini yönetir. Şu anda veriler uygulama kapatıldığında kaybolur (geçici bellek). Kalıcı veri depolama özellikleri gelecek sürümlerde eklenecektir.

