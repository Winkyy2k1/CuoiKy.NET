   M S B A M L   `   `   `  ����Q  MBTHDe1_Ma02OnThayHoang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BTH_De02OnThayHoangBTH_De02OnThayHoang  5   	   $    BTH_De02OnThayHoang.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:BTH_De02OnThayHoang     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 	Resources=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%=  '��       '�� z     '  �    �� 5
   
   " ��6   .�5      ��  � ��$ Time New Roman)�6+   �� 5       � ��
 ��  �6+   5      �� 5   
   " ��6   .�5      ��  � ��$	 100��6&   �� 5       � ��$ 30��6'   �� 5       � ��
 ��� �6+   �� 5       � ��$
 Bold"�6+    5      ���� 5      '��       " ��6   .�5      ��  �@��CornerRadius$ 45��65   5      5      5      �� 5   
   "  6   .�5      ��  � ��$
 Bold"�6+   �� 5       � ��$ Center=�6;   5      5      .��5      � .+�5!   
   �� $��Mã bệnh nhân: ��6   $
��Left=�6)   $��	68,50,0,0q�6D   $	��Top=�6W    J� RenderTransformOrigin$ 0.566,-0.364&�6o   �� 5"   
   $��Họ tên: ��6   $
��Left=�6#   $��	70,76,0,0q�6>   $	��Top=�6Q   �� 5#   
   $��Số ngày nằm viện: ��6   $
��Left=�6-   $��
69,103,0,0q�6H   $	��Top=�6\   �� 5$   
   $��Khoa khám: ��6   $
��Left=�6&   $��
70,134,0,0q�6A   $	��Top=�6U   �� 5%   
   -   5        $��txtMabn��5%      $
��Left=�6!   $��
218,56,0,0q�6<   $�� ��6P    �� TextWrapping$
 Wrap=�6X   $	��Top=�6l   $	��120��6�   �� 5&   
   -   5        $��txtHoten��5&      $
��Left=�6"   $��
218,81,0,0q�6=   $�� ��6Q   $
 Wrap=�6Y   $	��Top=�6m   $	��274��6�   $��18��6�   �� 5'   
   -   5        $��	txtSoNgay��5'      $
��Left=�6#   $��218,108,0,0q�6>   $�� ��6S   $
 Wrap=�6[   $	��Top=�6o   $	��120��6�   �� 5(   
   -   5        $��cobKhoaKham��5(      $
��Left=�6&   $��218,134,0,0q�6A   $	��Top=�6V   $	��120��6n   &   System.Windows.Controls.DataGrid  5*   
   -   5        $��dataBenhNhan��5*      $��68,165,34,0q�6'   $	��160��6<   $	��Top=�6I   $��Normal"�6a   $��5��6u      AutoGenerateColumns$ False��6�   	   Columns	 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Mã bệnh nhân��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *MaBn+6T     5-      $
 	Họ Tên��6%   $	 100 6O    65   �� *HoTen+6O     5.      $
 	Tên Khoa��6%   $	 100 6S    67   �� *	TenKhoa+6S     5/      $
 Số ngày nằm viện��6%   $	 150 6a    6?   �� *SoNgayNamVien+6a     50      $
 Viện phí��6%   $	 100 6S      
Foreground
 �  ��   
FontWeight$
 Bold"�6p    67   �� *	VienPhi+   ElementStyle 51      �� 52      " ��6    .�53      ��  � ��$ Right=�6D   54      56      58      �� 59   
   -   5        $��btnThem��59       $��Thêm��6   $
��Left=�6/   $��263,348,0,0q�6J   $	��Top=�6_   6w   �� 5:   
   -   5        $��btnSua��5:      $��Sửa��6   $
��Left=�6-   $��434,349,0,0q�6H   $	��Top=�6]   6u   �� 5;   
   -   5        $��btnTim��5;      $
��Tìm��6   $
��Left=�6-   $��604,350,0,0q�6H   $	��Top=�6]   6u   �� 5<   
   -	   5        $��btnXoa��5<      $
��Xoá��6   $
��Left=�6-   $��108,350,0,0q�6H   $	��Top=�6]   6u   5>      5?      