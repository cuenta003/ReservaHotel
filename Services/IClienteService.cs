using ReservaHotel.Models;
using ReservaHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Services
{
    public interface IClienteService
    {
        cliente Authenticate(string user, string password);
        cliente Registra(cliente nuevocliente, string password);
        List<ReservacionVM> GetClienteReservaciones(int clienteid); 
    }
}
