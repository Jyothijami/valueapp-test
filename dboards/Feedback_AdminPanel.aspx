<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" 
    CodeFile="Feedback_AdminPanel.aspx.cs" Inherits="dboards_Feedback_AdminPanel" %>
<%@ Register Src="~/Modules/widgets/TopRatingByEmp.ascx" TagPrefix ="uc1" TagName ="TopRatingByEmp" %>
<%@ Register Src="~/Modules/widgets/graph_RatingCountAnually.ascx" TagPrefix ="uc1" TagName ="graph_RatingCountAnually" %>

<%@ Register Src="~/Modules/widgets/graph_FeedbackCountMonthly.ascx" TagPrefix ="uc1" TagName="graph_FeedbackCountMonthly" %>
<%--<%@ Register Src ="~/Modules/widgets/graph_RatingCountMonthly.ascx" TagPrefix ="uc1" TagName ="graph_RatingCountMonthly" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <uc1:TopRatingByEmp runat ="server" ID="TopRatingByEmp" />
    <uc1:graph_RatingCountAnually runat ="server" ID="graph_RatingCountAnually" />

    <uc1:graph_FeedbackCountMonthly runat ="server" ID="graph_FeedbackCountMonthly" />
    <%--<uc1:graph_RatingCountMonthly runat ="server" ID="graph_RatingCountMonthly" />--%>
</asp:Content>

