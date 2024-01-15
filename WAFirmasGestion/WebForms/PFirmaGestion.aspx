<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/MPInicio.Master" AutoEventWireup="true" CodeBehind="PFirmaGestion.aspx.cs" Inherits="WAFirmasGestion.WebForms.PFirmaGestion" %>
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
                    Visible='<%# Eval("Estado").ToString() == "En espera" || Eval("Estado").ToString() == "Firmado" %>' /><asp:Button ID="BtnVerDocumento" runat="server" CommandName="Ver" 
                            CommandArgument='<%# Eval("DocumentId") %>'
                            Text="Ver Documento" CssClass="btn btn-ver" 
                            Visible='<%# Eval("Estado").ToString() == "En espera" || Eval("Estado").ToString() == "Firmado" %>' />

                <asp:Button ID="BtnRechazar" runat="server" CommandName="Rechazar"
                            CommandArgument='<%# Eval("DocumentId") %>'
                            Text="Rechazar" CssClass="btn btn-cancelar" 
                            Visible='<%# Eval("Estado").ToString() == "En espera" %>' />
                <asp:Button ID="Button2" runat="server" CommandName="Firmar"
                            CommandArgument='<%# Eval("DocumentId") %>'
                            Text="Firmar" CssClass="btn btn-cancelar" 
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
