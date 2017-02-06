using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.DAL
{
    public interface IEntity
    {
        long Id { get; set; }
    }
}