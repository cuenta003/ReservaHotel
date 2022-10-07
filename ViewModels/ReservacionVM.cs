using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservaHotel.ViewModels
{
    public class ReservacionVM
    {
        public int ReservacionId { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFinal { get; set; }
        public string Titulo { get; set; }
        public Nullable<int> HabitacionId { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<int> ClienteId { get; set; }

        public string URLImagen { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<int> NCamas { get; set; }
        public Nullable<bool> TV { get; set; }
        public Nullable<bool> SanitarioPrivado { get; set; }
        public Nullable<bool> AC { get; set; }

        public string correolectronico { get; set; }
        public string CompletoNombre { get; set; }
        public string NumeroTelefonico { get; set; }

    }
}