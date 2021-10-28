using Aplication.Interfaces;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using Entidades.Noticia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApis.Models;

namespace WebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly IAplicacaoNoticia _IAplicacaoNoticia;

        public NoticiasController(IAplicacaoNoticia IAplicacaoNoticia)
        {
            _IAplicacaoNoticia = IAplicacaoNoticia;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListarNoticiaCustomizada")]
        public async Task<List<NoticiaViewModel>> ListarNoticiaCustomizada()
        {
            return await _IAplicacaoNoticia.ListarNoticiaCustomizada();
        }


        // So vai se entrar nesse metado quem tiver o token de acessso
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListarNoticias")]
        public async Task<List<Noticia>> ListarNoticias()
        {
            return await _IAplicacaoNoticia.ListarNoticiaAtiva();
        }

        // é todos erros do usurios
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaNoticia")]
        public async Task<List<Notifica>> AdicionaNoticia(NoticiaModel noticia)
        {
            var noticiaNova = new Noticia();

            noticiaNova.Titulo = noticia.Titulo;

            noticiaNova.Informacao = noticia.Informacao;

            noticiaNova.UserId = await RetornaridUsuarioLogado();

            await _IAplicacaoNoticia.AdicionaNoticia(noticiaNova);

            return noticiaNova.Notificacoes;

        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AtualizaNoticia")]
        public async Task<List<Notifica>> AtualizaNoticia(NoticiaModel noticia)
        {
            var noticiaNova = await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);

            noticiaNova.Titulo = noticia.Titulo;

            noticiaNova.Informacao = noticia.Informacao;

            noticiaNova.UserId = await RetornaridUsuarioLogado();

            await _IAplicacaoNoticia.AtualizaNoticia(noticiaNova);

            return noticiaNova.Notificacoes;

        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ExcluirNoticia")]
        public async Task<List<Notifica>> ExcluirNoticia(NoticiaModel noticia)
        {
            var noticiaNova = await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);

            await _IAplicacaoNoticia.Excluir(noticiaNova);

            return noticiaNova.Notificacoes;

        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/BuscarPorId")]
        public async Task<Noticia> BuscarPorId(NoticiaModel noticia)
        {
            var noticiaNova = await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);

            return noticiaNova;

        }

        // retornar o usuario logado

        private async Task<string> RetornaridUsuarioLogado()
        {
            //  Se não for null, quer dizer q o usuario é logado
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");

                return idUsuario.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
