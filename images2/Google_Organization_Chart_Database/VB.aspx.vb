
Imports System.Data
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class VB
    Inherits System.Web.UI.Page

    <WebMethod()> _
    Public Shared Function GetChartData() As List(Of Object)
        Dim query As String = "SELECT EmployeeId, Name, Designation, ReportingManager"
        query &= " FROM EmployeesHierarchy"
        Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                Dim chartData As New List(Of Object)()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        chartData.Add(New Object() {sdr("EmployeeId"), sdr("Name"), sdr("Designation"), sdr("ReportingManager")})
                    End While
                End Using
                con.Close()
                Return chartData
            End Using
        End Using
    End Function
End Class
