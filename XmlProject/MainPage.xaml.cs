namespace XmlProject;

public partial class MainPage : ContentPage
{
    private string _xmlFilePath = string.Empty;
    private readonly XmlService _xmlService = new XmlService(new SaxSearch());

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSelectXmlClicked(object sender, EventArgs e)
    {
        var customFileType = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".xml" } },
                { DevicePlatform.MacCatalyst, new[] { "xml" } },
                { DevicePlatform.Android, new[] { "text/xml", "application/xml" } },
                { DevicePlatform.iOS, new[] { "public.xml" } }
            });

        var result = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Оберіть XML файл",
            FileTypes = customFileType
        });

        if (result != null)
        {
            _xmlFilePath = result.FullPath;
            FilePathLabel.Text = _xmlFilePath;
            LoadAttributesIntoPickers();
        }
    }

    private void LoadAttributesIntoPickers()
    {
        if (string.IsNullOrEmpty(_xmlFilePath) || !File.Exists(_xmlFilePath))
        {
            return;
        } 

        var attributes = XmlService.ExtractUniqueAttributes(_xmlFilePath);

        FacultyPicker.ItemsSource = attributes["Faculties"];
        TopicPicker.ItemsSource = attributes["Topics"];
        TargetCoursesPicker.ItemsSource = attributes["TargetCourses"];
        CuratorPicker.ItemsSource = attributes["Curators"];
    }

    private void OnSearchClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_xmlFilePath) || !File.Exists(_xmlFilePath))
        {
            DisplayAlertAsync("Помилка", "Будь ласка, оберіть XML-файл перед пошуком.", "OK");
            return;
        }


        if (DomRadio.IsChecked)
        {
            _xmlService.ChooseSearch(new DomSearch());
        } 
        else if (LinqRadio.IsChecked)
        {
            _xmlService.ChooseSearch(new LinqToXmlSearch());
        }
        else
        {
            _xmlService.ChooseSearch(new SaxSearch());
        } 
        var targetCoursesList = new List<int>();
        if (TargetCoursesPicker.SelectedItem != null && 
            int.TryParse(TargetCoursesPicker.SelectedItem.ToString(), out int selectedCourse))
        {
            targetCoursesList.Add(selectedCourse);
        }
        var filter = new SearchFilter
        {
            Faculty = FacultyPicker.SelectedItem?.ToString() ?? string.Empty,
            Topic = TopicPicker.SelectedItem?.ToString() ?? string.Empty,
            TargetCourses = targetCoursesList,
            CuratorName = CuratorPicker.SelectedItem?.ToString() ?? string.Empty
        };

        var results = _xmlService.Search(_xmlFilePath, filter);

        OutputEditor.Text = string.Empty;
        if (results.Count == 0)
        {
            OutputEditor.Text = "Нічого не знайдено.";
            return;
        }

        foreach (var club in results)
        {
            OutputEditor.Text += $"Назва: {club.Name}\n";
            OutputEditor.Text += $"Факультет: {club.Faculty} \n";
			OutputEditor.Text += $"Департамент: {club.Department}\n";
            OutputEditor.Text += $"Час: {club.Time}\n";
            OutputEditor.Text += $"Староста: {club.HeadStudentName} Курс: {club.HeadStudentCourse}\n";
            OutputEditor.Text += $"Для кого: {club.TargetCourses} \n";
			OutputEditor.Text += $"Тематика: {club.Topic}\n";
            string isTeacherText = string.IsNullOrWhiteSpace(club.IsTeacher) ? "Ні" : club.IsTeacher;
            OutputEditor.Text += $"Керівник: {club.CuratorName}, це викладач: {isTeacherText}\n";
            OutputEditor.Text += "\n";
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        FacultyPicker.SelectedIndex = -1;
        TopicPicker.SelectedIndex = -1;
        TargetCoursesPicker.SelectedIndex = -1;
        CuratorPicker.SelectedIndex = -1;

        OutputEditor.Text = string.Empty;
    }

    private async void OnTransformHtmlClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_xmlFilePath) || !File.Exists(_xmlFilePath))
        {
            await DisplayAlertAsync("Помилка", "Оберіть файл XML для трансформації.", "OK");
            return;
        }

        string xmlDirectory = Path.GetDirectoryName(_xmlFilePath) ?? FileSystem.Current.AppDataDirectory;
        string htmlPath = Path.Combine(xmlDirectory, "output.html");
        string xslPath = Path.Combine(xmlDirectory, "transform.xsl");
        
        if (!File.Exists(xslPath))
        {
            await DisplayAlertAsync("Помилка", $"Файл transform.xsl не знайдено у папці:\n{xmlDirectory}", "OK");
            return;
        }
        

        try
        {
            XsltTransformer.TransformToHtml(_xmlFilePath, xslPath, htmlPath);
            await DisplayAlertAsync("Успіх", $"Файл HTML успішно згенеровано:\n{htmlPath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Помилка", $"Помилка трансформації: {ex.Message}", "OK");
        }
    }

    private async void OnExitClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlertAsync("Підтвердження", "Чи дійсно ви хочете завершити роботу з програмою?", "Так", "Ні");
        if (answer)
        {
            if (Window != null)
			{
				Application.Current?.CloseWindow(Window);
			}
        }
    }
}
