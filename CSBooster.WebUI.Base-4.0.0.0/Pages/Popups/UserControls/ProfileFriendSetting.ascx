<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.Pages.Popups.UserControls.ProfileFriendSetting" Codebehind="ProfileFriendSetting.ascx.cs" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div>
	<telerik:RadPanelBar ID="RPB" Runat="server" Width="510" ExpandMode="SingleExpandedItem" AllowCollapseAllItems="True">
	<CollapseAnimation Type="None" Duration="100"></CollapseAnimation>
		<Items>
			<telerik:RadPanelItem runat="server" EnableViewState="False" Expanded="True">
			    
				<Items>
					<telerik:RadPanelItem EnableViewState="False">
						<ItemTemplate> 
							<div>
							<table cellpadding="0" cellspacing="3" width="100%">
								<tr>
									<th align="left">
										<%=language.GetString("Information")%>
									</th>
									<th>
									    <%=language.GetString("VisibilityAll")%>
									</th>
									<th>
									    <%=language.GetString("VisibilityFriend")%>
									</th>
								</tr>
								<tr>
									<td colspan="3">
										<hr />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("FirstName")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_VORNAME" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_VORNAME" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Name")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_NAME" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_NAME" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Sex")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_SEX" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_SEX" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Birthday")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_GEB" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_GEB" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("City")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_ORT" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_ORT" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Country")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_LAND" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_LAND" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Language")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_LANG" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_LANG" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Status")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_REL" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_REL" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("AttractedTo")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_NEIG" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_NEIG" runat="server" EnableViewState="False" />
									</td>
								</tr>
							</table>
							</div>
						</ItemTemplate> 
					</telerik:RadPanelItem>				
			</Items>
			</telerik:RadPanelItem>
			<telerik:RadPanelItem runat="server" EnableViewState="False">
				<Items>
					<telerik:RadPanelItem EnableViewState="False">
						<ItemTemplate>
							<div>
							<table cellpadding="0" cellspacing="3" width="100%">
								<tr>
									<th align="left">
										<%=language.GetString("Information")%>
									</th>
									<th>
									    <%=language.GetString("VisibilityAll")%>
									</th>
									<th>
									    <%=language.GetString("VisibilityFriend")%>
									</th>
								</tr>
								<tr>
									<td colspan="3">
										<hr />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Mobile")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_HAND" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_HAND" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Phone")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_TEL" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_TEL" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("MSN")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_MSN" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_MSN" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Yahoo")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_YAH" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_YAH" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Skype")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_SKY" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_SKY" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("ICQ")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_ICQ" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_ICQ" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("AIM")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_AIM" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_AIM" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Homepage")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_HOME" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_HOME" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("Blog")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_BLOG" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_BLOG" runat="server" EnableViewState="False" />
									</td>
								</tr>
							</table>
							</div>
						</ItemTemplate> 
					</telerik:RadPanelItem>
				</Items>
			</telerik:RadPanelItem>
			<telerik:RadPanelItem runat="server" EnableViewState="False">
				<Items>
					<telerik:RadPanelItem EnableViewState="False">
						<ItemTemplate>
							<div>
							<table cellpadding="0" cellspacing="3" width="100%">
								<tr>
									<th align="left">
										<%=language.GetString("Information")%>
									</th>
									<th>
									    <%=language.GetString("VisibilityAll")%>
									</th>
									<th>
									    <%=language.GetString("VisibilityFriend")%>
									</th>
								</tr>
								<tr>
									<td colspan="3">
										<hr />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("EyeColor")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_EYE" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_EYE" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("HairColor")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_HAIR" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_HAIR" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("BodyHeight")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_GRO" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_GRO" runat="server" EnableViewState="False" />
									</td>
								</tr>
								<tr>
									<td>
									    <%=language.GetString("BodyWeight")%>:
									</td>
									<td align="center">
										<asp:CheckBox ID="CBA_GEW" runat="server" EnableViewState="False" Enabled="False" />
									</td>
									<td align="center">
										<asp:CheckBox ID="CBS_GEW" runat="server" EnableViewState="False" />
									</td>
								</tr>
							</table>
							</div>
						</ItemTemplate> 
					</telerik:RadPanelItem>
				</Items>
			</telerik:RadPanelItem>
		</Items>
	<ExpandAnimation Type="None" Duration="100"></ExpandAnimation>
	</telerik:RadPanelBar>
</div>
<div class="inputBlockContent" style="float:left; padding-left:10px;">
  <asp:LinkButton ID="lbS" CssClass="inputButton" OnClick="lbS_Click" runat="server"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
   <%=languageShared.GetString("TextOr")%> 
  <b><a href="javascript:CloseWindow();"><%= languageShared.GetString("CommandCancel")%></a></b>
</div>
