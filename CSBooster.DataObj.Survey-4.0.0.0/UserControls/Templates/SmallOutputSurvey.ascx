<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmallOutputSurvey.ascx.cs" Inherits="_4screen.CSB.DataObj.UserControls.Templates.SmallOutputSurvey" %>
<div class="CSB_ov_survey">
    <div>
        <asp:PlaceHolder ID="phTitle" runat="server" EnableViewState="false">
            <div class="title">
                <asp:HyperLink ID="lnkTitle" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phDesc" runat="server" EnableViewState="false" Visible="false">
            <div class="desc">
                <asp:Literal ID="litDesc" runat="server" Visible="false" />
            </div>
        </asp:PlaceHolder>
    </div>
</div>
