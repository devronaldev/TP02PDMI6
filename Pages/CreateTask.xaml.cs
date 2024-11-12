using TP02PDMI6.Models;

namespace TP02PDMI6.Pages;

public partial class CreateTask : ContentPage
{
	public CreateTask()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
	{
        PriorityPicker.ItemsSource = Enum.GetValues(typeof(EPriority)).Cast<EPriority>().ToList();
    }

	protected async void OnSaveBtnClicked(object sender, EventArgs e)
	{
        if (string.IsNullOrEmpty(TitleEntry.Text.ToString()))
        {
            DisplayError("Incompleto", "Por favor preencha o campo título");
            return;
        }

        if (string.IsNullOrEmpty(DescriptionEntry.Text.ToString()))
        {
            DisplayError("Incompleto", "Por favor preencha o campo descrição.");
            return;
        }

        if (PriorityPicker.SelectedItem == null)
        {
            DisplayError("Incompleto", "Por favor selecione um das prioridades");
            return;
        }

        DateTime createdAt = new DateTime(DatePicker.Date.Year, DatePicker.Date.Month, DatePicker.Date.Day, TimePicker.Time.Hours, TimePicker.Time.Minutes, TimePicker.Time.Seconds);
        var newTask = new TaskItem
		{
			Title = TitleEntry.Text,
			Description = DescriptionEntry.Text,
            CreatedAt = createdAt,
            Priority = (EPriority)PriorityPicker.SelectedItem
        };

        var _dbContext = new ApplicationDbContext();
        await _dbContext.TasksItems.AddAsync(newTask);
        await _dbContext.SaveChangesAsync();
        await Navigation.PopAsync();
	}

    private async void DisplayError(string title, string description)
    {
        await DisplayAlert(title, description, "Ok");
    }
}