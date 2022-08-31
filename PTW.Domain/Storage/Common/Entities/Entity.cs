namespace PTW.Domain.Storage.Common.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        public DateTime CreatedOn { get; protected set; }

        public DateTime? DeletedOn { get; protected set; }
    }
}
