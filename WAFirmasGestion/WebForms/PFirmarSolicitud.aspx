<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/MPInicio.Master" AutoEventWireup="true" CodeBehind="PFirmarSolicitud.aspx.cs" Inherits="WAFirmasGestion.WebForms.PFirmarSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnSubir" runat="server" Text="Subir y Encriptar Firma" OnClick="btnSubir_Click" />
        <br /><br />
        <asp:FileUpload ID="FileUploadPDF" runat="server" />
        <asp:Button ID="btnSubirPDF" runat="server" Text="Subir y Firmar PDF" OnClick="btnSubirPDF_Click" />
        <br /><br />
    </div>
    </div>
</asp:Content>
