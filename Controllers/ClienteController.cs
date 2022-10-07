using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReservaHotel.Models;
using ReservaHotel.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;
using AllowAnonymousAttribute = System.Web.Mvc.AllowAnonymousAttribute;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace ReservaHotel.Controllers
{
    [Authorize]
    //[ApiController]
    //[Route("api/[controller]")]
    //[EnableCors("MyPolicy")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        //public ClienteController()
        //{
        //}

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registrar([System.Web.Http.FromBody] cliente oCliente)
        {
            try
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                _clienteService.Registra(oCliente, oCliente.Clave);
                return RedirectToAction("Authenticate");
            }
            catch (ApplicationException ex)
            {
                Response.StatusCode = 400;
                return HttpNotFound(ex.Message);
            }
            catch (Exception ex2)
            {
                Response.StatusCode = 400;
                return HttpNotFound(ex2.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Authenticate()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Authenticate([System.Web.Http.FromBody] cliente oCliente)
        {

            if (oCliente == null)
                return View();

            var cliente = _clienteService.Authenticate(oCliente.correolectronico, oCliente.Clave);

            if (cliente == null)
                return HttpNotFound("Correo o Clave son Incorrectos!");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Reservaciones-Hotel-Proyecto-Intecap-2022");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, cliente.ClienteId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            Session["ClienteId"] = cliente.ClienteId;
            Session["EMAIL"] = cliente.correolectronico;
            Session["Nombre"] = cliente.PrimerNombre;
            Session["Apellido"] = cliente.PrimerApellido;
            Session["Telefono"] = cliente.NumeroTelefonico;
            Session["Token"] = tokenString;


            //return View(new
            //{
            //   ClienteId = cliente.ClienteId,
            //   EMAIL = cliente.correolectronico,
            //   Nombre = cliente.PrimerNombre,
            //   Apellido = cliente.PrimerApellido,
            //   Telefono = cliente.NumeroTelefonico,
            //   Token = tokenString
            //});

            return RedirectToAction("Index","Home");

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetReservacionesCliente()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetReservacionesCliente(int clienteId)
        {
            try
            {
                var reservaciones = _clienteService.GetClienteReservaciones(clienteId);
                return Json(reservaciones);
            }
            catch (ApplicationException ex)
            {
                Response.StatusCode = 400;
                return HttpNotFound(ex.Message);
            }
        }

    }
}
