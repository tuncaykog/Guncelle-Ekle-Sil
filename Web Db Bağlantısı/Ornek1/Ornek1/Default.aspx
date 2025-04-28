<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ornek1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblMesaj" runat="server" ForeColor="Green"></asp:Label><br />
            <asp:TextBox ID="txtCustomerCode" runat="server" placeholder="Müşteri Kodu"></asp:TextBox><br />
            <asp:TextBox ID="txtCustomerName" runat="server" placeholder="Müşteri Adı"></asp:TextBox><br />
            <asp:TextBox ID="txtCity" runat="server" placeholder="Şehir"></asp:TextBox><br />
            <asp:TextBox ID="txtCountry" runat="server" placeholder="Ülke"></asp:TextBox><br />
            <asp:TextBox ID="txtAdress" runat="server" placeholder="Adres"></asp:TextBox><br />
            <asp:TextBox ID="txtTelephone" runat="server" placeholder="Telefon"></asp:TextBox><br />
            <asp:Button ID="btnEkle" runat="server" Text="Ekle" OnClick="btnEkle_Click" /><br />
            <br />
            <asp:GridView ID="dbGridview" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="CustomerCode"
                OnRowEditing="dbGridview_RowEditing"
                OnRowUpdating="dbGridview_RowUpdating"
                OnRowCancelingEdit="dbGridview_RowCancelingEdit"
                OnRowDeleting="dbGridview_RowDeleting">

                <Columns>
                    <asp:BoundField DataField="CustomerCode" HeaderText="Müşteri Kodu" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Ad">
                        <ItemTemplate>
                            <%# Eval("CustomerName") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerName" runat="server" Text='<%# Bind("CustomerName") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Şehir">
                        <ItemTemplate>
                            <%# Eval("City") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ülke">
                        <ItemTemplate>
                            <%# Eval("Country") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCountry" runat="server" Text='<%# Bind("Country") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Adres">
                        <ItemTemplate>
                            <%# Eval("Addrees") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAdress" runat="server"
                                Text='<%# Bind("Addrees") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Telefon">
                        <ItemTemplate>
                            <%# Eval("Telephone") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTelephone" runat="server" Text='<%# Bind("Telephone") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
