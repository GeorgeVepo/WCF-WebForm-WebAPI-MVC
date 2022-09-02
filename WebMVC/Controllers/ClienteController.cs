using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController()
        {
            _clienteService = new ClienteService();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _clienteService.FindAllClients());
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.PopulaViewBag();
            return View(new Cliente());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Cliente model)
        {
            if (!ModelState.IsValid)
            {
                this.PopulaViewBag();
                return View();
            }

            await _clienteService.CreateClient(model);

            return View("Index", await _clienteService.FindAllClients());
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            this.PopulaViewBag();

            Cliente cliente = await _clienteService.FindClientById(id);
            return View(cliente);
        }

        public async Task<ActionResult> Edit(Cliente model)
        {

            if (!ModelState.IsValid)
            {
                this.PopulaViewBag();
                return View();
            }


            await _clienteService.UpdateClient(model);

            return View("Index", await _clienteService.FindAllClients());
        }


        public async Task<ActionResult> Delete(int id)
        {
            this.PopulaViewBag();

            await _clienteService.DeleteClientById(id);

            return View("Index", await _clienteService.FindAllClients());
        }

        [NonAction]
        public void PopulaViewBag()
        {
            List<SelectListItem> listSexo = new List<SelectListItem>();

            listSexo.Add(new SelectListItem()
            {
                Text = "Selecione",
                Value = null
            });

            listSexo.Add(new SelectListItem()
            {
                Text = "Masculino",
                Value = "Masculino"
            });

            listSexo.Add(new SelectListItem()
            {
                Text = "Feminino",
                Value = "Feminino"
            });

            List<SelectListItem> listEstadoCivil = new List<SelectListItem>();

            listEstadoCivil.Add(new SelectListItem()
            {
                Text = "Selecione",
                Value = null
            });

            listEstadoCivil.Add(new SelectListItem()
            {
                Text = "Solteiro",
                Value = "Solteiro"
            });

            listEstadoCivil.Add(new SelectListItem()
            {
                Text = "Casado",
                Value = "Casado"
            });


            ViewBag.SexoList = new SelectList(listSexo, "Value", "Text");
            ViewBag.EstadoCivilList = new SelectList(listEstadoCivil, "Value", "Text");
        }


    }
}