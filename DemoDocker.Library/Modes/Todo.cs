using System.ComponentModel.DataAnnotations;

namespace DemoDocker.Library.Modes
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }
        public string userId { get; set; }
    }
}
