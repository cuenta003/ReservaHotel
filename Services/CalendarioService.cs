using ReservaHotel.Models;
using ReservaHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ReservaHotel.Services
{
    public class CalendarioService : ICalentarioService
    {

        private readonly cti_reservacionEntities _dbContext;

        public CalendarioService(cti_reservacionEntities dbContext)
        {
            _dbContext = dbContext; 
        }

        public void CrearReservacion(reservacion nuevareservacion)
        {
            reservacion nuevaReservacion = new reservacion();
            nuevaReservacion.HabitacionId = nuevareservacion.HabitacionId;
            nuevaReservacion.FechaInicio = nuevareservacion.FechaInicio;
            nuevaReservacion.FechaFinal = nuevareservacion.FechaFinal;
            nuevaReservacion.Total = nuevareservacion.Total;
            nuevaReservacion.ClienteId = nuevareservacion.ClienteId;
            nuevareservacion.Titulo = nuevareservacion.Titulo;

            _dbContext.reservacion.Add(nuevaReservacion);
            _dbContext.SaveChanges();

            DateTime fechaPago = DateTime.Now;

            pago nuevoPago = new pago();
            nuevoPago.FechaPago = fechaPago;
            nuevoPago.TotalPagado = nuevaReservacion.Total;
            nuevoPago.RecervacionId = nuevaReservacion.ReservacionId;

            _dbContext.pago.Add(nuevoPago);
            _dbContext.SaveChanges();

            recibo nuevoRecibo = new recibo();
            nuevoRecibo.HabitacionId = nuevaReservacion.HabitacionId;
            nuevoRecibo.PagoId = nuevoPago.PagoId;
            nuevoRecibo.ReservacionId = nuevaReservacion.ReservacionId;
            nuevoRecibo.TotalPrecio = nuevaReservacion.Total;

            _dbContext.recibo.Add(nuevoRecibo);
            _dbContext.SaveChanges();
        }

        public List<reservacion> GetReservacionesPorCliente(int cliente)
        {
            var reservaciones = (from r in _dbContext.reservacion
                                 where r.ClienteId == cliente
                                 select new reservacion
                                 {
                                     ReservacionId = r.ReservacionId,
                                     ClienteId = r.ClienteId,
                                     FechaInicio = r.FechaInicio,
                                     FechaFinal = r.FechaFinal,
                                     Total = r.Total
                                 }).ToList();
            return reservaciones;
        }

        public List<reservacion> GetReservacionesPorFecha(DateTime fechainicio)
        {
            var reservaciones = (from r in _dbContext.reservacion
                                 where r.FechaInicio >= fechainicio
                                 select new reservacion
                                 {
                                     ReservacionId = r.ReservacionId,
                                     ClienteId = r.ClienteId,
                                     FechaInicio = r.FechaInicio,
                                     FechaFinal = r.FechaFinal,
                                     Total = r.Total
                                 }).ToList();
            return reservaciones;
        }

        public List<reservacion> GetReservacionesPorHabiatacion(int habitacion)
        {
            var reservaciones = (from r in _dbContext.reservacion
                                 where r.HabitacionId == habitacion
                                 select new reservacion
                                 {
                                     ReservacionId = r.ReservacionId,
                                     ClienteId = r.ClienteId,
                                     FechaInicio = r.FechaInicio,
                                     FechaFinal = r.FechaFinal,
                                     Total = r.Total
                                 }).ToList();
            return reservaciones;
        }

        public List<ReservacionVM> GetReservacionesPorId(int Id)
        {
            //var reservaciones = (from r in _dbContext.reservacion
            //                     where r.ReservacionId == Id
            //                     select new reservacion
            //                     {
            //                         ReservacionId = r.ReservacionId,
            //                         ClienteId = r.ClienteId,
            //                         FechaInicio = r.FechaInicio,
            //                         FechaFinal = r.FechaFinal,
            //                         Total = r.Total
            //                         }).ToList();

            var reservaciones = (from r in _dbContext.reservacion
                          join h in _dbContext.habitacion on r.HabitacionId equals h.HabitacionId
                          join c in _dbContext.cliente on r.ClienteId equals c.ClienteId
                          where r.ReservacionId == Id
                          select new ReservacionVM
                          {
                              ReservacionId = r.ReservacionId,
                              FechaInicio = r.FechaInicio,
                              FechaFinal = r.FechaFinal,
                              Titulo = r.Titulo,
                              HabitacionId = h.HabitacionId,
                              Total = r.Total,
                              ClienteId = c.ClienteId,
                              URLImagen = h.URLImagen,
                              Precio = h.Precio,
                              NCamas = h.NCamas,
                              TV = h.TV,
                              SanitarioPrivado = h.SanitarioPrivado,
                              AC = h.AC,
                              correolectronico = c.correolectronico,
                              CompletoNombre = String.Format( $"{c.PrimerNombre} {c.SegundoNombre} {c.PrimerApellido} {c.SegundoApellido}" ),
                              NumeroTelefonico = c.NumeroTelefonico
                          }).ToList();

            return reservaciones;
        }

        public List<reservacion> SearchReservaciones(string titulo) 
        {
            var reservaciones = (from r in _dbContext.reservacion
                                 where r.Titulo.ToLower().Contains(titulo.ToLower())
                                 select new reservacion 
                                 { 
                                     ReservacionId = r.ReservacionId,
                                     FechaInicio = r.FechaInicio,
                                     FechaFinal = r.FechaFinal,
                                     ClienteId = r.ClienteId,
                                     HabitacionId = r.HabitacionId,
                                     Titulo = r.Titulo,
                                     Total = r.Total
                                 }).ToList();
            return reservaciones;
        }
    }
}