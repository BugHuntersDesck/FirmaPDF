<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PGestionFirmas.aspx.cs" Inherits="WAFirmaPDF.PGestionFirmas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridViewFirmas" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewFirmas_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Documento" HeaderText="Documento" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" />
            <asp:TemplateField HeaderText="Acción">
                <ItemTemplate>
                    <asp:LinkButton ID="btnVerDocumento" runat="server" CommandArgument='<%# Eval("Id") %>' Visible="false">Ver Documento</asp:LinkButton>
                    <asp:LinkButton ID="btnDescargar" runat="server" CommandArgument='<%# Eval("Id") %>' Visible="false">Descargar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
