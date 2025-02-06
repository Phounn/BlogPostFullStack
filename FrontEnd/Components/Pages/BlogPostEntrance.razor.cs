using Microsoft.AspNetCore.Components;
using MudBlazor;
using Share.Models;

namespace FrontEnd.Components.Pages
{
    partial class BlogPostEntrance
    {
        [Inject] public NavigationManager? nav { get; set; }
        [Inject] public IDialogService? DialogService { get; set; }
        [Inject] public HttpClient? http { get; set; }
        private List<BlogPost>? BlogPostModelList { get; set; }
        private BlogPost? BlogPostModel { get; set; } = new();
        private CategoriesOfBlog? CategoryModel { get; set; } = new();
        public void Nav(string id) => nav!.NavigateTo($"/blogpost/create/{id}");

        protected override async Task OnInitializedAsync()
        {
            BlogPostModelList = await http!.GetFromJsonAsync<List<BlogPost>>("https://localhost:7231/api/BlogPost");

        }
        private async Task OpenCreateDialogAsync()
        {
            var parameters = new DialogParameters<BlogPostCreateDialog>
            {
                { x => x.BlogPostModel, BlogPostModel},
                {x => x.CategoryModel, CategoryModel},
                {x => x.OnRenderParent, EventCallback.Factory.Create(this, LoadBlogPosts)}
            };
            var options = new DialogOptions { FullWidth = true , MaxWidth = MaxWidth.Medium};
            var dialog = await DialogService!.ShowAsync<BlogPostCreateDialog>("", parameters, options);

            var result = await dialog.Result;
        }
        private async Task RemoveBlogPost(string? blogId)
        {
            if (string.IsNullOrEmpty(blogId))
                return;

            var response = await http!.DeleteAsync($"https://localhost:7231/api/BlogPost/{blogId}");

            if (response.IsSuccessStatusCode)
            {
                // Remove the deleted blog post from the list
                var blogToRemove = BlogPostModelList.FirstOrDefault(b => b.BlogId == blogId);
                if (blogToRemove != null)
                {
                    BlogPostModelList.Remove(blogToRemove);
                }

                // Trigger UI re-render
                await InvokeAsync(StateHasChanged);
            }
        }
        private async Task LoadBlogPosts()
        {
            BlogPostModelList = await http!.GetFromJsonAsync<List<BlogPost>>("https://localhost:7231/api/BlogPost");
            StateHasChanged(); // ✅ Trigger UI update
        }

    }
}
