using ReservaHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReservaHotel.Services
{
    public class HabitacionService : IHabitacionService
    {

        private readonly cti_reservacionEntities _dbContext;

        public HabitacionService(cti_reservacionEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public void Crearhabitacion(habitacion oHabitacion)
        {
            habitacion nuevaHabitacion = new habitacion();
            nuevaHabitacion.TV = oHabitacion.TV;
            nuevaHabitacion.NCamas = oHabitacion.NCamas;
            nuevaHabitacion.SanitarioPrivado = oHabitacion.SanitarioPrivado;
            nuevaHabitacion.AC = oHabitacion.AC;
            nuevaHabitacion.Titulo = oHabitacion.Titulo;
            nuevaHabitacion.Precio = oHabitacion.Precio;
            nuevaHabitacion.URLImagen = oHabitacion.URLImagen;

            _dbContext.habitacion.Add(nuevaHabitacion);
            _dbContext.SaveChanges();
        }

        public List<habitacion> GetHabitaciones()
        {
            var listHabitaciones = (from h in _dbContext.habitacion
                                    select new habitacion
                                    {
                                        HabitacionId = h.HabitacionId,
                                        AC = h.AC,
                                        NCamas = h.NCamas,
                                        Precio = h.Precio,
                                        SanitarioPrivado = h.SanitarioPrivado,
                                        Titulo = h.Titulo,
                                        TV = h.TV,
                                        URLImagen = h.URLImagen
                                    }).ToList();
            return listHabitaciones;
        }
    }
}