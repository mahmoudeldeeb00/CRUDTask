using Task.DAl.Entities;

namespace TaskProject.BL.IServices
{
    public interface ISelectService
    {
        List<Governate> Governates();
        List<City> Cities(int GovernateId);
        List<Village> Villages(int CityId);
    }
}
