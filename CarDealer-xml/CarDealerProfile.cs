using AutoMapper;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSupplierDTO, Supplier>();
            CreateMap<ImportPartsDTO, Part>();
            CreateMap<ImportCarDTO, Car>();
            CreateMap<Car, ExportCarsWithDistance>();
            CreateMap<ImportCustomerDTO, Customer>();
            CreateMap<ImportSaleDTO, Sale>();
        }
    }
}
