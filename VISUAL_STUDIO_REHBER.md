# Visual Studio 2022'de LifeHub Projelerini Çalıştırma Rehberi

## 📋 Gereksinimler

- Visual Studio 2022 (Community, Professional veya Enterprise)
- .NET MAUI workload yüklü olmalı
- Windows 10/11 SDK

## 🚀 Adım Adım Kurulum ve Çalıştırma

### 1. Solution Dosyasını Açma

1. **Visual Studio 2022'yi açın**
2. **File > Open > Project/Solution** menüsüne tıklayın (veya `Ctrl+Shift+O`)
3. `LifeHub.sln` dosyasını seçin ve **Open** butonuna tıklayın
4. Solution Explorer'da 4 proje görünecek:
   - LifeHub.Dashboard
   - LifeHub.HabitTracker
   - LifeHub.MoodJournal
   - LifeHub.Planner

### 2. .NET MAUI Workload Kontrolü

Eğer projeler yüklenemiyorsa:

1. **Tools > Get Tools and Features** menüsüne gidin
2. **Individual components** sekmesine tıklayın
3. Şunları kontrol edin:
   - ✅ .NET Multi-platform App UI development
   - ✅ .NET Desktop development
   - ✅ Windows 10 SDK (en son sürüm)

### 3. Projeleri Restore Etme

1. Solution Explorer'da **LifeHub** solution'ına sağ tıklayın
2. **Restore NuGet Packages** seçeneğine tıklayın
3. Veya **Build > Restore NuGet Packages** menüsünü kullanın

### 4. Bir Projeyi Çalıştırma

#### Yöntem 1: Solution Explorer'dan

1. Solution Explorer'da çalıştırmak istediğiniz projeye sağ tıklayın
   - Örnek: **LifeHub.Dashboard**
2. **Set as Startup Project** seçeneğine tıklayın
3. Üst menüden **Debug** modunu seçin (veya **Release**)
4. Platform olarak **Windows Machine** seçin
5. **F5** tuşuna basın veya yeşil **▶ Start** butonuna tıklayın

#### Yöntem 2: Toolbar'dan

1. Üst toolbar'da **Startup Projects** dropdown'ından projeyi seçin
2. Platform olarak **Windows Machine** seçin
3. **▶ Start** butonuna tıklayın

### 5. Her Projeyi Ayrı Ayrı Çalıştırma

Her proje bağımsız bir uygulamadır. Sırayla test edebilirsiniz:

#### Dashboard Uygulaması
1. **LifeHub.Dashboard** projesine sağ tıklayın
2. **Set as Startup Project** seçin
3. **F5** ile çalıştırın

#### HabitTracker Uygulaması
1. **LifeHub.HabitTracker** projesine sağ tıklayın
2. **Set as Startup Project** seçin
3. **F5** ile çalıştırın

#### MoodJournal Uygulaması
1. **LifeHub.MoodJournal** projesine sağ tıklayın
2. **Set as Startup Project** seçin
3. **F5** ile çalıştırın

#### Planner Uygulaması
1. **LifeHub.Planner** projesine sağ tıklayın
2. **Set as Startup Project** seçin
3. **F5** ile çalıştırın

## 🔧 Olası Sorunlar ve Çözümleri

### Sorun 1: "The workload 'net8.0-ios' is out of support" Hatası

**Çözüm:** Bu normaldir. Projeler Windows ve Android için yapılandırılmıştır. iOS/MacCatalyst desteği kaldırılmıştır.

### Sorun 2: Build Hatası - NuGet Packages

**Çözüm:**
1. **Tools > NuGet Package Manager > Package Manager Console**
2. Şu komutu çalıştırın: `dotnet restore`
3. Veya Solution'a sağ tıklayıp **Restore NuGet Packages**

### Sorun 3: MAUI Workload Bulunamıyor

**Çözüm:**
1. **Tools > Get Tools and Features**
2. **Workloads** sekmesine gidin
3. **.NET Multi-platform App UI development** işaretleyin
4. **Modify** butonuna tıklayın ve yüklemeyi bekleyin

### Sorun 4: Windows Machine Seçeneği Görünmüyor

**Çözüm:**
1. Projeye sağ tıklayın
2. **Properties** seçin
3. **Application** sekmesinde **Target framework** kontrol edin
4. **net8.0-windows10.0.19041.0** olmalı

## 📱 Debug ve Test

### Debug Modu
- **F5**: Debug modunda çalıştırır (breakpoint'ler çalışır)
- **Ctrl+F5**: Debug olmadan çalıştırır (daha hızlı)

### Breakpoint Koyma
1. Kod satırının soluna tıklayın (kırmızı nokta görünür)
2. Uygulama çalışırken o satıra geldiğinde durur
3. Değişkenleri inceleyebilirsiniz

## 🎯 Hızlı Başlangıç

1. **LifeHub.sln** dosyasını Visual Studio 2022'de açın
2. **LifeHub.Dashboard** projesine sağ tıklayın
3. **Set as Startup Project** seçin
4. **F5** tuşuna basın
5. Uygulama Windows'ta açılacak!

## 📝 Notlar

- Her proje bağımsız çalışır
- Windows Machine platformunu seçtiğinizden emin olun
- İlk build biraz zaman alabilir (NuGet paketleri indirilir)
- Uygulamalar Windows 10/11'de çalışır

## 🆘 Yardım

Eğer sorun yaşarsanız:
1. **View > Output** penceresini açın (hata mesajlarını görmek için)
2. **Build > Clean Solution** yapın
3. **Build > Rebuild Solution** yapın
4. Tekrar deneyin

