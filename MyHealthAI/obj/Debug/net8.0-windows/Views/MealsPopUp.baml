   M S B A M L   `   `   `  ����E  AMyHealthAI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyHealthAI
MyHealthAI  5   	       MyHealthAI.MealsPopUp   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   *localclr-namespace:MyHealthAI     5   	     � Title$  
MyHealthAI��5   	   $	��500��5   	   $	��350��6    � 
ResizeMode$ CanMinimize=�5   	    � WindowStyle$ SingleBorderWindow=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   ��  �� DisplayMemberPath$
 Name��5      $��10q�5      $��0��5       ��5      �� *	MealTypes+ �� SelectedItem 5      �� *SelectedMealType+5       �� 5#   
   $��	10,0,10,0q�5$   
   $��1��5%   
      CustomListBoxItemStyle#��[  5'   
   ��5#      �� *MealsByType+ 5&   
   �� *SelectedMeal+��5(      �� 5)      .R�5*      ��  �� Orientation$ Vertical=�6!   $	��5,0q�68   .+�5+      �� $��14(�6:   $
��Bold"�6H   $��Center=�6Z   $	��0,5q�6u   ��6$   �� *Name+6u   �� 5,      $ 
Horizontal=�6%   $	��0,2q�6>   .+�5-      �� $��Calorías: ��6(   $��12(�6:   $��Center=�6H   ���#FF57336c   �� 5.      $��12(�6>   
���   �$	��5,0q�6_   ��6(   �� *Kcal+6_   �� 5/      $	�� | ��6(   $��12(�63   
���   �$	��5,0q�6T   �� 50      $��Proteínas: ��6(   $��12(�6;   $��Center=�6I   ���#4CAF506d   �� 51      $��12(�6A   
���   �$	��5,0q�6b   ��6(   �� *	Protein+6b   52      �� 53      $ 
Horizontal=�6%   $	��0,2q�6>   .+�54      �� $��Grasas: ��6(   $��12(�68   $��Center=�6F   ���#2196F36a   �� 55      $��12(�6=   
���   �$	��5,0q�6^   ��6(   �� *Fat+6^   �� 56      $	�� | ��6(   $��12(�63   
���   �$	��5,0q�6T   �� 57      $��Carbohidratos: ��6(   $��12(�6?   $��Center=�6M   ���#FFC1076h   �� 58      $��12(�6F   
���   �$	��5,0q�6g   ��6(   �� *Carbohydrate+6g   59      5:      5;      5=      �� 5?   
   $��Añadir Comida��6   $��Center=�6)   $��2��5@      $��0��6   $	��315��6*   $��5q�67   ��5?   F   �� *AddMealCommand+5@   7   5C      5D      