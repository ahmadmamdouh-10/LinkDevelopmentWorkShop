using Matgary.DAL;
using System.ComponentModel.DataAnnotations;
using Matgary.BLL.Infra.Stores;

namespace Matgary.BLL.Infra.User
{
    public class UserViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public string Phone { get; set; }

        public AddressViewModel Address { set; get; }
        public string DeviceToken { get; set; }
        public string GoogleAccessToken { get; set; }
        public string FacebookAccessToken { get; set; }
        public StoreDto Store { get; set; }

    }

    public class AddressViewModel
    {
        public string Street { get; set; }
        public bool IsDefault { get; set; }
    }
    public class AddressViewModelResponse
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public CityKeyValueModel City { get; set; }
        public CityKeyValueModel Store { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public bool IsDefault { get; set; }
    }

    public class DefaultAddress
    {
        public int Id { get; set; }
        public bool IsDefault { get; set; }
    }

    public class CreateAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int CityId { get; set; }
        public long UserId { get; set; }
        public long? Area { get; set; }
        public string Location { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public bool IsDefault { get; set; }
        public long StoreId { get; set; }
    }
    public class CityKeyValueModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
    }
}
