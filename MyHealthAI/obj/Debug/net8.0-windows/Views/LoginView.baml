   M S B A M L   `   `   `  ����E  AMyHealthAI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyHealthAI
MyHealthAI  5   	   [ WLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF 5   	       MyHealthAI.LoginView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   *localclr-namespace:MyHealthAI     5   	   hlvcXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF     5   	     � Title$  Login��5   	   $	��600��6   $	��900��6$    � 
ResizeMode$ CanMinimize=�5   	    � WindowStyle$ SingleBorderWindow=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � DataContext 5         MyHealthAI.LiveCharts  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5#   
   �� 5$      $��* �6   .+�5'   
   5  /LiveChartsCore.SkiaSharpView.WPF.CartesianChart  $��146,62,88,-209q�5(   #   $��5��6;      Series 6	   �� *Series+5)      �� 5,   
   $��Email��6   $��Center=�6    $��Center=�6=   $��0��6X   $��10q�6e   �� 5.   
   -   5        $��	txtEmail1��5.      $	��300��6#   $��30��6/   $��Center=�6;   $��Center=�6X   $��1��6s   $��10q�6�   �� 50   
   $��Password��6   $��Center=�6#   $��Center=�6@   $��2��6[   $��10q�6h   G� 54   
   -   5        $��txtPassword1��54      $	��300��6*   $��30��66   $��Center=�6B   $��Center=�6_   $��3��6z   $��10q�6�   �� 56   
   -   5        $��btnLogin��56      $	��200��6!   $��40��6-   $��Center=�69   $��Center=�6V   $��4��6q   $��10q�6~   $��Login��6�   6�   �� 58   
   -   5        $��Login_Change��58      $��Change Register��6%   $��Center=�6?   $��Center=�6\   $��5��6w   $	��250��6�   $��10q�6�   59      5;      