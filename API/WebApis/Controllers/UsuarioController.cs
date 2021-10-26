using Aplication.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApis.Models;
using WebApis.Token;

namespace WebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAplicacaoUsuario _IAplicacaoUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsuarioController(IAplicacaoUsuario IAplicacaoUsuario, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _IAplicacaoUsuario = IAplicacaoUsuario;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Criação de Token
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarToken")]
        public async Task<IActionResult> CriarToken([FromBody] Login login)
        {
            // Verificar se o Login e senha foi nulo
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }

            // Gerando o Token
            var resultado = await _IAplicacaoUsuario.ExisteUsuario(login.email, login.senha);

            if (resultado)
            {
                var idUsuario = await _IAplicacaoUsuario.RetornaIdUsuario(login.email);
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Empresa - Aprendendo Dev Net Core")
                    .AddIssuer("Teste.Securiry.Bearer")
                    .AddAudience("Teste.Securiry.Bearer")
                    .AddClaim("idUsuario", idUsuario)
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Ok("Falta alguns dados");
                //return Unauthorized();
            }

            var resultado = await
                _IAplicacaoUsuario.AdicionarUsuario(login.email, login.senha, login.idade, login.celular);

            if (resultado)
            {
                return Ok("Usuario Adicionado com sucesso");
            }
            else
            {
                return Ok("Erro ao adicionar ao usuario");
            }
        }

        // Criação via Identity
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }

            var resultado = await
                _signInManager.PasswordSignInAsync(login.email, login.senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var idUsuario = await _IAplicacaoUsuario.RetornaIdUsuario(login.email);
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Empresa - Aprendendo Dev Net Core")
                    .AddIssuer("Teste.Securiry.Bearer")
                    .AddAudience("Teste.Securiry.Bearer")
                    .AddClaim("idUsuario", idUsuario)
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicioanarUsuarioIdentity")]
        public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Ok("Falta algumas informações");
            }

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                Celular = login.celular,
                Tipo = TipoUsuario.Comum,
            };

            var resultado = await _userManager.CreateAsync(user, login.senha);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            //Gerar um email de Confirmação caso precise

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno de email

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
            {
                return Ok("Usuário adicionado com sucesso");
            }
            else
            {
                return Ok("Error ao confirmar o usuário");
            }


        }

        // Informação Para adicionar um Usuario tem quer ter pelo menos uma Caracter especial

    }
}