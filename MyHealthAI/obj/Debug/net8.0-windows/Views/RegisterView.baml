   M S B A M L   `   `   `  ����E  AMyHealthAI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyHealthAI
MyHealthAI  5   	       MyHealthAI.RegisterView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   *localclr-namespace:MyHealthAI     5   	     � Title$  Register��5   	   $	��600��6   $	��450��6'    � 
ResizeMode$ CanMinimize=�5   	    � WindowStyle$ SingleBorderWindow=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��258* �6   �� 5      $
��177* �6   �� 5      $
��145* �6    � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $��1* �6   .+�5   
   ��  �� CornerRadius$ 5��6   $��18,17,17,13q�6$   
�������$��1q�6L    J� RenderTransformOrigin$ 0.5,0.5&�6`   $��3��6�   $��2��6�   z�5      V� 5      .~�5      �� �� 5       �� AngleX$ 0.157Q�6$   �� 5       T� 5!      	 T� X$	 0.397Q�6)   5"      ._�5$      �� $��
10,10,10,0q�6   $	��528��6-   $	��Top=�6:   .+�5&      *   $MyHealthAI.PlaceholderTextBoxControl  $
��0,10q�5'   /   
   PlaceholderText$$
 Nombre (entre 3-15 caracteres)��5(   /   ��5&   2   �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=�5(   /     5*      $
��0,10q�5+   /   $
 Email��5,   /   ��5*   2   �� *Email+$ PropertyChanged=�5,   /   �� 5-      $��Contraseña��6   $��11(�6-   G� 5/      -   5         G�Name$ PasswordBoxControl��5/      $	��0,5q�51      �� 53      $��Repita la contraseña��6   $��11(�67   G� 55      -   5        $ PasswordBoxControl2��55      $	��0,5q�57        59      $	��0,9q�5:   /   $
 Tu peso en kg��5;   /   ��59   2   �� *Weight+$ PropertyChanged=�5;   /     5=      $	��0,9q�5>   /   $
 Tu altura en cm��5?   /   ��5=   2   �� *Height+$ PropertyChanged=�5?   /     5A      $	��0,9q�5B   /   $
 Tu edad��5C   /   ��5A   2   �� *Age+$ PropertyChanged=�5C   /     5F      $	��0,9q�5G   /   $
 Tu peso ideal��5H   /   ��5F   2   �� *
GoalWeight+$ PropertyChanged=�5H   /   �� 5K       �� DisplayMemberPath$
 Name��5M       �� SelectedValuePath$ ID��5O      $	��0,9q�5P      ��5K      �� *Gender+ �� SelectedValue 5L      �� *
GenderID+5P      �� 5R      $ ID��5T      $
 Name��5U      $	��0,9q�5V      ��5S      �� *	Objective+ 5W      �� *ObjectiveID+ �� Mode$ TwoWay=��� 5Y      $
 Name��5[      $ ID��5]      $	��0,9q�5^      ��5Y      �� *
Activity+ 5Z      �� *
ActivityID+5^      �� 5a      $��
Registrase��6   $	��0,9q�5c      $��26��6#   ��5b      �� *SaveCommand+5c   #   �� 5f      -   5        $��Login��5f      $��22��5h      $	��104��6"   $��0,0,0,90q�60   $��Right=�6B   $ 0.867,0.841&�6^   5i      5j      5k      5l      