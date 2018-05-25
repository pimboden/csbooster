<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.Widget.UserControls.Repeaters.AssemblyInfo" CodeBehind="AssemblyInfo.ascx.cs" %>
<asp:Repeater ID="YMRP" runat="server" EnableViewState="False" OnItemDataBound="YMRP_ItemDataBound">
    <HeaderTemplate>
        <table cellspacing="5px">
            <tr>
                <th>
                    <%=language.GetString("FileName")%>
                </th>
                <th>
                    <%=language.GetString("FileVersion")%>
                </th>
                <th>
                    <%=language.GetString("ProductVersion")%>
                </th>
                <th>
                    <%=language.GetString("Creation")%>
                </th>
                <th>
                    <%=language.GetString("Modified")%>
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:PlaceHolder ID="Ph" runat="server"></asp:PlaceHolder>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
