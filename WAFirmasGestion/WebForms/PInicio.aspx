<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/MPInicio.Master" AutoEventWireup="true" CodeBehind="PInicio.aspx.cs" Inherits="WAFirmasGestion.WebForms.PInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-6 text-center">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-block my-3" Text="Realizar Solicitud" OnClick="btnHacerSolicitud_Click" />
                <asp:Button ID="Button2" runat="server" CssClass="btn btn-info btn-block my-3" Text="Gestionar Solicitudes" OnClick="btnGestionSolicitudes_Click" />
                <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning btn-block my-3" Text="Firmar Documentos" OnClick="btnFirmarDocumetos_Click" />
                <asp:Button ID="Button4" runat="server" CssClass="btn btn-success btn-block my-3" Text="Gestionar Documentos" OnClick="btnGestionarDocumetos_Click" />
            </div>
        </div>
    </div>
</asp:Content>


