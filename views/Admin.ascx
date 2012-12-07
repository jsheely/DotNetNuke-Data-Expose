<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="InspectorIT.DataExpose.views.Admin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/InspectorIT/DataExpose/css/jquery-ui-1.9.1.custom.css"></dnn:DnnCssInclude>
<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/InspectorIT/DataExpose/js/codemirror/lib/codemirror.css"></dnn:DnnCssInclude>
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/InspectorIT/DataExpose/js/DataExpose.Admin.js" Priority="2"  />

<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/InspectorIT/DataExpose/js/CodeMirror/lib/codemirror.js" Priority="3"  />
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/InspectorIT/DataExpose/js/CodeMirror/mode/javascript/javascript.js" Priority="4"  />
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/InspectorIT/DataExpose/js/CodeMirror/mode/xml/xml.js" Priority="4"  />


<table border="0" cellspacing="2" cellpadding="2" class="iit_Admin">
    <tr>
        <td valign="top" class="iit_LeftColumn">
            <div class="iit_feedList">
                <ul>
                    <li>Loading...</li>
                </ul>
            </div>
        </td>
        <td valign="top">
            <div class="iit_feedDetails">
                <div id="iit_tabs">
	                <ul>
		                <li><a href="#tabs-1">Settings</a></li>
		                <li><a href="#tabs-2">SQL</a></li>
                        <li><a href="#tabs-3">Settings</a></li>
                        <li><a href="#tabs-4">Test</a></li>
                        <li style="float:right"><a href="#tabs-5" id="iit_NewFeed">New</a></li>
	                </ul>
	                <div id="tabs-1">
		                <h3>Feed Settings</h3>
                        <input type="hidden" id="iit_ID" name="iit_ID" />
                        <p>
                            <strong>Feed Name</strong><br />
                            <input type="text" name="iit_feedName" id="iit_feedName" class="iit_largeTextbox" />
                        </p>

                        <p>
                            <strong>Feed Description</strong><br />
                            <textarea id="iit_feedDesc" name="iit_feedDesc" class="iit_largeTextbox iit_textArea"></textarea>
       
                        </p>

	                </div>
	                <div id="tabs-2">
		                <h3>SQL Data</h3>
                        <p>Enter a stored procedure name (without database owner or object qualifier) OR custom sql statement.</p>
                        <p>
                            <div><strong>Stored Procedure</strong></div>
                            <div>
                                <input type="text" ID="iit_ProcName" name="iit_StoredProc" class="iit_largeTextbox" />
                            </div>
                        </p>
                        <p>
                            <div><strong>SQL</strong></div>
                            <div><textarea id="iit_SQL" name="iit_SQL" class="iit_largeTextbox iit_SQLBox"></textarea></div>
                        </p>
	                </div>
                    <div id="tabs-3">
                        <h3>Feed Settings</h3>
                        <p>Enter a comma delimited list of roles whom you want to grant access this this feed.</p>
                        <div class="dnnFormItem">
                            <div class="dnnFormLabel">
                                <label>Security Roles</label>
                            </div>
                            <input type="text" name="iit_Roles" id="iit_Roles" class="iit_largeTextbox"/>
                        </div>
                        
                    </div>
                    <div id="tabs-4">
                        <h3>Test Feed</h3>
                        <p>Test your feed to ensure it's returning the data you expect.</p>
                        <div class="dnnClear">
                            <div style="float:left; width:532px;"><input type="text" name="iit_feedUrl" id="iit_feedUrl" class="iit_largeTextbox" /></div>
                            <div style="float:right;"><input type="button" id="iit_BtnTestFeed" value="Send" name="iit_BtnTestFeed" /> </div>
                        </div>
                        <div>
                            <div class="dnnFormItem">
                                <div class="dnnFormLabel">
                                    <label>Content Type</label>
                                </div>
                                <select name="iit_ContentType" id="iit_ContentType">
                                    <option value="json">JSON</option>
                                    <option value="xml">XML</option>
                                </select>
                            </div>
                            
                        </div>
                        <div class="iit_feedResults" id="iit_feedResults">
                            <iframe src="" id="iit_iFrame" style="width:100%;height:100%;"></iframe>

                        </div>

                    </div>
                    <div id="tabs-5"></div>
                </div>

                <div class="iit_alerts">
                    <p>Your settings have been saved.</p>
                </div>
    
                <ul class="dnnActions">
                    <li><a href="#" id="iit_LnkSave" class="dnnPrimaryAction">Save</a></li>
                    <li><a href="#" id="iit_TestFeed" class="dnnSecondaryAction">Test Feed</a></li>
                    <li><a href="#" id="iit_LnkDelete" class="dnnSecondaryAction">Delete</a></li>

                </ul>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">
    var myData;
    $(document).ready(function () {

        var options = {
            servicesFramework: $.ServicesFramework(<%=ModuleId%>)
        };

        $.DataExpose.Admin.Init(options);

    });

</script>