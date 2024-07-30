using AutoMapper;
using KoleksiyonKarşılaştırma;
using Mapster;
using System.Diagnostics;

const int iterations = 1000;
const int collectionSize = 1000;

var dtos = new List<DTO>();
for (int i = 0; i < collectionSize; i++)
{
    dtos.Add(new DTO { Id = i, Name = $"Name {i}", DateOfBirth = new DateTime(1990, 1, 1).AddDays(i) });
}

// AutoMapper Setup
var config = new MapperConfiguration(cfg => cfg.CreateMap<DTO, VM>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.DateOfBirth)));
var mapper = config.CreateMapper();

// Measure AutoMapper Performance
var autoMapperStopwatch = Stopwatch.StartNew();
for (int i = 0; i < iterations; i++)
{
    var vms = mapper.Map<List<VM>>(dtos);
}
autoMapperStopwatch.Stop();
Console.WriteLine($"AutoMapper: {autoMapperStopwatch.ElapsedMilliseconds} ms");

// Mapster Setup
TypeAdapterConfig<DTO, VM>.NewConfig()
    .Map(dest => dest.FullName, src => src.Name)
    .Map(dest => dest.DOB, src => src.DateOfBirth);

// Measure Mapster Performance
var mapsterStopwatch = Stopwatch.StartNew();
for (int i = 0; i < iterations; i++)
{
    var vms = dtos.Adapt<List<VM>>();
}
mapsterStopwatch.Stop();
Console.WriteLine($"Mapster: {mapsterStopwatch.ElapsedMilliseconds} ms");