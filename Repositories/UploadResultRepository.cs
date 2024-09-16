﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MJRPAdmin.DBContext;
using MJRPAdmin.Entities;
using MJRPAdmin.Repositories.Interfaces;

namespace MJRPAdmin.Repositories
{
    public class UploadResultRepository : GenericRepository<UploadResult>, IUploadResultRepository
    {
        private readonly mjrpContext _dbContext;

        public UploadResultRepository(mjrpContext DbContext) : base(DbContext)
        {
            _dbContext = DbContext;
        }
    }
}
