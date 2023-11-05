using HotelProject.BL.Interfaces;
using HotelProject.DL.Repositories;
using System.Configuration;

namespace HotelProject.Util
{
    public static class RepositoryFactory
    {
        public static IActivityRepository ActivityRepository { get { return new ActivityRepositoryADO(ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString); } }

        public static ICustomerRepository CustomerRepository { get { return new CustomerRepositoryADO(ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString); } }
    }
}