using Antlr.Runtime.Tree;
using ReservaHotel.Models;
using ReservaHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Services
{
    public interface ICalentarioService
    {
        List<reservacion> GetReservacionesPorHabiatacion(int habitacion);
        List<ReservacionVM> GetReservacionesPorId(int Id);
        List<reservacion> GetReservacionesPorCliente(int cliente);
        List<reservacion> GetReservacionesPorFecha(DateTime fechainicio);
        List<reservacion> SearchReservaciones(string titulo);
        void CrearReservacion(reservacion nuevareservacion);
    }
}
