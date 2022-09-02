using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly MyContext _applicationContext;

        public ClienteController()
        {
            _applicationContext = new MyContext();
        }

        [HttpGet]
        public async Task<List<Cliente>> ObterTodos()
        {
            DbSet<Cliente> clientesDB = _applicationContext.Clientes;

            List<Cliente> clientes = new List<Cliente>();
            if (clientesDB != null)
            {
                clientes = await clientesDB.ToListAsync<Cliente>();
            }

            return clientes;
        }

        [HttpPost]
        public void Criar([FromBody] Cliente model)
        {
            Cliente cliente = new Cliente()
            {
                CPF = model.CPF,
                DataExpedicao = model.DataExpedicao,
                DataNascimento = model.DataNascimento,
                EstadoCivil = model.EstadoCivil,
                Name = model.Name,
                OrgaoExpedicao = model.OrgaoExpedicao,
                RG = model.RG,
                Sexo = model.Sexo,
                UF = model.UF,
                Endereco = new Endereco()
                {
                    Bairro = model.Endereco.Bairro,
                    UF = model.Endereco.UF,
                    Cep = model.Endereco.Cep,
                    Cidade = model.Endereco.Cidade,
                    Complemento = model.Endereco.Complemento,
                    Logradouro = model.Endereco.Logradouro,
                    Numero = model.Endereco.Numero
                }
            };

            _applicationContext.Clientes.Add(cliente);

            _applicationContext.SaveChanges();
        }

        [HttpPut]
        public void Editar([FromBody] Cliente model)
        {
            _applicationContext.Enderecos.AddOrUpdate(model.Endereco);

            _applicationContext.Clientes.AddOrUpdate(model);

            _applicationContext.SaveChanges();
        }

        [HttpDelete]
        public void Deletar(int id)
        {
            Cliente cliente = _applicationContext.Clientes.Include("Endereco").Where(e => e.ClienteId == id).FirstOrDefault();

            _applicationContext.Clientes.Remove(cliente);

            _applicationContext.SaveChanges();                    
        }

        [HttpGet]
        public Cliente Procurar(int id)
        {
            Cliente clienteDB = _applicationContext.Clientes.Include("Endereco").Where(e => e.ClienteId == id).FirstOrDefault();
            if(clienteDB != null)
            {
                clienteDB.Endereco.Cliente = null;
            }

            return clienteDB;
        }


    }
}