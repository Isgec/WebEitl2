Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  <DataObject()> _
  Partial Public Class pakMaterialStates
    Private Shared _RecordCount As Integer
    Private _MaterialStateID As Int32 = 0
    Private _Description As String = ""
    Public ReadOnly Property ForeColor() As System.Drawing.Color
      Get
        Dim mRet As System.Drawing.Color = Drawing.Color.Blue
        Try
          mRet = GetColor()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property Visible() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetVisible()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property Enable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetEnable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public Property MaterialStateID() As Int32
      Get
        Return _MaterialStateID
      End Get
      Set(ByVal value As Int32)
        _MaterialStateID = value
      End Set
    End Property
    Public Property Description() As String
      Get
        Return _Description
      End Get
      Set(ByVal value As String)
        _Description = value
      End Set
    End Property
    Public Readonly Property DisplayField() As String
      Get
        Return "" & _Description.ToString.PadRight(30, " ")
      End Get
    End Property
    Public Readonly Property PrimaryKey() As String
      Get
        Return _MaterialStateID
      End Get
    End Property
    Public Shared Property RecordCount() As Integer
      Get
        Return _RecordCount
      End Get
      Set(ByVal value As Integer)
        _RecordCount = value
      End Set
    End Property
    Public Class PKpakMaterialStates
      Private _MaterialStateID As Int32 = 0
      Public Property MaterialStateID() As Int32
        Get
          Return _MaterialStateID
        End Get
        Set(ByVal value As Int32)
          _MaterialStateID = value
        End Set
      End Property
    End Class
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakMaterialStatesSelectList(ByVal OrderBy As String) As List(Of SIS.PAK.pakMaterialStates)
      Dim Results As List(Of SIS.PAK.pakMaterialStates) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakMaterialStatesSelectList"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NvarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.PAK.pakMaterialStates)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakMaterialStates(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakMaterialStatesGetNewRecord() As SIS.PAK.pakMaterialStates
      Return New SIS.PAK.pakMaterialStates()
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakMaterialStatesGetByID(ByVal MaterialStateID As Int32) As SIS.PAK.pakMaterialStates
      Dim Results As SIS.PAK.pakMaterialStates = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakMaterialStatesSelectByID"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaterialStateID",SqlDbType.Int,MaterialStateID.ToString.Length, MaterialStateID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NvarChar, 9, HttpContext.Current.Session("LoginID"))
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.PAK.pakMaterialStates(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakMaterialStatesSelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String) As List(Of SIS.PAK.pakMaterialStates)
      Dim Results As List(Of SIS.PAK.pakMaterialStates) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sppakMaterialStatesSelectListSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
            Cmd.CommandText = "sppakMaterialStatesSelectListFilteres"
          End If
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NvarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.PAK.pakMaterialStates)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakMaterialStates(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function pakMaterialStatesSelectCount(ByVal SearchState As Boolean, ByVal SearchText As String) As Integer
      Return _RecordCount
    End Function
      'Select By ID One Record Filtered Overloaded GetByID
    <DataObjectMethod(DataObjectMethodType.Insert, True)> _
    Public Shared Function pakMaterialStatesInsert(ByVal Record As SIS.PAK.pakMaterialStates) As SIS.PAK.pakMaterialStates
      Dim _Rec As SIS.PAK.pakMaterialStates = SIS.PAK.pakMaterialStates.pakMaterialStatesGetNewRecord()
      With _Rec
        .Description = Record.Description
      End With
      Return SIS.PAK.pakMaterialStates.InsertData(_Rec)
    End Function
    Public Shared Function InsertData(ByVal Record As SIS.PAK.pakMaterialStates) As SIS.PAK.pakMaterialStates
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakMaterialStatesInsert"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Description",SqlDbType.NVarChar,31, Record.Description)
          Cmd.Parameters.Add("@Return_MaterialStateID", SqlDbType.Int, 11)
          Cmd.Parameters("@Return_MaterialStateID").Direction = ParameterDirection.Output
          Con.Open()
          Cmd.ExecuteNonQuery()
          Record.MaterialStateID = Cmd.Parameters("@Return_MaterialStateID").Value
        End Using
      End Using
      Return Record
    End Function
    <DataObjectMethod(DataObjectMethodType.Update, True)> _
    Public Shared Function pakMaterialStatesUpdate(ByVal Record As SIS.PAK.pakMaterialStates) As SIS.PAK.pakMaterialStates
      Dim _Rec As SIS.PAK.pakMaterialStates = SIS.PAK.pakMaterialStates.pakMaterialStatesGetByID(Record.MaterialStateID)
      With _Rec
        .Description = Record.Description
      End With
      Return SIS.PAK.pakMaterialStates.UpdateData(_Rec)
    End Function
    Public Shared Function UpdateData(ByVal Record As SIS.PAK.pakMaterialStates) As SIS.PAK.pakMaterialStates
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakMaterialStatesUpdate"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_MaterialStateID",SqlDbType.Int,11, Record.MaterialStateID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Description",SqlDbType.NVarChar,31, Record.Description)
          Cmd.Parameters.Add("@RowCount", SqlDbType.Int)
          Cmd.Parameters("@RowCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Con.Open()
          Cmd.ExecuteNonQuery()
          _RecordCount = Cmd.Parameters("@RowCount").Value
        End Using
      End Using
      Return Record
    End Function
    <DataObjectMethod(DataObjectMethodType.Delete, True)> _
    Public Shared Function pakMaterialStatesDelete(ByVal Record As SIS.PAK.pakMaterialStates) As Int32
      Dim _Result as Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakMaterialStatesDelete"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_MaterialStateID",SqlDbType.Int,Record.MaterialStateID.ToString.Length, Record.MaterialStateID)
          Cmd.Parameters.Add("@RowCount", SqlDbType.Int)
          Cmd.Parameters("@RowCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Con.Open()
          Cmd.ExecuteNonQuery()
          _RecordCount = Cmd.Parameters("@RowCount").Value
        End Using
      End Using
      Return _RecordCount
    End Function
'    Autocomplete Method
    Public Shared Function SelectpakMaterialStatesAutoCompleteList(ByVal Prefix As String, ByVal count As Integer, ByVal contextKey As String) As String()
      Dim Results As List(Of String) = Nothing
      Dim aVal() As String = contextKey.Split("|".ToCharArray)
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakMaterialStatesAutoCompleteList"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NvarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@prefix", SqlDbType.NVarChar, 50, Prefix)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@records", SqlDbType.Int, -1, count)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@bycode", SqlDbType.Int, 1, IIf(IsNumeric(Prefix), 0, 1))
          Results = New List(Of String)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Not Reader.HasRows Then
            Results.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("---Select Value---".PadRight(30, " "),""))
          End If
          While (Reader.Read())
            Dim Tmp As SIS.PAK.pakMaterialStates = New SIS.PAK.pakMaterialStates(Reader)
            Results.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Tmp.DisplayField, Tmp.PrimaryKey))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results.ToArray
    End Function
    Public Sub New(ByVal Reader As SqlDataReader)
      Try
        For Each pi As System.Reflection.PropertyInfo In Me.GetType.GetProperties
          If pi.MemberType = Reflection.MemberTypes.Property Then
            Try
              Dim Found As Boolean = False
              For I As Integer = 0 To Reader.FieldCount - 1
                If Reader.GetName(I).ToLower = pi.Name.ToLower Then
                  Found = True
                  Exit For
                End If
              Next
              If Found Then
                If Convert.IsDBNull(Reader(pi.Name)) Then
                  Select Case Reader.GetDataTypeName(Reader.GetOrdinal(pi.Name))
                    Case "decimal"
                      CallByName(Me, pi.Name, CallType.Let, "0.00")
                    Case "bit"
                      CallByName(Me, pi.Name, CallType.Let, Boolean.FalseString)
                    Case Else
                      CallByName(Me, pi.Name, CallType.Let, String.Empty)
                  End Select
                Else
                  CallByName(Me, pi.Name, CallType.Let, Reader(pi.Name))
                End If
              End If
            Catch ex As Exception
            End Try
          End If
        Next
      Catch ex As Exception
      End Try
    End Sub
    Public Sub New()
    End Sub
  End Class
End Namespace
