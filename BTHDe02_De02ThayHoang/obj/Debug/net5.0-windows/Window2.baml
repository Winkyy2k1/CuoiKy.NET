   M S B A M L   `   `   `  ����P  LBTHDe02_De02ThayHoang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:BTHDe02_De02ThayHoangBTHDe02_De02ThayHoang  5   	   #    BTHDe02_De02ThayHoang.Window2   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:BTHDe02_De02ThayHoang     5   	     � Title$  Window2��5   	   $	��450��6   $	��800��6&    � 	Resources 5	      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%|   '        �� 5
   
   "  6   .�5      ��  � ��$
 Bold"�6+   �� 5       � ��$ Center=�6;   5      5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��
dtSanPham2��5      $��100,61,100,234q�6&      AutoGenerateColumns$ False��6>   $	��600��6Z      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã SP��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6M   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 64   �� *MaSp+6M     5      $ Tên SP��6%   $	 100 6T   	 65   �� *
TenSanPham+6T     5      $ 
Đơn giá��6%   $	 100 6Q   	 66   �� *DonGia+6Q     5      $ Số lượng bán��6%   $	 100 6Z   	 6;   �� *
SoLuongBan+6Z     5      $ Nhóm hàng��6%   $	 100 6X   	 68   �� *TenNhomHang+6X     5      $ Tiền hàng��6%   $	 100 6T   
   
FontWeight$

 Bold"�6`      
Foreground
 �  ��	 68   �� *	TienBan+6r   5      5      5      