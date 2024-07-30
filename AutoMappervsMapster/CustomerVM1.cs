using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMappervsMapster
{
    public class CustomerVM1
    {
        public Guid Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }

        public DateTime DogumTarihi { get; set; }
    }
}
