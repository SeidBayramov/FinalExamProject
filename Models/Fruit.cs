using FinalExamProject.Models.Common;

namespace FinalExamProject.Models
{
    public class Fruit:BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
    }
}
