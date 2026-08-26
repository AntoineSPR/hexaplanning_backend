namespace Hexaplanning.Models.Interfaces
{
    public interface ICreatable
    {
        public DateTime CreatedAt { get; set; }
    }

    public interface IUpdatable
    {
        public DateTime? UpdatedAt { get; set; }
    }
}
