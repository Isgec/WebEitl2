Imports OfficeOpenXml
Imports System.Web.Script.Serialization
Imports System.IO
Partial Class GF_pakPkgPortH
  Inherits SIS.SYS.GridBase
  Protected Sub GVpakPkgListH_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVpakPkgListH.RowCommand
    If e.CommandName.ToLower = "lgedit".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPkgListH.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim PkgNo As Int32 = GVpakPkgListH.DataKeys(e.CommandArgument).Values("PkgNo")
        Dim RedirectUrl As String = TBLpakPkgListH.EditUrl & "?SerialNo=" & SerialNo & "&PkgNo=" & PkgNo
        Response.Redirect(RedirectUrl)
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
    If e.CommandName.ToLower = "initiatewf".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPkgListH.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim PkgNo As Int32 = GVpakPkgListH.DataKeys(e.CommandArgument).Values("PkgNo")
        SIS.PAK.pakPkgListH.InitiatePortWF(SerialNo, PkgNo)
        GVpakPkgListH.DataBind()
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
    If e.CommandName.ToLower = "approvewf".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPkgListH.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim PkgNo As Int32 = GVpakPkgListH.DataKeys(e.CommandArgument).Values("PkgNo")
        SIS.PAK.pakPkgListH.ApproveWF(SerialNo, PkgNo)
        GVpakPkgListH.DataBind()
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
  End Sub
  Protected Sub GVpakPkgListH_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVpakPkgListH.Init
    DataClassName = "GpakPkgListH"
    SetGridView = GVpakPkgListH
  End Sub
  Protected Sub TBLpakPkgListH_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakPkgListH.Init
    SetToolBar = TBLpakPkgListH
  End Sub
  Protected Sub F_SerialNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles F_SerialNo.TextChanged
    Session("F_SerialNo") = F_SerialNo.Text
    Session("F_SerialNo_Display") = F_SerialNo_Display.Text
    InitGridPage()
  End Sub
  <System.Web.Services.WebMethod()>
  <System.Web.Script.Services.ScriptMethod()>
  Public Shared Function SerialNoCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    Return SIS.PAK.pakPO.SelectpakPOAutoCompleteList(prefixText, count, contextKey)
  End Function
  Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    F_SerialNo_Display.Text = String.Empty
    If Not Session("F_SerialNo_Display") Is Nothing Then
      If Session("F_SerialNo_Display") <> String.Empty Then
        F_SerialNo_Display.Text = Session("F_SerialNo_Display")
      End If
    End If
    F_SerialNo.Text = String.Empty
    If Not Session("F_SerialNo") Is Nothing Then
      If Session("F_SerialNo") <> String.Empty Then
        F_SerialNo.Text = Session("F_SerialNo")
      End If
    End If
    Dim strScriptSerialNo As String = "<script type=""text/javascript""> " &
      "function ACESerialNo_Selected(sender, e) {" &
      "  var F_SerialNo = $get('" & F_SerialNo.ClientID & "');" &
      "  var F_SerialNo_Display = $get('" & F_SerialNo_Display.ClientID & "');" &
      "  var retval = e.get_value();" &
      "  var p = retval.split('|');" &
      "  F_SerialNo.value = p[0];" &
      "  F_SerialNo_Display.innerHTML = e.get_text();" &
      "}" &
      "</script>"
    If Not Page.ClientScript.IsClientScriptBlockRegistered("F_SerialNo") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "F_SerialNo", strScriptSerialNo)
    End If
    Dim strScriptPopulatingSerialNo As String = "<script type=""text/javascript""> " &
      "function ACESerialNo_Populating(o,e) {" &
      "  var p = $get('" & F_SerialNo.ClientID & "');" &
      "  p.style.backgroundImage  = 'url(../../images/loader.gif)';" &
      "  p.style.backgroundRepeat= 'no-repeat';" &
      "  p.style.backgroundPosition = 'right';" &
      "  o._contextKey = '';" &
      "}" &
      "function ACESerialNo_Populated(o,e) {" &
      "  var p = $get('" & F_SerialNo.ClientID & "');" &
      "  p.style.backgroundImage  = 'none';" &
      "}" &
      "</script>"
    If Not Page.ClientScript.IsClientScriptBlockRegistered("F_SerialNoPopulating") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "F_SerialNoPopulating", strScriptPopulatingSerialNo)
    End If
    Dim validateScriptSerialNo As String = "<script type=""text/javascript"">" &
      "  function validate_SerialNo(o) {" &
      "    validated_FK_PAK_PkgListH_SerialNo_main = true;" &
      "    validate_FK_PAK_PkgListH_SerialNo(o);" &
      "  }" &
      "</script>"
    If Not Page.ClientScript.IsClientScriptBlockRegistered("validateSerialNo") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "validateSerialNo", validateScriptSerialNo)
    End If
    Dim validateScriptFK_PAK_PkgListH_SerialNo As String = "<script type=""text/javascript"">" &
      "  function validate_FK_PAK_PkgListH_SerialNo(o) {" &
      "    var value = o.id;" &
      "    var SerialNo = $get('" & F_SerialNo.ClientID & "');" &
      "    try{" &
      "    if(SerialNo.value==''){" &
      "      if(validated_FK_PAK_PkgListH_SerialNo.main){" &
      "        var o_d = $get(o.id +'_Display');" &
      "        try{o_d.innerHTML = '';}catch(ex){}" &
      "      }" &
      "    }" &
      "    value = value + ',' + SerialNo.value ;" &
      "    }catch(ex){}" &
      "    o.style.backgroundImage  = 'url(../../images/pkloader.gif)';" &
      "    o.style.backgroundRepeat= 'no-repeat';" &
      "    o.style.backgroundPosition = 'right';" &
      "    PageMethods.validate_FK_PAK_PkgListH_SerialNo(value, validated_FK_PAK_PkgListH_SerialNo);" &
      "  }" &
      "  validated_FK_PAK_PkgListH_SerialNo_main = false;" &
      "  function validated_FK_PAK_PkgListH_SerialNo(result) {" &
      "    var p = result.split('|');" &
      "    var o = $get(p[1]);" &
      "    var o_d = $get(p[1]+'_Display');" &
      "    try{o_d.innerHTML = p[2];}catch(ex){}" &
      "    o.style.backgroundImage  = 'none';" &
      "    if(p[0]=='1'){" &
      "      o.value='';" &
      "      try{o_d.innerHTML = '';}catch(ex){}" &
      "      __doPostBack(o.id, o.value);" &
      "    }" &
      "    else" &
      "      __doPostBack(o.id, o.value);" &
      "  }" &
      "</script>"
    If Not Page.ClientScript.IsClientScriptBlockRegistered("validateFK_PAK_PkgListH_SerialNo") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "validateFK_PAK_PkgListH_SerialNo", validateScriptFK_PAK_PkgListH_SerialNo)
    End If
  End Sub
  <System.Web.Services.WebMethod()>
  Public Shared Function validate_FK_PAK_PkgListH_SerialNo(ByVal value As String) As String
    Dim aVal() As String = value.Split(",".ToCharArray)
    Dim mRet As String = "0|" & aVal(0)
    Dim SerialNo As Int32 = CType(aVal(1), Int32)
    Dim oVar As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(SerialNo)
    If oVar Is Nothing Then
      mRet = "1|" & aVal(0) & "|Record not found."
    Else
      mRet = "0|" & aVal(0) & "|" & oVar.DisplayField
    End If
    Return mRet
  End Function
  Private st As Long = HttpContext.Current.Server.ScriptTimeout

  Private Sub cmdTmplUpload_Command(sender As Object, e As CommandEventArgs) Handles cmdTmplUpload.Command
    If IsUploaded.Value <> "YES" Then Exit Sub
    IsUploaded.Value = ""
    HttpContext.Current.Server.ScriptTimeout = Integer.MaxValue
    Dim AllowNegativeBalance As Boolean = Convert.ToBoolean(ConfigurationManager.AppSettings("AllowNegativeBalance"))
    If e.CommandName.ToLower = "tmplupload" Then
      Try
        Dim SerialNo As String = ""
        With F_FileUpload
          If .HasFile Then
            Dim tmpPath As String = Server.MapPath("~/../App_Temp")
            Dim tmpName As String = IO.Path.GetRandomFileName()
            Dim tmpFile As String = tmpPath & "\\" & tmpName
            .SaveAs(tmpFile)
            Dim fi As FileInfo = New FileInfo(tmpFile)
            Dim IsError As Boolean = False
            Dim ErrMsg As String = ""
            Using xlP As ExcelPackage = New ExcelPackage(fi)
              Dim wsD As ExcelWorksheet = Nothing
              Try
                wsD = xlP.Workbook.Worksheets("Data")
              Catch ex As Exception
                wsD = Nothing
              End Try
              '1. Process Document
              If wsD Is Nothing Then
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize("Invalid XL File") & "');", True)
                xlP.Dispose()
                HttpContext.Current.Server.ScriptTimeout = st
                Exit Sub
              End If

              Dim PkgNo As String = wsD.Cells(2, 3).Text

              Dim tmpPkg As SIS.PAK.pakPkgListH = SIS.PAK.pakPkgListH.pakPkgPortHGetByID(0, PkgNo)

              If tmpPkg.StatusID <> pakPkgStates.Free Then
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize("Port Packing List status is not FREE, cannot update packing list.") & "');", True)
                xlP.Dispose()
                HttpContext.Current.Server.ScriptTimeout = st
                Exit Sub
              End If

              Dim tmpSerialNo As String = ""
              Dim tmpPkgNo As String = ""
              Dim tmpBomNo As String = ""
              Dim tmpItemNo As String = ""

              Dim tmpPkgHWt As Decimal = 0
              Dim tmpQuantity As String = ""
              Dim notUpdatable As String = ""

              For I As Integer = 9 To 99999
                tmpSerialNo = wsD.Cells(I, 1).Text
                tmpPkgNo = wsD.Cells(I, 3).Text
                tmpBomNo = wsD.Cells(I, 4).Text
                tmpItemNo = wsD.Cells(I, 5).Text
                If tmpBomNo = String.Empty Then Exit For
                tmpQuantity = wsD.Cells(I, 24).Text.Trim
                notUpdatable = wsD.Cells(I, 8).Text
                '====================================
                If tmpQuantity = String.Empty Then Continue For
                If Not IsNumeric(tmpQuantity) Then Continue For
                If notUpdatable = "" Then Continue For
                '====================================
                Dim sTmpD = SIS.PAK.pakPkgListD.pakPkgListDGetByID(tmpSerialNo, tmpPkgNo, tmpBomNo, tmpItemNo)

                Dim Found As Boolean = True
                Dim OldQuantity As Decimal = 0
                Dim OldWeight As Decimal = 0
                Dim tmpListD As SIS.PAK.pakPkgListD = Nothing
                tmpListD = SIS.PAK.pakPkgListD.pakPkgListDGetByID(tmpSerialNo, PkgNo, tmpBomNo, tmpItemNo)
                If tmpListD Is Nothing Then
                  Dim tmpPOBItem As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(tmpSerialNo, tmpBomNo, tmpItemNo)
                  Found = False
                  tmpListD = New SIS.PAK.pakPkgListD
                  With tmpListD
                    .SerialNo = tmpSerialNo
                    .PkgNo = PkgNo
                    .BOMNo = tmpBomNo
                    .ItemNo = tmpItemNo
                    .UOMQuantity = tmpPOBItem.UOMQuantity
                    .UOMWeight = tmpPOBItem.UOMWeight
                    .WeightPerUnit = tmpPOBItem.WeightPerUnit
                    .DocumentNo = sTmpD.DocumentNo
                    .DocumentRevision = sTmpD.DocumentRevision
                    .PackTypeID = sTmpD.PackTypeID
                    .PackingMark = sTmpD.PackingMark
                    .UOMPack = sTmpD.UOMPack
                    .PackLength = sTmpD.PackLength
                    .PackWidth = sTmpD.PackWidth
                    .PackHeight = sTmpD.PackHeight
                    .Remarks = sTmpD.Remarks
                    .SourcePkgNo = tmpPkgNo
                    .Quantity = 0
                    .TotalWeight = 0
                  End With
                End If
                OldQuantity = tmpListD.Quantity
                OldWeight = tmpListD.TotalWeight
                tmpListD.Quantity = tmpQuantity
                '=============================
                'Total Weight of Packed Item
                Dim tmpPkgDwt As Decimal = 0
                tmpPkgDwt = SIS.PAK.pakPO.GetTotalWeight(tmpListD.Quantity, tmpListD.WeightPerUnit, tmpListD.UOMQuantity, tmpListD.UOMWeight)
                tmpListD.TotalWeight = tmpPkgDwt
                '======================
                If Found Then
                  If tmpListD.Quantity <= 0 Then
                    Try
                      SIS.PAK.pakPkgListD.pakPkgListDDelete(tmpListD)
                      sTmpD.QuantityBalance += OldQuantity
                      sTmpD.TotalWeightBalance += OldWeight
                      sTmpD = SIS.PAK.pakPkgListD.UpdateData(sTmpD)
                    Catch ex As Exception
                      IsError = True
                      ErrMsg = ErrMsg & IIf(ErrMsg = "", "", ", ") & tmpItemNo & "-D"
                    End Try
                  Else
                    Try
                      sTmpD.QuantityBalance = sTmpD.QuantityBalance + OldQuantity - tmpQuantity
                      If Not AllowNegativeBalance Then
                        If sTmpD.QuantityBalance < 0 Then
                          IsError = True
                          ErrMsg = ErrMsg & IIf(ErrMsg = "", "", ", (-)ve Balance NOT Allowed.") & tmpItemNo & "-U"
                          Continue For
                        End If
                      End If
                      tmpListD = SIS.PAK.pakPkgListD.UpdateData(tmpListD)
                      sTmpD.TotalWeightBalance = sTmpD.TotalWeightBalance + OldWeight - tmpPkgDwt
                      sTmpD = SIS.PAK.pakPkgListD.UpdateData(sTmpD)
                      tmpPkgHWt += tmpPkgDwt
                    Catch ex As Exception
                      IsError = True
                      ErrMsg = ErrMsg & IIf(ErrMsg = "", "", ", ") & tmpItemNo & "-U"
                    End Try

                  End If
                Else
                  If tmpListD.Quantity > 0 Then
                    Try
                      sTmpD.QuantityBalance = sTmpD.QuantityBalance + OldQuantity - tmpQuantity
                      If Not AllowNegativeBalance Then
                        If sTmpD.QuantityBalance < 0 Then
                          IsError = True
                          ErrMsg = ErrMsg & IIf(ErrMsg = "", "", ", (-)ve Balance NOT Allowed.") & tmpItemNo & "-I"
                          Continue For
                        End If
                      End If
                      tmpListD = SIS.PAK.pakPkgListD.InsertData(tmpListD)
                      sTmpD.TotalWeightBalance = sTmpD.TotalWeightBalance + OldWeight - tmpPkgDwt
                      sTmpD = SIS.PAK.pakPkgListD.UpdateData(sTmpD)
                      tmpPkgHWt += tmpPkgDwt
                    Catch ex As Exception
                      IsError = True
                      ErrMsg = ErrMsg & IIf(ErrMsg = "", "", ", ") & tmpItemNo & "-I"
                    End Try
                  End If
                End If
              Next 'Document Line
              'Update Total Wt if Pkg Items in Header
              tmpPkg.TotalWeight = tmpPkgHWt
              tmpPkg = SIS.PAK.pakPkgListH.UpdateData(tmpPkg)
              '======================================
              xlP.Dispose()
              If IsError Then
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize("ITEMS NOT INSERTED/UPDATED: " & ErrMsg) & "');", True)
              Else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize("Updated") & "');", True)
              End If
            End Using
          End If
        End With
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
    HttpContext.Current.Server.ScriptTimeout = st
  End Sub

End Class
