using Microsoft.EntityFrameworkCore;
using TP02PDMI6.Models;

namespace TP02PDMI6.Page;

public partial class Details : ContentPage
{
	public Details()
	{
		InitializeComponent();

	}

    protected override async void OnAppearing()
    {
        base.OnBindingContextChanged();

        int id = (int)BindingContext;
        var _dbContext = new ApplicationDbContext();

        TaskItem? taskItem = await _dbContext.TasksItems.FirstOrDefaultAsync(t => t.Id == id);

        if (taskItem == null)
        {
            await DisplayAlert("Erro", "Tarefa não encontrada no banco de dados", "Retornar");
            await Navigation.PopAsync();
        }

        Title = taskItem.Title;
        DateLabel.Text = taskItem.CreatedAt.ToString();
        DescriptionLabel.Text = taskItem.Description.ToString();
        PriorityLabel.Text = taskItem.Priority.ToString();
        DateLabel.Text = taskItem.CreatedAt.ToString();
    }

    private async void OnEditButtonClicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new Pages.EditDetails { BindingContext = this.BindingContext });
    }

	private async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var option = await DisplayAlert("Apagar a tarefa?", "Atenção é uma ação irreversível", "Apagar", "Cancelar");

		if (option == true)
		{
			try
			{
				var _dbContext = new ApplicationDbContext();
				var taskItem = BindingContext as TaskItem;
				var taskToDelete = await _dbContext.TasksItems.FirstOrDefaultAsync(task => task.Id == taskItem.Id);

				if (taskToDelete == null)
				{
                    await DisplayAlert("Erro", $"Ocorreu um erro inesperado", "Ok");
                }

                _dbContext.TasksItems.Remove(taskToDelete);
				await _dbContext.SaveChangesAsync();
                await DisplayAlert("Sucesso", $"A tarefa foi excluída", "Ok");
            } catch (Exception ex)
			{
                await DisplayAlert("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "Ok");
            }
        }

		await Navigation.PopAsync();
	}
}