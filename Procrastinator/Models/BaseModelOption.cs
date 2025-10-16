using Procrastinator.Models.Interfaces;

namespace Procrastinator.Models
{
    public class BaseModelOption : BaseModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string? Icon { get; set; }
    }
}
