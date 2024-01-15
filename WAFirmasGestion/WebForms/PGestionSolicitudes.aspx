<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/MPInicio.Master" AutoEventWireup="true" CodeBehind="PGestionSolicitudes.aspx.cs" Inherits="WAFirmasGestion.WebForms.PGestionSolicitudes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:GridView ID="GridViewSolicitudes" runat="server" AutoGenerateColumns="False" 
              OnRowCommand="GridViewSolicitudes_RowCommand"
              CssClass="table table-bordered table-hover table-responsive-md">
    <Columns>
        <asp:BoundField DataField="FileName" HeaderText="Documento" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" />

        <asp:TemplateField>
            <ItemTemplate>
               <asp:Button ID="Button1" runat="server" CommandName="Ver" 
                    CommandArgument='<%# Bind("DocumentId") %>' 
                    Text="Ver Documento" CssClass="btn btn-ver" 
                    Visible='<%# Eval("Estado").ToString() == "En espera" || Eval("Estado").ToString() == "Firmado" %>' />

                <asp:Button ID="BtnCancelar" runat="server" CommandName="Cancelar" 
                            CommandArgument='<%# Eval("DocumentId") %>'
                            Text="Cancelar" CssClass="btn btn-cancelar" 
                            Visible='<%# Eval("Estado").ToString() == "En espera" %>' />

                <asp:Button ID="BtnDescargar" runat="server" CommandName="Descargar" 
                            CommandArgument='<%# Eval("DocumentId") %>'
                            Text="Descargar" CssClass="btn btn-descargar" 
                            Visible='<%# Eval("Estado").ToString() == "Firmado" %>'/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


</asp:Content>
