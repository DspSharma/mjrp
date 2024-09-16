using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MJRPAdmin.DBContext;
using MJRPAdmin.Entities;
using MJRPAdmin.Repositories.Interfaces;

namespace MJRPAdmin.Repositories
{
    public class AcadProgRepository : GenericRepository<AcademicProg>, IAcadProgRepository
    {
        private readonly mjrpContext _dbContext;

        public AcadProgRepository(mjrpContext DbContext) : base(DbContext)
        {
            _dbContext = DbContext;
        }
    }
}
