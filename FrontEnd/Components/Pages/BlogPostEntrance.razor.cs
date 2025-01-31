using Microsoft.AspNetCore.Components;
using MudBlazor;
using Share.Models;

namespace FrontEnd.Components.Pages
{
    partial class BlogPostEntrance
    {
        [Inject] public NavigationManager nav { get; set; }
        [Inject] public IDialogService? DialogService { get; set; }
        private List<BlogPost>? BlogPostModelList { get; set; }
        public void Nav() => nav.NavigateTo("/blogpost/create");

        private async Task OpenCreateDialogAsync()
        {
            var parameters = new DialogParameters<BlogPostCreateDialog>
            {
                { x => x.BlogPostModel, BlogPostModelList},
            };
            var dialog = await DialogService!.ShowAsync<BlogPostCreateDialog>("", parameters);

            var result = await dialog.Result;

        }
    }
}
