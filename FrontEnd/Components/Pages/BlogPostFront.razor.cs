using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.JSInterop;
using MudBlazor;
using Share;
using Share.Enums;
using Share.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace FrontEnd.Components.Pages
{
    partial class BlogPostFront
    {
        [Parameter] public string? BlogId { get; set; }
        [Inject] public HttpClient? httpService { get; set; }
        [Inject] public NavigationManager? Nav { get; set; }
        [Inject] public IJSRuntime? JS { get; set; }
        private int fontSize = 0;
        public BlogPost BlogPostModel { get; set; } = new();
        private string text { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var e = await httpService!.GetFromJsonAsync<BlogPost>($"https://localhost:7231/api/BlogPost/{BlogId}");
                BlogPostModel = e!;
            }
            catch { }
        }

        private async Task HandleUploadImage(IBrowserFile file)
        {
            var content = await Helper.HandleImage(file);

            BlogPostModel.Content!.Add(
                new Section()
                {
                    Image = content,
                    Sequences = BlogPostModel.Content.Count() + 1,
                    ContentType = _ContentType.image
                });
        }
        private async Task HandleSubmit()
        {

            var response = await httpService!.PutAsJsonAsync<BlogPost>($"https://localhost:7231/api/BlogPost/{BlogPostModel.BlogId}", BlogPostModel);
            if (response.IsSuccessStatusCode)
            {
                Nav?.NavigateTo("/blogpost");
            }
        }
        private void OnAddInfoToModel()
        {
            BlogPostModel.Content?.Add(new() { Sequences = BlogPostModel.Content!.Count() + 1, Text = text, ContentType = _ContentType.text });
            text = string.Empty;
        }
        private async Task HandleAddSections()
        {
            await OnSaveContent();
            OnAddInfoToModel();
            await InvokeAsync(StateHasChanged);

        }
        private void HandleRemoveSection(Section e)
        {
            BlogPostModel.Content!.Remove(e);

        }

        private async Task OnSaveContent(int? sequences = null)
        {
            text = await JS!.InvokeAsync<string>("getEditorContent", sequences);
            await InvokeAsync(StateHasChanged);
        }
        private async Task HandleExecCommand(string command)
        {
            if (!string.IsNullOrEmpty(command) || !string.IsNullOrWhiteSpace(command))
            {
                await JS!.InvokeVoidAsync("execCommand", command);
            }
        }
        private async Task HandleExecCommand(string command, int size)
        {
            await JS!.InvokeVoidAsync("execCommandFontSize", command, size);
        }
        private void HandleIncreaseFontSize()
        {
            if (!(fontSize <= 7)) return;
            ++fontSize;
        }
        private void HandleDecreaseFontSize()
        {
            if (!(fontSize >= 0)) return;
            --fontSize;
        }
    }

}
