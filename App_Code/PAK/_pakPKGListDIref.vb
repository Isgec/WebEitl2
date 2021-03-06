﻿Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  <DataObject()>
  Partial Public Class pakPKGListDIRef
    Public Property SerialNo As Integer = 0
    Public Property PKGNo As Integer = 0
    Public Property ItemReference As String = ""
    Public Property SubItem As String = ""
    Public Property TotalWeight As Decimal = 0
    Public Property ProgressPercent As Decimal = 0
    'Add Total Weight Field and Update It When Received or Quantity Updated
    Public Shared Function GetReceivedPKGSiteDIref(ByVal RecNo As Integer) As List(Of SIS.PAK.pakPKGListDIRef)
      Dim Results As New List(Of SIS.PAK.pakPKGListDIRef)
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.RecNo As PkgNo, "
      Sql &= "   sum(pkg.TotalWeight) as TotalWeight, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Sql &= " from PAK_SitePkgD as pkg "
      Sql &= "   inner join PAK_POBItems as itm "
      Sql &= "      on pkg.SerialNo = itm.SerialNo "
      Sql &= " 	   and pkg.BOMNo = itm.BOMNo "
      Sql &= " 	   and pkg.ItemNo = itm.ItemNo "
      Sql &= " where "
      Sql &= "   pkg.RecNo = " & RecNo
      Sql &= " group by "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.RecNo, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Dim tmp As New SIS.PAK.pakPKGListDIRef(Reader)
            Try
              tmp.ProgressPercent = (tmp.TotalWeight / SIS.PAK.pakPO.pakPOGetByID(tmp.SerialNo).POWeight) * 100
            Catch ex As Exception
            End Try
            Results.Add(tmp)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function


    Public Shared Function GetDespatchedPKGPortDIref(ByVal PkgNo As Integer) As List(Of SIS.PAK.pakPKGListDIRef)
      Dim Results As New List(Of SIS.PAK.pakPKGListDIRef)
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.PKGNo, "
      Sql &= "   sum(pkg.TotalWeight) as TotalWeight, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Sql &= " from PAK_PkgListD as pkg "
      Sql &= "   inner join PAK_POBItems as itm "
      Sql &= "      on pkg.SerialNo = itm.SerialNo "
      Sql &= " 	   and pkg.BOMNo = itm.BOMNo "
      Sql &= " 	   and pkg.ItemNo = itm.ItemNo "
      Sql &= " where "
      Sql &= "   pkg.PKGNo = " & PkgNo
      Sql &= " group by "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.PKGNo, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Dim tmp As New SIS.PAK.pakPKGListDIRef(Reader)
            Try
              tmp.ProgressPercent = (tmp.TotalWeight / SIS.PAK.pakPO.pakPOGetByID(tmp.SerialNo).POWeight) * 100
            Catch ex As Exception
            End Try
            Results.Add(tmp)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function GetDespatchedPKGListDIref(ByVal hPKG As SIS.PAK.pakPkgListH) As List(Of SIS.PAK.pakPKGListDIRef)
      Dim Results As New List(Of SIS.PAK.pakPKGListDIRef)
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.PKGNo, "
      Sql &= "   sum(pkg.TotalWeight) as TotalWeight, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Sql &= " from PAK_PkgListD as pkg "
      Sql &= "   inner join PAK_POBItems as itm "
      Sql &= "      on pkg.SerialNo = itm.SerialNo "
      Sql &= " 	   and pkg.BOMNo = itm.BOMNo "
      Sql &= " 	   and pkg.ItemNo = itm.ItemNo "
      Sql &= " where "
      Sql &= "       pkg.SerialNo = " & hPKG.SerialNo
      Sql &= "   and pkg.PKGNo = " & hPKG.PkgNo
      Sql &= " group by "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.PKGNo, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Dim tmp As New SIS.PAK.pakPKGListDIRef(Reader)
            Try
              tmp.ProgressPercent = (tmp.TotalWeight / hPKG.FK_PAK_PkgListH_SerialNo.POWeight) * 100
            Catch ex As Exception
            End Try
            Results.Add(tmp)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
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
