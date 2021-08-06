<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="tt.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvttvt" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
                <Columns>
                    <asp:BoundField  HeaderText="Tháng/Năm" DataField="THOIGIAN"/>
                    <asp:BoundField  HeaderText="Mã Tổ Kỹ Thuật" DataField="MATKT"/>
                    <asp:BoundField  HeaderText="SL Thuê Bao Mới Phát Triển" DataField="SL_TBMPT"/>
                    <asp:BoundField  HeaderText="SL Thuê Bao Mở Lại" DataField="SL_TBML"/>
                    <asp:BoundField  HeaderText="SL Thuê Bao Ngưng Huỷ" DataField="SL_TBNH"/>
                    <asp:BoundField  HeaderText="Tỉ Lệ Giảm Thuê Bao Huỷ (%)" DataField="TL_GTBH"/>

                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
