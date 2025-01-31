using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
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
        [Inject] public HttpClient? httpService { get; set; }
        public Section? InputSection { get; set; }
        public BlogPost BlogPostModel { get; set; } = new();
        public List<Section> _Section { get; set; } = new();
        public Section BindingSection { get; set; } = new();


        private string? text { get; set; }

        private bool _open = true;

        protected override async Task OnInitializedAsync()
        {
            var e = await httpService!.GetFromJsonAsync<List<BlogPost>>("https://localhost:7231/api/BlogPost");
            foreach (var i in e)
            {
                foreach (var t in i.Content) { _Section.Add(t); }
            }
            BindingSection.ContentType = _ContentType.text;
        }

        private async Task HandleUploadImage(InputFileChangeEventArgs file)
        {
            var _file = file.File;
            var content = await Helper.HandleImage(_file);

            BlogPostModel.Content!.Add(
                new Section()
                {
                    Image = content,
                    Sequences = BlogPostModel.Content.Count() + 1,
                    ContentType = _ContentType.image
                });
        }
        private void ToggleOpen()
        {
            _open = !_open;
        }
        private async Task HandleSubmit()
        {

            var response = await httpService!.PostAsJsonAsync<BlogPost>("https://localhost:7231/api/BlogPost", BlogPostModel);
        }

        private async Task HandleEnter(KeyboardEventArgs enter)
        {
            if (enter.Code == "Enter" && !string.IsNullOrEmpty(text))
            {

                var maxId = BlogPostModel.Content!.Count() + 1;
                BlogPostModel.Content!.Add(
                    new Section() { Sequences = maxId, Text = text, ContentType = _ContentType.text }
                );
                text = string.Empty;
                await InvokeAsync(StateHasChanged);
            }
        }

    }
}
