using Assignment_Intership.Data.Common;

namespace Assignment_Intership.Data.Repositories
{
    public class ApplicationRepository : Repository, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}
