using Microsoft.AspNetCore.Http;

namespace FinalExamProject.Helpers
{
    public static class FileManager
    {
        public static bool CheckImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/") && file.Length / 1024 / 1024 <= 3;

        }
        public static string Upload(this IFormFile file,string path,string env)
        {
            var uploadpath=Path.Combine(env,path);
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }

            string filename = Guid.NewGuid().ToString() + file.FileName;
            using(var stream=new FileStream(Path.Combine(uploadpath, filename), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }
    }
}
