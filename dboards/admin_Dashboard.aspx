<%@ Page Title="|| Value App ||" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" 
    CodeFile="admin_Dashboard.aspx.cs" Inherits="dboards_admin_Dashboard" %>

<%@ Register src="../Modules/widgets/SalesLead_OpenVsClose.ascx" tagname="SalesLead_OpenVsClose" tagprefix="uc1" %>
<%@ Register Src="~/Modules/widgets/SalesQuot_AnnualRpt.ascx" TagPrefix="uc1" TagName="SalesQuot_AnnualRpt" %>
<%@ Register Src="~/Modules/widgets/itemAvailability.ascx" TagPrefix="uc1" TagName="itemAvailability" %>
<%@ Register Src="~/Modules/widgets/graph_topMemoUsers.ascx" TagPrefix="uc1" TagName="graph_topMemoUsers" %>
<%@ Register Src="~/Modules/widgets/chartJSImporter.ascx" TagPrefix="uc1" TagName="chartJSImporter" %>
<%@ Register Src="~/Modules/widgets/graph_topEmpLeaves.ascx" TagPrefix="uc1" TagName="graph_topEmpLeaves" %>
<%@ Register Src="~/Modules/widgets/graph_SalesQuotationsMonthly2.ascx" TagPrefix="uc1" TagName="graph_SalesQuotationsMonthly2" %>
<%@ Register Src="~/Modules/widgets/graphs_QuotVsPO.ascx" TagPrefix="uc1" TagName="graphs_QuotVsPO" %>
<%@ Register Src="~/Modules/widgets/graph_SalesQuotationsMonthly.ascx" TagPrefix="uc1" TagName="graph_SalesQuotationsMonthly" %>
<%@ Register Src="~/Modules/widgets/graph_SalesPOMonthly.ascx" TagPrefix="uc1" TagName="graph_SalesPOMonthly" %>
<%@ Register Src="~/Modules/widgets/graph_BrandPOMonthly.ascx" TagPrefix="uc1" TagName="graph_BrandPOMonthly" %>
<%@ Register Src="~/Modules/widgets/graph_BrandDCMonthly.ascx" TagPrefix="uc1" TagName="graph_BrandDCMonthly" %>
<%@ Register Src="~/Modules/widgets/graph_POAmtMonthly.ascx" TagPrefix="uc1" TagName="graph_POAmtMonthly" %>






<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <uc1:chartJSImporter runat="server" ID="chartJSImporter" />
    <uc1:SalesLead_OpenVsClose ID="SalesLead_OpenVsClose1" runat="server" />
    <uc1:SalesQuot_AnnualRpt runat="server" ID="SalesQuot_AnnualRpt" />
<%--    <uc1:itemAvailability runat="server" ID="itemAvailability" />--%>
    <uc1:graph_topMemoUsers runat="server" ID="graph_topMemoUsers" />
    <uc1:graph_topEmpLeaves runat="server" id="graph_topEmpLeaves" />
    <br />
    <uc1:graphs_QuotVsPO runat="server" ID="graphs_QuotVsPO" />
    <uc1:graph_SalesQuotationsMonthly runat="server" ID="graph_SalesQuotationsMonthly" />
    <uc1:graph_SalesPOMonthly runat="server" ID="graph_SalesPOMonthly" />
    <uc1:graph_BrandPOMonthly runat="server" ID="graph_BrandPOMonthly" />
    <uc1:graph_BrandDCMonthly runat="server" ID="graph_BrandDCMonthly" />
    <uc1:graph_POAmtMonthly runat="server" ID="graph_POAmtMonthly" />
</asp:Content>


