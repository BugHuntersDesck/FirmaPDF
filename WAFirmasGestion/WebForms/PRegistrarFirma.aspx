<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/MPInicio.Master" AutoEventWireup="true" CodeBehind="PRegistrarFirma.aspx.cs" Inherits="WAFirmasGestion.WebForms.PRegistrarFirma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnSubir" runat="server" Text="Registrar Firma" OnClick="btnSubir_Click" />
</asp:Content>
