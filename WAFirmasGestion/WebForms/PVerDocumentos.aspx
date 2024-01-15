<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/MPInicio.Master" AutoEventWireup="true" CodeBehind="PVerDocumentos.aspx.cs" Inherits="WAFirmasGestion.WebForms.PVerDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Documento: <asp:Label ID="lblNombreDocumento" runat="server"></asp:Label></h2>
    <iframe id="iframeDocumento" runat="server" style="width:100%; height:600px;"></iframe>

</asp:Content>
