<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Loader.ascx.cs" Inherits="InspectorIT.DataExpose.Loader" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/InspectorIT/DataExpose/js/DataExpose.js" Priority="1"  />
<div class="DataExpose">
    <asp:PlaceHolder runat="server" ID="phOutput"></asp:PlaceHolder>
</div>

<script type="text/javascript">
    var testData = $.ServicesFramework(<%=ModuleId %>);
</script>