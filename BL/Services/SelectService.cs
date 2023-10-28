using Task.BL.UOW;
using Task.DAl.Entities;
using TaskProject.BL.IServices;

namespace TaskProject.BL.Services
{
    public class SelectService : ISelectService
    {
        private readonly IUnitOfWork UOW;
        public SelectService(IUnitOfWork UOW)
        {
            this.UOW = UOW;
        }
        public List<City> Cities(int GovernateId) => UOW.Cities.FindAllAsync(s => s.Status == 1 && s.GovernateId==GovernateId).Result.ToList();


        public List<Governate> Governates() => UOW.Governates.FindAllAsync(s => s.Status == 1).Result.ToList();
        
        public List<Village> Villages(int CityId)=> UOW.Villages.FindAllAsync(s => s.Status == 1 && s.CityId==CityId).Result.ToList();
    }
}
