using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TP02PDMI6.Models;
using TP02PDMI6.Page;
using TP02PDMI6.Pages;

namespace TP02PDMI6
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var _dbContext = new ApplicationDbContext();
            LoadTask(_dbContext);
        }

        public async Task LoadTask(ApplicationDbContext dbContext) => TaskListView.ItemsSource = await dbContext.TasksItems.ToListAsync();

        public async void OnTaskSelected(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var tappedTask = e.Item as TaskItem;

            await Navigation.PushAsync(new Details { BindingContext = tappedTask.Id });
        }

        private async void OnAddBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateTask());
        }
    }
}
