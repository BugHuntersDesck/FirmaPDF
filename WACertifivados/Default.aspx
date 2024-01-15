<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WACertifivados._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridViewArchivos" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre del Archivo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnFirmar" runat="server" Text="Firmar" CommandArgument='<%# Eval("RutaArchivo") %>' OnClick="btnFirmar_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
