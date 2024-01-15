<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/MPInicio.Master" AutoEventWireup="true" CodeBehind="PHacerSolicitud.aspx.cs" Inherits="WAFirmasGestion.WebForms.PHacerSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:FileUpload ID="fileUploadDocumento" runat="server" onchange="handleFileSelect(this)" />

        <br />
        <iframe id="iframeDocumento" runat="server" style="width:100%; height:500px;"></iframe>
        <br />
        <asp:Button ID="btnFirmar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
    </div>

    <script type="text/javascript">
        function handleFileSelect(input) {
    if (input.files && input.files.length > 0) {
        var file = input.files[0];

        if (file.type === "application/pdf") {
            var reader = new FileReader();

            reader.onload = function (e) {
                var iframe = document.getElementById('<%= iframeDocumento.ClientID %>');
                iframe.src = e.target.result;
            };

            reader.readAsDataURL(file);
        } else {
            alert("Por favor, seleccione un archivo PDF.");
                }
        }
    }

    </script>
</asp:Content>
