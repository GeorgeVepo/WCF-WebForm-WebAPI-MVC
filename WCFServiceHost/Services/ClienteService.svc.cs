using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using WCFServiceHost.Models;

namespace WCFServiceHost.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ClienteService
    {
        private readonly MyContext _applicationContext;
        public ClienteService()
        {
            _applicationContext = new MyContext();
        }

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
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

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Procurar/{id}")]
        public Cliente Procurar(string id)
        {
            int idNum = int.Parse(id);
            Cliente clienteDB = _applicationContext.Clientes.Include("Endereco").Where(e => e.ClienteId == idNum).FirstOrDefault();
            if (clienteDB != null)
            {
                clienteDB.Endereco.Cliente = null;
            }

            return clienteDB;
        }

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Criar", Method = "OPTIONS")]
        public void CriarOPTIONS(Cliente model)
        {
           
        }

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Criar", Method = "POST")]
        public void Criar(Cliente model)
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


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Editar", Method = "OPTIONS")]
        public void EditarOPTIONS(Cliente model)
        {
        }


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Editar", Method = "PUT")]
        public void Editar(Cliente model)
        {
            _applicationContext.Enderecos.AddOrUpdate(model.Endereco);

            _applicationContext.Clientes.AddOrUpdate(model);

            _applicationContext.SaveChanges();
        }


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Deletar/{id}", Method = "DELETE")]
        public void Deletar(string id)
        {
            int idNum = int.Parse(id);
            Cliente cliente = _applicationContext.Clientes.Include("Endereco").Where(e => e.ClienteId == idNum).FirstOrDefault();

            _applicationContext.Clientes.Remove(cliente);

            _applicationContext.SaveChanges();
        }


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Deletar/{id}", Method = "OPTIONS")]
        public void DeletarOPTIONS(string id)
        {
        }

    }
}
