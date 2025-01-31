using Microsoft.AspNetCore.Components.Forms;

namespace Share 
{
    public static class Helper
    {
        public async static Task<string> HandleImage(IBrowserFile file)
        {
            var maxFile = 1024 * 1024 * 10;
            using var ms = new MemoryStream();
            await file.OpenReadStream(maxFile).CopyToAsync(ms);

            var base64 = Convert.ToBase64String(ms.ToArray());
            var content = $"data:image/png;base64,{base64}";
            return content; 

        }
    }
}
