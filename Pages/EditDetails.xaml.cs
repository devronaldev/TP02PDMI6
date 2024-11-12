using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TP02PDMI6.Models;

namespace TP02PDMI6.Pages;

public partial class EditDetails : ContentPage
{
	public EditDetails()
	{
		InitializeComponent();
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

        int id = (int)BindingContext;
        var _dbContext = new ApplicationDbContext();

        TaskItem? taskItem = await _dbContext.TasksItems.FirstOrDefaultAsync(t => t.Id == id);

        if (taskItem == null)
        {
            await DisplayAlert("Erro", "Tarefa não encontrada no banco de dados", "Retornar");
            await Navigation.PopAsync();
        }

        if (taskItem != null)
		{
			TitleEntry.Text = taskItem.Title;
			DescriptionEntry.Text = taskItem.Description;
			PriorityPicker.ItemsSource = Enum.GetValues(typeof(EPriority)).Cast<EPriority>().ToList();
			DatePicker.Date = taskItem.CreatedAt.Date;
			TimePicker.Time = taskItem.CreatedAt.TimeOfDay;
		}
    }

	private async void OnUpdateBtnClicked(object sender, EventArgs e)
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


        int id = (int)BindingContext;
        var _dbContext = new ApplicationDbContext();
        var newTask = await _dbContext.TasksItems.FirstOrDefaultAsync(t => t.Id == id);

		if (newTask == null)
		{
			DisplayError("Erro", "Atividade inexistente no banco de dados.");
		}

		newTask.Title = TitleEntry.Text;
		newTask.Description = DescriptionEntry.Text;
		newTask.CreatedAt = createdAt;
		newTask.Priority = (EPriority)PriorityPicker.SelectedItem;

		await _dbContext.SaveChangesAsync();

		BindingContext = newTask;

        await Navigation.PopAsync();
	}

	protected async void DisplayError(string title, string message) 
	{
		await DisplayAlert(Title, message, "Ok");
	}
}