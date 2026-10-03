using Microsoft.Data.SqlClient;
using POSS.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace POSS
{
    public partial class PosPage : Page
    {
        ObservableCollection<CartItem> cart = new ObservableCollection<CartItem>();
        private readonly string connectionString = DatabaseConfig.ConnectionString;
        public PosPage(){InitializeComponent();CartGrid.ItemsSource=cart;txtQty.SelectedIndex=0;}
        private void btnAdd_Click(object sender,RoutedEventArgs e){try{if(string.IsNullOrWhiteSpace(txtName.Text)){MessageBox.Show("商品名を入力してください");return;}if(!int.TryParse(txtPrice.Text,out int price)){MessageBox.Show("価格を正しく入力してください");return;}int qty=int.Parse(((ComboBoxItem)txtQty.SelectedItem).Content.ToString());var existingItem=cart.FirstOrDefault(x=>x.Name==txtName.Text);if(existingItem!=null){existingItem.Quantity+=qty;CartGrid.Items.Refresh();UpdateTotal();ClearInput();return;}cart.Add(new CartItem(){No=cart.Count+1,Name=txtName.Text,Price=price,Quantity=qty});UpdateTotal();ClearInput();}catch(Exception ex){MessageBox.Show(ex.Message);}}
        private void UpdateTotal(){int total=cart.Sum(x=>x.Subtotal);txtTotal.Text="¥"+total;txtSubTotal.Text="¥"+total;txtItemCount.Text=cart.Sum(x=>x.Quantity)+"点";}
        private void ClearInput(){txtBarcode.Clear();txtName.Clear();txtPrice.Clear();txtQty.SelectedIndex=0;txtBarcode.Focus();}
        private void Delete_Click(object sender,RoutedEventArgs e){Button btn=sender as Button;CartItem item=btn.Tag as CartItem;if(item!=null)cart.Remove(item);int no=1;foreach(var i in cart)i.No=no++;CartGrid.Items.Refresh();UpdateTotal();}
        private void txtBarcode_KeyDown(object sender,KeyEventArgs e){if(e.Key==Key.Enter)SearchProduct(txtBarcode.Text);}
        private void SearchProduct(string barcode){try{using SqlConnection conn=new SqlConnection(connectionString);conn.Open();string query=@"SELECT TOP 1 * FROM [Table] WHERE Barcode=@Barcode";using SqlCommand cmd=new SqlCommand(query,conn);cmd.Parameters.AddWithValue("@Barcode",barcode);SqlDataReader reader=cmd.ExecuteReader();if(reader.Read()){txtName.Text=reader["Name"].ToString();txtPrice.Text=reader["Price"].ToString();}else MessageBox.Show("Product not found");reader.Close();}catch(Exception ex){MessageBox.Show(ex.Message);}}
        private void txtReceived_TextChanged(object sender,TextChangedEventArgs e){if(!int.TryParse(txtReceived.Text,out int received)){txtChange.Clear();return;}int total=cart.Sum(x=>x.Subtotal);txtChange.Text=(received-total).ToString();}
        private void btnPayment_Click(object sender,RoutedEventArgs e){if(cart.Count==0){MessageBox.Show("カートが空です");return;}if(!int.TryParse(txtReceived.Text,out int received)){MessageBox.Show("お預かり金額を入力してください");return;}int total=cart.Sum(x=>x.Subtotal);if(received<total){MessageBox.Show("お金が足りません");return;}string receipt="====================\n      POS レジ\n====================\n\n";foreach(var item in cart){receipt+=$"{item.Name} x {item.Quantity}\n";receipt+=$"¥{item.Price} × {item.Quantity} = ¥{item.Subtotal}\n\n";}receipt+="--------------------\n"+$"合計      : ¥{total}\n"+$"お預かり  : ¥{received}\n"+$"お釣り    : ¥{received-total}\n\nありがとうございました\n====================";MessageBox.Show("会計が完了しました。","完了",MessageBoxButton.OK,MessageBoxImage.Information);SaveSale();cart.Clear();UpdateTotal();txtReceived.Clear();txtChange.Clear();txtBarcode.Focus();}
        private void SaveSale(){try{using SqlConnection conn=new SqlConnection(connectionString);conn.Open();foreach(var item in cart){string query=@"INSERT INTO Sales (ProductName,Price,Qty,Total,SaleDate) VALUES (@ProductName,@Price,@Qty,@Total,@SaleDate)";using SqlCommand cmd=new SqlCommand(query,conn);cmd.Parameters.AddWithValue("@ProductName",item.Name);cmd.Parameters.AddWithValue("@Price",item.Price);cmd.Parameters.AddWithValue("@Qty",item.Quantity);cmd.Parameters.AddWithValue("@Total",item.Subtotal);cmd.Parameters.AddWithValue("@SaleDate",DateTime.Now);cmd.ExecuteNonQuery();}}catch(Exception ex){MessageBox.Show(ex.Message);}}
    }
}