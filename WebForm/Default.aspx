<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">   
     <script type="text/javascript">
         function getProducts() {
             $.getJSON("http://localhost:52801/api/Cliente/ObterTodos",
                 function (data) {
                     $('#clientes').empty(); // Clear the table body.

                     // Loop through the list of products.
                     $.each(data, function (key, val) {
                         // Add a table row for the product.
                         var row = '<td>' + val.Name + '</td><td>' + val.CPF + '</td><td>' + val.RG + '</td><td>' + val.DataExpedicao + '</td><td>' + val.UF + '</td><td>' + val.DataNascimento + '</td><td>' + val.Sexo + '</td><td>' + val.EstadoCivil + '</td>';
                         $('<tr/>', { html: row })  // Append the name.
                             .appendTo($('#clientes'));
                     });
                 });
         }

         $(document).ready(getProducts);
     </script>
   
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <div class="container body-content">

        <h2 class="text-center">Listagem de Clientes</h2>
        <div class="text-center">

            <div class="form-group">
                <a class="btn btn-primary" href="/Cliente/Create" style="float: right; margin: 10px">Criar Novo Cliente</a>
            </div>
            <table class="table">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>CPF</th>
                        <th>RG</th>
                        <th>Data de Expedição</th>
                        <th>UF de Expedição</th>
                        <th>Data de Nascimento</th>
                        <th>Sexo</th>
                        <th>Estado Civil</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody id="clientes">                   
                </tbody>

            </table>
        </div>
    </div>
</asp:Content>
