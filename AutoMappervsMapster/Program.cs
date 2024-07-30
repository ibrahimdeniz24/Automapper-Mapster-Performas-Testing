using AutoMapper;
using AutoMappervsMapster;
using Mapster;
using System.Diagnostics;

const int iterations = 100000;
var customerDTO = new CustomerDTO { Id = new Guid(), FirstName = "İbrahim", LastName = "Deniz" ,DateOfBirth = new DateTime(1991, 07, 15) };

Console.WriteLine(new string('-', 30));
var mapsterStopwatch = Stopwatch.StartNew();
for (int i = 0; i < iterations; i++)
{
    var destination = customerDTO.Adapt<CustomerVM>();
}
mapsterStopwatch.Stop();
Console.WriteLine($"Mapster: {mapsterStopwatch.ElapsedMilliseconds} ms");


Console.WriteLine(new string('-', 30));
var mapsterStopwatch0 = Stopwatch.StartNew();
for (int i = 0; i < iterations; i++)
{
    var destination = customerDTO.Adapt<CustomerVM>();
}
mapsterStopwatch0.Stop();
Console.WriteLine($"Mapster0: {mapsterStopwatch0.ElapsedMilliseconds} ms");

//Maspter1 Setup
Console.WriteLine(new string('-', 30));
TypeAdapterConfig<CustomerDTO, CustomerVM1>.NewConfig()
    .Map(dest => dest.Adi, src => src.FirstName)
    .Map(dest => dest.Soyadi, src => src.LastName)
    .Map(dest => dest.DogumTarihi, src => src.DateOfBirth);

// Measure Mapster Performance
var mapsterStopwatch1 = Stopwatch.StartNew();
for (int i = 0; i < iterations; i++)
{
    var destination1 = customerDTO.Adapt<CustomerVM1>();
}
mapsterStopwatch1.Stop();
Console.WriteLine($"Mapster1: {mapsterStopwatch1.ElapsedMilliseconds} ms");
Console.WriteLine(new string('-', 30));
//AutoMapper Setup
var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerVM>()
    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth)));
var mapper = config.CreateMapper();

// Measure AutoMapper Performance
var autoMapperStopwatch = Stopwatch.StartNew();
for (int i = 0; i < iterations; i++)
{
    var destination = mapper.Map<CustomerVM>(customerDTO);
}
autoMapperStopwatch.Stop();
Console.WriteLine($"AutoMapper: {autoMapperStopwatch.ElapsedMilliseconds} ms");


