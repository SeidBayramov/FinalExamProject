using Microsoft.AspNetCore.Http;

namespace FinalExamProject.Areas.Manage.ViewModel.Fruit
{
    public class FruitUpdateVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }

    }
}
