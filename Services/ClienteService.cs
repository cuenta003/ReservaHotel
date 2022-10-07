using ReservaHotel.Helpers;
using ReservaHotel.Models;
using ReservaHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace ReservaHotel.Services
{
    public class ClienteService : IClienteService
    {
        private readonly cti_reservacionEntities _dbContext;

        public ClienteService(cti_reservacionEntities dbContext)
        {
            _dbContext = dbContext;
        }
        public cliente Authenticate(string user, string password)
        {
            // check if email and password fields are empty or null
            if (string.IsNullOrEmpty(user) || (string.IsNullOrEmpty(password)))
                return null;

            var cliente = _dbContext.cliente.SingleOrDefault(c => c.correolectronico == user);

            if (cliente == null)
                return null;

            string snuevopass = ComputeSha256Hash(password);

            if (!Seguridad.VerifyPasswordHash(snuevopass, cliente.PasswordHash, cliente.PasswordSalt))
                return null;

            return cliente;

        }

        public List<ReservacionVM> GetClienteReservaciones(int clienteid)
        {
            var reservaciones = (from r in _dbContext.reservacion
                                 join h in _dbContext.habitacion on r.HabitacionId equals h.HabitacionId
                                 join c in _dbContext.cliente on r.ClienteId equals c.ClienteId
                                 where r.ClienteId == clienteid
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
                                     CompletoNombre = String.Format($"{c.PrimerNombre} {c.SegundoNombre} {c.PrimerApellido} {c.SegundoApellido}"),
                                     NumeroTelefonico = c.NumeroTelefonico
                                 }).ToList();

            return reservaciones;
        }

        public cliente Registra(cliente nuevocliente, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Contraseña es requerida!");

            if (!Seguridad.IsValidEmail(nuevocliente.correolectronico))
                throw new ApplicationException("Ingrese un correo electronico valido!");

            if (_dbContext.cliente.Any(x => x.correolectronico == nuevocliente.correolectronico))
                throw new ApplicationException($"Cliente con {nuevocliente.correolectronico} ya existe!!!");

            byte[] passwordHash, passwordSalt;
            
            string snuevopass = ComputeSha256Hash(password);

            Seguridad.CreatePasswordHash(snuevopass, out passwordHash, out passwordSalt);

            nuevocliente.PasswordHash = passwordHash;
            nuevocliente.PasswordSalt = passwordSalt;
            nuevocliente.Clave = snuevopass;

            _dbContext.cliente.Add(nuevocliente);
            _dbContext.SaveChanges();

            return nuevocliente;

        }

        /// <summary>
        /// Convierte a MD5 la clave
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public string ComputeSha256Hash(string rawData)
        {

            //HMACSHA512 sha512 = HMACSHA512.Create();
            //HMACMD5 md5 = HMACMD5.Create();

            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            //using (HMAC shaMD5Hash = HMACMD5.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                //byte[] bytes = shaMD5Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}