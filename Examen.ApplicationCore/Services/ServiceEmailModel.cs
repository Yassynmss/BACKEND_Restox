using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class ServiceEmailModel : Service<EmailModel>, IServiceEmailModel
    {
        private readonly DbContext _context;

        public ServiceEmailModel(IUnitOfWork unitOfWork , DbContext context) : base(unitOfWork)
        {
            _context = context;
        }
        public async Task AddAsync(EmailModel email)
        {
            await _context.Set<EmailModel>().AddAsync(email);
            await _context.SaveChangesAsync();
        }

    }
}
