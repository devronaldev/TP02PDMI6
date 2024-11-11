using Microsoft.EntityFrameworkCore;
using TP02PDMI6.Models;

namespace TP02PDMI6.Page;

public partial class Details : ContentPage
{
	public Details()
	{
		InitializeComponent();

	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

		var taskItem = BindingContext as TaskItem;

		if (taskItem != null)
		{
			DateLabel.Text = taskItem.CreatedAt.ToString();
			DescriptionLabel.Text = taskItem.Description.ToString();
			PriorityLabel.Text = taskItem.Priority.ToString();
		}
    }

    private void OnEditButtonClicked(object sender, EventArgs e)
    {

    }

	private async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var option = await DisplayAlert("Apagar a tarefa?", "Atenção é uma ação irreversível", "Apagar", "Cancelar");

		if (option == true)
		{
			try
			{
				var _dbContext = new ApplicationDbContext();
				var taskToDelete = await _dbContext.TasksItems.FirstOrDefaultAsync(task => task.Id == int.Parse(Id.Text));

				if (taskToDelete == null)
				{
                    await DisplayAlert("Erro", $"Ocorreu um erro inesperado", "Ok");
                }

                _dbContext.TasksItems.Remove(taskToDelete);
				await _dbContext.SaveChangesAsync();
                await DisplayAlert("Sucesso", $"A tarefa foi excluída", "Ok");

				var mainPage = (MainPage)Application.Current.MainPage;
				await mainPage.LoadTask(_dbContext);
            } catch (Exception ex)
			{
                await DisplayAlert("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "Ok");
            }
        }

		await Navigation.PopAsync();
	}
}