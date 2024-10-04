using Library.DataAccess.Data;
using Library.Entities.Models;
using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.RepositoryImplementation
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllMembersAsync()
        {

            return await userManager.GetUsersInRoleAsync(StaticData.MemberRole);
        }

    }
}
