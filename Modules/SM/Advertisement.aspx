<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Advertisement.aspx.cs" Inherits="Modules_SM_Advertisement " Title="|| YANTRA : S&M : Advertisement  ||" %>

<%@ Register Src="AdvertisementAgenciesInfo.ascx" TagName="AdvertisementAgenciesInfo"   TagPrefix="uc1" %>
<%@ Register Src="AdvertisingAgency.ascx" TagName="AdvertisingAgency" TagPrefix="uc2" %>
<%@ Register Src="AdvertisingMode.ascx" TagName="AdvertisingMode" TagPrefix="uc3" %>
<%@ Register Src="AdvertisingMagzines.ascx" TagName="AdvertisingMagzines" TagPrefix="uc4" %>
<%@ Register Src="SizeOfAdvertising.ascx" TagName="SizeOfAdvertising" TagPrefix="uc5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                advertisement</td>
        </tr>
    </table>
    <table width="750">
        <tr>
            <td style="height: 21px">
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" BackColor="Transparent" CssClass="ajax__tab_xp">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                        <ContentTemplate>
                            <uc1:AdvertisementAgenciesInfo id="AdvertisementAgenciesInfo1" runat="server">
                            </uc1:AdvertisementAgenciesInfo>
                        </ContentTemplate>
                        <HeaderTemplate>
                        Advertisement Information
                        </HeaderTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                        <ContentTemplate>
                            <uc2:AdvertisingAgency id="AdvertisingAgency1" runat="server">
                            </uc2:AdvertisingAgency>
                        </ContentTemplate>
                        <HeaderTemplate>
                            Advertising Agency
                        </HeaderTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                        <ContentTemplate>
                            <uc3:AdvertisingMode id="AdvertisingMode1" runat="server">
                            </uc3:AdvertisingMode>
                        </ContentTemplate>
                        <HeaderTemplate>
                           Advertising Mode     
                        </HeaderTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
                        <ContentTemplate>
                            <uc4:AdvertisingMagzines id="AdvertisingMagzines1" runat="server">
                            </uc4:AdvertisingMagzines>
                        </ContentTemplate>
                        <HeaderTemplate>
                           Advertising Magzines
                        </HeaderTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel5">
                        <ContentTemplate>
                            <uc5:SizeOfAdvertising id="SizeOfAdvertising1" runat="server">
                            </uc5:SizeOfAdvertising>
                        </ContentTemplate>
                        <HeaderTemplate>
                           Size Of Advertising   
                        </HeaderTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer></td>
        </tr>
        <tr>
            <td style="text-align: left">
            </td>
        </tr>
    </table>
</asp:Content>

 
