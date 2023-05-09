using Collini.GestioneInterventi.Domain.Security;

namespace Collini.GestioneInterventi.Application.Security.DTOs
{
    public class UserReadModel
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public bool Enabled { get; set; }
        public Role Role { get; set; }
    }
}