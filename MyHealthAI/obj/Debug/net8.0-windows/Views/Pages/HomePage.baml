   M S B A M L   `   `   `  ����E  AMyHealthAI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyHealthAI
MyHealthAI  5      [ WLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF 5          MyHealthAI.HomePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:MyHealthAI     5      hlvcXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF     5        N� Title$  Page1��5
       � DataContext 5         MyHealthAI.LiveCharts  5   
   .��5      � ���#f1f3f5 � RowDefinitions 5   
   �� 5      $��0* �6   �� 5      $
��200* �6   �� 5      $
��300* �6    � ColumnDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5#   
   � ���#f1f3f5$��10q�6$   $��1��60   $��1��6=    5$      �� 5%      $��1* �6#   �� 5&      $��1* �6#   �� 5'      $��1* �6#   �� 5(      $��1* �6#   �� 5)      $��1* �6#   .+�5.      5  /LiveChartsCore.SkiaSharpView.WPF.CartesianChart  $��0��6k   $	��175��6{      Series 6!   �� *Series+   XAxes 6;   �� *XAxes+   YAxes 6S   �� *YAxes+6{     5/      $��1��6m   $	��175��6}    6!   �� *	Series1+ 6<   �� *XAxes+ 6T   �� *YAxes1+6}     50      $��2��6m   $	��175��6}    6!   �� *	Series2+ 6<   �� *XAxes+ 6T   �� *YAxes2+6}     51      $��3��6m   $	��175��6}    6!   �� *	Series3+ 6<   �� *XAxes+ 6T   �� *YAxes3+6}     52      $��4��6m   $	��175��6}    6!   �� *	Series4+ 6<   �� *XAxes+ 6T   �� *YAxes4+6}   54      �� 57   
   $��Sumar 250ml��6   $��715,190,2,15q�59   
   $��1��6    $��20��6-   $��80��69      NewGameButtonStyle#��[  6F   ��58   
   �� *WaterCommand+59   F   �� 5;   
   $��Actualizar grafico��6   $��130,190,2,15q�5=   
   $��1��6    $��20��6-   $	��103��69   #��[  6G   ��5<   
   �� *UpdateGrafic+5=   G   �� 5A   
   $
��Agua��6   $��Center=�6   $	��Top=�6<   $��1��6T   $��
715,5,2,15q�6a   $
��17.5(�6u   $��ubuntu)�6�   �� 5B   
   $��Grasas��6   $��Center=�6!   $	��Top=�6>   $��1��6V   $��
375,5,2,15q�6c   $
��17.5(�6w   $��ubuntu)�6�   �� 5C   
   $��Carbohidratos��6   $��Center=�6(   $	��Top=�6E   $��1��6]   $��	65,5,2,15q�6j   $
��17.5(�6}   $��ubuntu)�6�   �� 5D   
   $��	Proteinas��6   $��Center=�6$   $	��Top=�6A   $��1��6Y   $��-275,5,2,15q�6f   $
��17.5(�6{   $��ubuntu)�6�   �� 5E   
   $��Calorias��6   $��Center=�6#   $	��Top=�6@   $��1��6X   $��-586,5,2,15q�6e   $
��17.5(�6z   $��ubuntu)�6�     5I   
   $��1��68   $��0,181,415,180q�6E   $��2��6\    6   �� *	Series5+ 6m   �� *XAxes2+�� 5L   
    �� CornerRadius$ 5��6   $��427.5,220,15,15q�6$   
�������$��1q�6P    J� RenderTransformOrigin$ 0.5,0.5&�6d   $��3��6�   z�5M      V� 5N      .~�5O      �� �� 5P      	 �� AngleX$	 0.157Q�6$   �� 5Q      T� 5R      
 T� X$
 0.397Q�6)   5S      ._�5U      �� $��
10,10,10,0q�6   $	��326��6-   $	��Top=�6:   .+�5W      *   $MyHealthAI.PlaceholderTextBoxControl  $	��0,5q�5X   2      PlaceholderText$0 *Nombre de la comida (Maximo 15 caracteres)��5Y   2   ��5W   2   �� *
MealName+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions$
 True�� �� ValidatesOnDataErrors$
 True��5Y   2     5[      $	��0,5q�5\   2   $ Calorias (Max 10000)��5]   2   ��5[   2   �� *
MealKcal+$ PropertyChanged=�$
 True��$
 True��5]   2     5`      $	��0,5q�5a   2   $! Peso (Opcional (Max 10000))��5b   2   ��5`   2   �� *
MealWeight+$ PropertyChanged=�$
 True��$
 True��5b   2     5d      $	��0,5q�5e   2   $ Proteinas (Max 10000)��5f   2   ��5d   2   �� *MealProtein+$ PropertyChanged=�$
 True��$
 True��5f   2     5h      $	��0,5q�5i   2   $ Carbohidratos (Max 10000)��5j   2   ��5h   2   �� *MealCarbohydrate+$ PropertyChanged=�$
 True��$
 True��5j   2     5l      $	��0,5q�5m   2   $ Grasas (Max 10000)��5n   2   ��5l   2   �� *	MealFat+$ PropertyChanged=�$
 True��$
 True��5n   2   �� 5q       �� DisplayMemberPath$
 Name��5s       �� SelectedValuePath$ ID��5u      $	��0,5q�5v      ��5q      �� *
MealType+ �� SelectedValue 5r      �� *SelectedMealTypeId+5v      �� 5y      $��Guardar Comida��6   $	��0,5q�5{      ��5z      �� *SaveMealCommand+5{      �� 5}      $��Eliminar ultima comida��6   $	��0,5q�5      ��5~      �� *DeleteMealCommand+5      �� 5�      -   5        $%��Selelecionar comida de la lista��5�      $	��0,5q�5�      �� 5�      
��� � �$	��0,5q�6N   ��6   �� *StatusMessage+6N   5�      5�      �� 5�   
   $ 5��6   $��15,0,427.5,15q�6"   
�������$��1q�6L   $ 0.5,0.5&�6`   $��Bottom=�6�   $��2��6�   z�5�      V� 5�      .~�5�      �� �� 5�      $	 0.157Q�6$   �� 5�      T� 5�      $
 0.397Q�6)   5�      ._�5�      �� $��
10,10,10,0q�6   $	��158��6-   $��Bottom=�6:   .+�5�        $	��0,5q�5�   *   $1 +Nombre del ejercicio (Maximo 15 caracteres)��5�   *   ��5�   2   �� *ExerciseType+$ PropertyChanged=�$
 True��$
 True��5�   *     5�      $	��0,5q�5�   *   $3 -Duracion en minutos del ejercicio (Max 10000)��5�   *   ��5�   2   �� *DurationInMinutes+$ PropertyChanged=�$
 True��$
 True��5�   *     5�      $	��0,5q�5�   *   $* $Calorias quemadas aprox. (Max 10000)��5�   *   ��5�   2   �� *CaloriesBurned+$ PropertyChanged=�$
 True��$
 True��5�   *   �� 5�      $��Registrar Ejercicio��6   $
��0,10q�5�      ��5�      �� *SaveExerciseCommand+5�      �� 5�      $��Eliminar ultimo ejercicio��6   ��5�      �� *DeleteExerciseCommand+5�      5�      5�      5�      