using ReservaHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Services
{
    public interface IHabitacionService
    {
        List<habitacion> GetHabitaciones();
        void Crearhabitacion(habitacion oHabitacion);
    }
}
